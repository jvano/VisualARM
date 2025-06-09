using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vano.Tools.Azure.Dialogs;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure
{
    public enum ConnectionStatus
    {
        Disconnected,
        Connecting,
        Connected
    }

    public partial class MainForm : Form
    {
        private IAzureClient _client = null;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        private string _azureResourceManagerEndpoint = null;
        private IEnumerable<Subscription> _subscriptions = null;
        private ConnectionType _connectionType = ConnectionType.AzureResourceManager;
        private IEnumerable<Template> _templates = null;
        private string _privateGeoEndpoint;
        private TemplateDocument _customTemplatesDocument;
        private BindingList<Request> _requests = new BindingList<Request>();
        private int MaxRequestsToKeep = 100;

        public MainForm()
        {
            InitializeComponent();

            //Trace.Listeners.Add(new TextBoxTraceListener(this.traceTextBox));
            Trace.Listeners.Add(new ColoredTextBoxTraceListener(this.traceColoredTextBox));
            verbToolStripComboBox.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadTemplates();
            BindRequestLog();
            ShowConnectDialog();
        }

        private async void connectionToolStripButton_Click(object sender, EventArgs e)
        {
            await Task.Yield();

            if (_client == null)
            {
                ShowConnectDialog();
            }
            else
            {
                UpdateConnectionStatus(ConnectionStatus.Disconnected);
            }
        }

        private void ShowConnectDialog()
        {
            UpdateConnectionStatus(ConnectionStatus.Disconnected);
            using (CloudConnectionDialog dialog = new CloudConnectionDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _connectionType = dialog.ConnectionType;
                    _privateGeoEndpoint = _connectionType == ConnectionType.AzureResourceManagerProxy ? dialog.PrivateGeoEndpoint : string.Empty;

                    UpdateConnectionStatus(ConnectionStatus.Connecting);
                    this.cloudTypeToolStripComboBox.Items.Add(dialog.AzureResourceManager);
                    this.cloudTypeToolStripComboBox.SelectedIndex = this.cloudTypeToolStripComboBox.Items.Count - 1;

                    // For private deployments let's not enforce SSL validation
                    if (dialog.IsAzureStackEndpoint)
                    {
                        ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) =>
                        {
                            return true;
                        });
                    }
                }
            }
        }

        private void UpdateConnectionStatus(ConnectionStatus status)
        {
            switch(status)
            {
                case ConnectionStatus.Disconnected:
                    _client = null;
                    this.mainProgressBar.Visible = false;
                    this.portalToolStripButton.Enabled = false;
                    this.portalToolStripButton.Text = $"Open Azure Portal";
                    this.cloudTypeToolStripComboBox.Items.Clear();
                    this.subsToolStripComboBox.Enabled = false;
                    this.subsToolStripComboBox.Items.Clear();
                    this.resourceGroupsToolStripComboBox.Enabled=false;
                    this.resourceGroupsToolStripComboBox.Items.Clear();
                    this.resourcesTreeView.Nodes.Clear();
                    this.connectionToolStripButton.Text = "Connect";
                    this.connectionToolStripButton.Enabled = true;
                    this.runToolStripButton.Enabled = false;
                    this.pathToolStripTextBox.Text = "/subscriptions/";
                    this.bodyColoredTextBox.Text = String.Empty;
                    _privateGeoEndpoint = string.Empty;
                    break;
                case ConnectionStatus.Connecting:
                    this.IsBusy = true;
                    this.connectionToolStripButton.Enabled = false;
                    break;
                case ConnectionStatus.Connected:
                    this.IsBusy = false;
                    this.mainProgressBar.Visible = false;
                    this.connectionToolStripButton.Text = "Disconnect";
                    this.connectionToolStripButton.Enabled = true;
                    if (_client.Metadata != null)
                    {
                        this.portalToolStripButton.Enabled = true;
                        this.portalToolStripButton.Text = $"Open Azure Portal ({_client.Metadata.PortalEndpoint})";
                    }

                    break;
            }
        }

        private void BindRequestLog()
        {
            this._requests.AllowEdit = true;
            this._requests.AllowNew = true;
            this._requests.AllowRemove = true;

            this.requestLogListBox.DataSource = this._requests;
            this.requestLogListBox.DisplayMember = "DisplayLabel";
            this.requestLogListBox.ValueMember = "DisplayLabel";
        }

        private async void cloudTypeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _azureResourceManagerEndpoint = (string)this.cloudTypeToolStripComboBox.SelectedItem;

            await Task.Yield();

            try
            {
                _client = _connectionType == ConnectionType.AzureResourceManager || _connectionType == ConnectionType.AzureResourceManagerProxy ?
                    ((IAzureClient)new AzureClient(
                        resourceManagerEndpoint: _azureResourceManagerEndpoint,
                        apiVersion: "2022-12-01",
                        metadata: null)) :
                    ((IAzureClient)new GeoMasterClient(
                        geoMasterEndpoint: _azureResourceManagerEndpoint,
                        apiVersion: "2022-12-01"));

                await _client.Initialize();

                await LoadSubscriptions();

                UpdateConnectionStatus(ConnectionStatus.Connected);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ERROR:");
                Trace.WriteLine(JsonHelper.FormatJson(ex.Message));
                Trace.WriteLine(string.Empty);

                UpdateConnectionStatus(ConnectionStatus.Disconnected);

                return;
            }
        }

        private async Task LoadSubscriptions()
        {
            _subscriptions = await Task.Run<IEnumerable<Subscription>>(() => _client.GetSubscriptions(_cts.Token));

            if (_subscriptions.Count() == 0)
            {
                throw new Exception("No subscriptions found.");
            }

            foreach (var sub in _subscriptions)
            {
                this.subsToolStripComboBox.Items.Add(new ToolStripMenuItem(sub.ToString())
                {
                    Tag = sub
                });
            }

            this.subsToolStripComboBox.Enabled = true;
            this.subsToolStripComboBox.SelectedIndex = this.subsToolStripComboBox.Items.Count - 1;
        }

        private async void subsToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Task.Yield();

            this.pathToolStripTextBox.Text = @"/";

            if (this.SelectedSubscription != null)
            {
                this.pathToolStripTextBox.Text = string.Format(@"/subscriptions/{0}/resourceGroups", this.SelectedSubscription.Id);
            }

            this.LoadResources();
        }

        public Subscription SelectedSubscription 
        { 
            get
            {
                Func<Subscription> action = new Func<Subscription>(() => 
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)this.subsToolStripComboBox.SelectedItem;
                    if (item != null)
                    {
                        return item.Tag as Subscription;
                    }

                    return null;
                });

                return this.InvokeRequired ? (Subscription)this.Invoke(action) : action();
            }
        }

        public ResourceGroup SelectedResourceGroup
        {
            get
            {
                Func<ResourceGroup> action = new Func<ResourceGroup>(() =>
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)this.resourceGroupsToolStripComboBox.SelectedItem;
                    if (item != null)
                    {
                        return item.Tag as ResourceGroup;
                    }

                    return null;
                });

                return this.InvokeRequired ? (ResourceGroup)this.Invoke(action) : action();
            }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.traceColoredTextBox.Text);
        }

        private void clearBodyToolStripButton_Click(object sender, EventArgs e)
        {
            this.bodyColoredTextBox.Text = string.Empty;
        }

        private void clearToolStripButton_Click(object sender, EventArgs e)
        {
            this.traceColoredTextBox.Text = string.Empty;
        }

        private void runToolStripButton_Click(object sender, EventArgs e)
        {
            SendRequest();
        }

        private void pathToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendRequest();
            }
        }

        public bool IsBusy
        {
            get
            {                
                return this.mainProgressBar.Visible;
            }
            set
            {
                this.mainProgressBar.Visible = value;
                this.cancelToolStripButton.Enabled = this.mainProgressBar.Visible = value;
                this.runToolStripButton.Enabled = !value;
            }
        }

        private async void SendRequest()
        {
            if (this.IsBusy)
            {
                return;
            }

            // Update the UI
            this.IsBusy = true;

            Guid newRequestGuid = Guid.NewGuid();
            try
            {
                if (autoClearToolStripButton.Checked)
                {
                    this.traceColoredTextBox.Text = string.Empty;
                }

                Subscription sub = this.SelectedSubscription;
                string tenantToken = await _client.GetAuthSecret(sub != null ? sub.TenantId : null);
                string path = pathToolStripTextBox.Text;
                string body = excludeBodyToolStripButton.Checked ? string.Empty : this.bodyColoredTextBox.Text;


                bool valid = true;
                IEnumerable<string> parametersInPath = FindMissingParameters(path).Distinct();
                if (parametersInPath.Count() > 0)
                {
                    valid = false;

                    Trace.WriteLine("PATH: The following parameters require a value:");

                    foreach (string parameter in parametersInPath)
                    {
                        Trace.WriteLine("    " + parameter);
                    }

                    Trace.WriteLine(string.Empty);
                }

                if (!valid)
                {
                    return;
                }

                Trace.WriteLine("REQUEST: " + verbToolStripComboBox.SelectedItem.ToString() + " " + pathToolStripTextBox.Text);
                Trace.WriteLine(string.Empty);

                Request newRequestToLog = new Request()
                {
                    Body = body,
                    Path = path,
                    Verb = verbToolStripComboBox.SelectedItem.ToString(),
                    Id = newRequestGuid
                };

                this.AddRequest(newRequestToLog);

                string responseToLog = string.Empty;

                _client.HttpHeadersProcessor = new HttpHeadersProcessor();

                string response = await _client.CallAzureResourceManager(
                    method: verbToolStripComboBox.SelectedItem.ToString(),
                    path: path,
                    token: tenantToken,
                    body: body,
                    parameters: null,
                    apiVersion: null,
                    displaySecrets: !this.hideTokensToolStripButton.Checked,
                    cancellationToken: _cts.Token);

                Trace.WriteLine("REQUEST HEADERS: ");
                Trace.WriteLine(_client.HttpHeadersProcessor.GetFormattedRequestHeaders());

                StringBuilder responseToLogBuilder = new StringBuilder();
                responseToLogBuilder.AppendLine("RESPONSE HEADERS: ");
                responseToLogBuilder.AppendLine(_client.HttpHeadersProcessor.GetFormattedResponseHeaders());
                responseToLogBuilder.AppendLine("RESPONSE: ");

                if (string.IsNullOrWhiteSpace(response))
                {
                    responseToLogBuilder.AppendLine("The request has completed successfully!");
                }
                else
                {                 
                    responseToLogBuilder.AppendLine(JsonHelper.FormatJson(response));
                }

                responseToLog = responseToLogBuilder.ToString();
                Trace.WriteLine(responseToLog);

                UpdateRequestResponse(newRequestGuid, responseToLog);

                Trace.WriteLine(string.Empty);
            }
            catch (AzureClientException ex)
            {
                string responseToLog = string.Empty;

                StringBuilder responseToLogBuilder = new StringBuilder();
                responseToLogBuilder.AppendLine("RESPONSE HEADERS: ");
                responseToLogBuilder.AppendLine(_client.HttpHeadersProcessor.GetFormattedResponseHeaders());
                responseToLogBuilder.AppendLine("RESPONSE: ");
                responseToLogBuilder.AppendLine(JsonHelper.FormatJson(ex.Response));
                responseToLogBuilder.AppendLine(string.Empty);
                responseToLog = responseToLogBuilder.ToString();

                Trace.WriteLine(responseToLog);

                UpdateRequestResponse(newRequestGuid, responseToLog);
            }
            catch (Exception ex)
            {
                string responseToLog = string.Empty;

                StringBuilder responseToLogBuilder = new StringBuilder();
                responseToLogBuilder.AppendLine("ERROR:");
                responseToLogBuilder.AppendLine(ex.Message);
                responseToLogBuilder.AppendLine(string.Empty);
                responseToLog = responseToLogBuilder.ToString();

                Trace.WriteLine(responseToLog);

                UpdateRequestResponse(newRequestGuid, responseToLog);
            }
            finally 
            {
                // Remove http header processor from the client instance
                _client.HttpHeadersProcessor = null;

                // Update the UI
                this.IsBusy = false;
            }
        }

        private IEnumerable<string> FindMissingParameters(string str)
        {
            List<string> parameters = new List<string>();
            foreach (Match m in Regex.Matches(input: str, pattern: @"<[\w-]+>"))
            {
                if (!parameters.Contains(m.Value))
                {
                    parameters.Add(m.Value);
                }
            }

            foreach (Match m in Regex.Matches(input: str, pattern: @"{[\w-]+}"))
            {
                if (!parameters.Contains(m.Value))
                {
                    parameters.Add(m.Value);
                }
            }

            return parameters;
        }

        private void loadResourcesToolStripButton_Click(object sender, EventArgs e)
        {
            LoadResources();
        }

        private void LoadResources()
        {
            this.loadResourcesToolStripButton.Enabled = false;
            this.resourcesTreeProgressBar.Visible = true;

            Subscription subscription = this.SelectedSubscription;
            this.resourcesTreeView.Nodes.Clear();
            try
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Text = "Resources";
                rootNode.ImageIndex = 0;

                ThreadPool.QueueUserWorkItem(async (state) => 
                {                                 
                    IEnumerable<ResourceProvider> resourceProviders = await GetResourceProviders(subscription).ConfigureAwait(continueOnCapturedContext: false);
                    if (resourceProviders != null)
                    {
                        IEnumerable<ResourceGroup> resourceGroups = await GetResourceGroups(resourceProviders, subscription).ConfigureAwait(continueOnCapturedContext: false);
                        if (resourceGroups != null)
                        {
                            LoadResourceGroupsCombo(resourceGroups);

                            TreeNode resourceGroupsNode = BuildResourceGroupsTree(resourceGroups);
                            rootNode.Nodes.Add(resourceGroupsNode);
                            rootNode.Expand();
                        }

                        TreeNode providersNode = BuildProvidersTree(resourceProviders);
                        rootNode.Nodes.Add(providersNode);
                        rootNode.Expand();
                    }

                    this.Invoke(new Action(() => 
                    {
                        this.resourcesTreeView.Nodes.Add(rootNode);

                        this.loadResourcesToolStripButton.Enabled = true;

                        this.resourcesTreeProgressBar.Visible = false;

                    }));
                }, null);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ERROR:");
                Trace.WriteLine(JsonHelper.FormatJson(ex.Message));
                Trace.WriteLine(string.Empty);

                this.loadResourcesToolStripButton.Enabled = true;
                this.resourcesTreeProgressBar.Visible = false;
            }
        }

        private void LoadResourceGroupsCombo(IEnumerable<ResourceGroup> resourceGroups)
        {
            Action load = new Action(() =>
            {
                this.resourceGroupsToolStripComboBox.Items.Clear();
                foreach (ResourceGroup rg in resourceGroups.OrderBy(g => g.Name))
                {
                    this.resourceGroupsToolStripComboBox.Items.Add(new ToolStripMenuItem(rg.Name)
                    {
                        Tag = rg
                    });
                }

                this.resourceGroupsToolStripComboBox.Enabled = this.resourceGroupsToolStripComboBox.Items.Count > 0;
                if (this.resourceGroupsToolStripComboBox.Enabled && this.resourceGroupsToolStripComboBox.Items.Count == 1)
                { 
                    this.resourceGroupsToolStripComboBox.SelectedIndex = 0;
                }
            });

            if (this.InvokeRequired)
            {
                this.Invoke(load);
            }
            else
            {
                load();
            }
        }

        private async Task<IEnumerable<ResourceProvider>> GetResourceProviders(Subscription subscription)
        {
            IEnumerable<ResourceProvider> resourceProviders = null;

            if (_connectionType == ConnectionType.GeoMasterStamp)
            {
                string apiVersion = "2016-09-01";
                                    
                List<ResourceProvider> antaresOnlyResourceProviders = new List<ResourceProvider>()
                {
                    new ResourceProvider()
                    {
                        Id = "Microsoft.Web",
                        Name = "Microsoft.Web",
                        ResourceTypes = new List<ResourceType>()
                        {
                            new ResourceType()
                            {
                                Name = "sites",
                                ApiVersions = new string[]
                                {
                                    apiVersion
                                }
                            },
                            new ResourceType()
                            {
                                Name = "serverFarms",
                                ApiVersions = new string[]
                                {
                                    apiVersion
                                }
                            }
                        }
                    }
                };

                return await Task.FromResult<IEnumerable<ResourceProvider>>(antaresOnlyResourceProviders);
            }

            string tenantToken = await _client.GetAuthSecret(subscription.TenantId);

            string response = await _client.CallAzureResourceManager(
                method: "GET",
                path: string.Format(@"/subscriptions/{0}/providers", subscription.Id),
                token: tenantToken,
                body: null,
                displaySecrets: !this.hideTokensToolStripButton.Checked,
                cancellationToken: _cts.Token);

            if (!string.IsNullOrWhiteSpace(response))
            {
                JObject json = JObject.Parse(response);
                resourceProviders = json
                    .Value<JArray>("value")
                    .Select(provider => new ResourceProvider()
                    {
                        Id = provider.Value<string>("id"),
                        Name = provider.Value<string>("namespace"),
                        ResourceTypes = provider
                            .Value<JArray>("resourceTypes")
                            .Select(resourceType => new ResourceType()
                            {
                                Name = resourceType.Value<string>("resourceType"),
                                //Locations = resourceType.Value<JArray>("locations").Select(location => location.ToString()),
                                ApiVersions = resourceType.Value<JArray>("apiVersions").Select(location => location.ToString()),
                            })
                    });
            }

            return resourceProviders;
        }

        private async Task<IEnumerable<ResourceGroup>> GetResourceGroups(IEnumerable<ResourceProvider> resourceProviders, Subscription subscription)
        {
            IEnumerable<ResourceGroup> resourceGroups = null;

            if (_connectionType == ConnectionType.GeoMasterStamp)
            {
                List<ResourceGroup> rdfeResourceGroups = new List<ResourceGroup>();
                IEnumerable<Location> locations = await _client.GetLocations(subscription);
                foreach(Location location in locations)
                {
                    string webspace = location.Name;
                    string resourceGroup = "Default-Web-" + webspace.Replace(" ", "");

                    rdfeResourceGroups.Add(new ResourceGroup()
                    {
                        Id = resourceGroup,
                        Name = resourceGroup,
                        Location = location.Name,
                        Providers = await GetResourceProviders(subscription)
                    });
                }

                return await Task.FromResult<IEnumerable<ResourceGroup>>(rdfeResourceGroups);
            }

            string tenantToken = await _client.GetAuthSecret(subscription.TenantId);

            string response = await _client.CallAzureResourceManager(
                method: "GET",
                path: string.Format(@"/subscriptions/{0}/resourceGroups", subscription.Id),
                token: tenantToken,
                body: null,
                displaySecrets: !this.hideTokensToolStripButton.Checked,
                cancellationToken: _cts.Token);

            if (!string.IsNullOrWhiteSpace(response))
            {
                JObject json = JObject.Parse(response);
                resourceGroups = json
                    .Value<JArray>("value")
                    .Select(provider => new ResourceGroup()
                    {
                        Id = provider.Value<string>("id"),
                        Name = provider.Value<string>("name"),
                        Location = provider.Value<string>("location"),
                        Providers = CloneProviders(resourceProviders.Where(p => p.Name.StartsWith("Microsoft.Web")))
                    });
            }

            return resourceGroups;
        }

        private TreeNode BuildProvidersTree(IEnumerable<ResourceProvider> resourceProviders)
        {
            TreeNode providersNode = new TreeNode();
            providersNode.Text = "Providers";
            providersNode.ImageIndex = 1;
            providersNode.SelectedImageIndex = 1;

            foreach (ResourceProvider provider in resourceProviders.OrderBy(p => p.Name))
            {
                TreeNode providerNode = new TreeNode();
                providerNode.Text = provider.Name;
                providerNode.Tag = provider;
                providerNode.ImageIndex = 1;
                providerNode.SelectedImageIndex = 1;

                foreach (ResourceType resourceType in provider.ResourceTypes.OrderBy(r => r.Name))
                {
                    // Link resource to the parent provider
                    resourceType.Provider = provider;

                    TreeNode resourceTypeNode = new TreeNode();
                    resourceTypeNode.Text = resourceType.Name;
                    resourceTypeNode.Tag = resourceType;
                    resourceTypeNode.ImageIndex = 1;
                    resourceTypeNode.SelectedImageIndex = 1;

                    foreach (string apiVersion in resourceType.ApiVersions.OrderBy(v => v))
                    {
                        TreeNode apiVersionNode = new TreeNode();
                        apiVersionNode.Text = apiVersion;
                        apiVersionNode.Tag = apiVersion;
                        apiVersionNode.ImageIndex = 1;
                        apiVersionNode.SelectedImageIndex = 1;

                        resourceTypeNode.Nodes.Add(apiVersionNode);
                    }

                    //resourceTypeNode.Expand();
                    providerNode.Nodes.Add(resourceTypeNode);
                }

                //providerNode.Expand();
                providersNode.Nodes.Add(providerNode);
            }

            providersNode.Expand();

            return providersNode;
        }

        private TreeNode BuildResourceGroupsTree(IEnumerable<ResourceGroup> resourceGroups)
        {
            TreeNode resourceGroupsNode = new TreeNode();
            resourceGroupsNode.Text = "Resource Groups";
            resourceGroupsNode.ImageIndex = 1;
            resourceGroupsNode.SelectedImageIndex = 1;

            foreach (ResourceGroup resourceGroup in resourceGroups.OrderBy(g => g.Name))
            {
                TreeNode resourceGroupNode = new TreeNode();
                resourceGroupNode.Text = resourceGroup.Name;
                resourceGroupNode.Tag = resourceGroup;
                resourceGroupNode.ImageIndex = 1;
                resourceGroupNode.SelectedImageIndex = 1;

                foreach (ResourceProvider provider in resourceGroup.Providers.OrderBy(p => p.Name))
                {
                    // Link provider to the resource group
                    provider.Group = resourceGroup;

                    TreeNode providerNode = new TreeNode();
                    providerNode.Text = provider.Name;
                    providerNode.Tag = provider;
                    providerNode.ImageIndex = 1;
                    providerNode.SelectedImageIndex = 1;

                    foreach (ResourceType resourceType in provider.ResourceTypes.OrderBy(r => r.Name))
                    {
                        // Link resource to the parent provider
                        resourceType.Provider = provider;

                        TreeNode resourceTypeNode = new TreeNode();
                        resourceTypeNode.Text = resourceType.Name;
                        resourceTypeNode.Tag = resourceType;
                        resourceTypeNode.ImageIndex = 1;
                        resourceTypeNode.SelectedImageIndex = 1;

                        foreach (string apiVersion in resourceType.ApiVersions.OrderBy(v => v))
                        {
                            TreeNode apiVersionNode = new TreeNode();
                            apiVersionNode.Text = apiVersion;
                            apiVersionNode.Tag = apiVersion;
                            apiVersionNode.ImageIndex = 1;
                            apiVersionNode.SelectedImageIndex = 1;

                            resourceTypeNode.Nodes.Add(apiVersionNode);
                        }

                        //resourceTypeNode.Expand();
                        providerNode.Nodes.Add(resourceTypeNode);
                    }

                    //providerNode.Expand();
                    resourceGroupNode.Nodes.Add(providerNode);
                }

                //resourceGroupNode.Expand();
                resourceGroupsNode.Nodes.Add(resourceGroupNode);
            }

            resourceGroupsNode.Expand();

            return resourceGroupsNode;
        }

        private void resourcesTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ResourceProvider provider = e.Node.Tag as ResourceProvider;
            if (provider != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(_privateGeoEndpoint))
                {
                    parameters.Add("stamp", _privateGeoEndpoint);
                }

                ResourceType resourceType = provider.ResourceTypes.FirstOrDefault();
                if (resourceType != null)
                {
                    string apiVersion = resourceType.ApiVersions.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(apiVersion))
                    {
                        parameters.Add("api-version", apiVersion);
                    }
                }
               
                string queryString = BuildQueryString(parameters);

                this.pathToolStripTextBox.Text = provider.Group != null ?  
                    string.Format(@"/subscriptions/{0}/resourceGroups/{1}/providers/{2}{3}", this.SelectedSubscription.Id, provider.Group.Name, provider.Name, queryString) :
                    string.Format(@"/subscriptions/{0}/providers/{1}{2}", this.SelectedSubscription.Id, provider.Name, queryString);
                this.verbToolStripComboBox.SelectedIndex = 0;
                this.bodyColoredTextBox.Text = string.Empty;
                this.SendRequest();
            }

            ResourceType resType = e.Node.Tag as ResourceType;
            if (resType != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(_privateGeoEndpoint))
                {
                    parameters.Add("stamp", _privateGeoEndpoint);
                }

                string apiVersion = resType.ApiVersions.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(apiVersion))
                {
                    parameters.Add("api-version", apiVersion);
                }

                string queryString = BuildQueryString(parameters);

                this.pathToolStripTextBox.Text = resType.Provider.Group != null ?
                    string.Format(@"/subscriptions/{0}/resourceGroups/{1}/providers/{2}/{3}{4}", this.SelectedSubscription.Id, resType.Provider.Group.Name, resType.Provider.Name, resType.Name, queryString) :
                    string.Format(@"/subscriptions/{0}/providers/{1}/{2}{3}", this.SelectedSubscription.Id, resType.Provider.Name, resType.Name, queryString);
                this.verbToolStripComboBox.SelectedIndex = 0;
                this.bodyColoredTextBox.Text = string.Empty;
                this.SendRequest();
            }
        }

        private static string BuildQueryString(Dictionary<string, string> parameters)
        {
            StringBuilder queryString = new StringBuilder();

            bool first = true;
            foreach (KeyValuePair<string, string> param in parameters)
            {
                if (first)
                {
                    queryString.Append("?");
                    first = false;
                }
                else
                {
                    queryString.Append("&");
                }

                queryString.Append(param.Key);
                queryString.Append("=");
                queryString.Append(param.Value);
            }

            return queryString.ToString();
        }

        private IEnumerable<ResourceProvider> CloneProviders(IEnumerable<ResourceProvider> providers)
        {
            List<ResourceProvider> clonedResourceProviderList = new List<ResourceProvider>();
            foreach (ResourceProvider provider in providers)
            {
                ResourceProvider clonedProvider = new ResourceProvider();
                clonedProvider.Id = provider.Id;
                clonedProvider.Name = provider.Name;

                List<ResourceType> clonedResourceTypeList = new List<ResourceType>();
                foreach (ResourceType resourceType in provider.ResourceTypes)
                {
                    ResourceType clonedTesourceType = new ResourceType();
                    clonedTesourceType.Name = resourceType.Name;
                    clonedTesourceType.Locations = resourceType.Locations;
                    clonedTesourceType.ApiVersions = resourceType.ApiVersions;

                    clonedResourceTypeList.Add(clonedTesourceType);
                }

                clonedProvider.ResourceTypes = clonedResourceTypeList;

                clonedResourceProviderList.Add(clonedProvider);
            }

            return clonedResourceProviderList;
        }

        private void copyBodyToolStripButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.bodyColoredTextBox.Text);
        }

        private void OnTextBoxKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                TextBox txtBox = (TextBox)sender;
                txtBox.SelectAll();
            }
        }

        private async void LoadTemplates()
        {
            templatesToolStripComboBox.Items.Clear();

            _templates = await TemplateFactory.GetTemplates();

            IEnumerable<Template> templatesToDisplay = null;

            bool hasCustomTemplates = false;
            if (_customTemplatesDocument == null)
            {
                _customTemplatesDocument = TemplateDocument.FromFile();
            }

            if (_customTemplatesDocument == null)
            {
                templatesToDisplay = _templates;
                hasCustomTemplates = false;
            }
            else
            {
                List<Template> templatesWithCustomTemplates = new List<Template>(_templates);
                templatesWithCustomTemplates.AddRange(_customTemplatesDocument.Templates);

                templatesToDisplay = templatesWithCustomTemplates;

                hasCustomTemplates = _customTemplatesDocument.Templates.Count() > 0;
            }

            IEnumerable<TemplateCategory> categories = templatesToDisplay.GroupBy(t => t.Category, (name, group) => 
                new TemplateCategory()
                {
                    Name = name,
                    Templates = group
                });

            foreach (TemplateCategory category in categories)
            {
                templatesToolStripComboBox.Items.Add(category);
            }

            if (templatesToolStripComboBox.Items.Count > 0)
            {
                templatesToolStripComboBox.SelectedIndex = 0;
            }
        }

        private void templatesToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenderTemplateList();
        }


        private void filterTemplateToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            RenderTemplateList();
        }

        private void RenderTemplateList()
        {
            TemplateCategory category = templatesToolStripComboBox.SelectedItem as TemplateCategory;
            if (category != null)
            {
                IEnumerable<Template> templates = category.Templates;

                if (!string.IsNullOrEmpty(filterTemplateToolStripTextBox.Text))
                {
                    string filter = filterTemplateToolStripTextBox.Text;

                    templates = templates.Where(template => 
                        string.IsNullOrEmpty(template.Summary) ? 
                        template.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) != -1 :
                        template.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) != -1 || template.Summary.IndexOf(filter, StringComparison.OrdinalIgnoreCase) != -1);
                }

                templateListBox.DataSource = new BindingSource(templates, null);
            }
        }

        private void templateListBox_DoubleClick(object sender, EventArgs e)
        {
            Template template = templateListBox.SelectedItem as Template;
            if (template != null)
            {
                string defaultApiVersion = "2016-09-01";
                if (_connectionType == ConnectionType.AzureResourceManagerProxy)
                {
                    // GeoProxy will use this and stamp querysting parameter to forward requests
                    defaultApiVersion += "-privatepreview";
                }

                string geoProxyStampParameter = !string.IsNullOrEmpty(_privateGeoEndpoint) ? "stamp=" + _privateGeoEndpoint + "&" : "";

                string resourceGroup = "<resourcegroup>";
                string location = "<location>";
                string subscription = this.SelectedSubscription?.Id ?? "<subscription>";
                string defaultResourceGroup = this.SelectedResourceGroup?.Name;
                if (!string.IsNullOrEmpty(defaultResourceGroup))
                {
                    resourceGroup = defaultResourceGroup;
                    location = defaultResourceGroup.Split(new char[] { '-' }).Last();
                    location = Regex.Replace(location, "([a-z])([A-Z])", "$1 $2");
                    location = Regex.Replace(location, "([a-zA-Z])([0-9])", "$1 $2");
                }

                verbToolStripComboBox.Text = template.Verb;
                pathToolStripTextBox.Text = template.Path
                    .Replace("<subscription>", subscription)
                    .Replace("{subscriptionId}", subscription)
                    .Replace("<resourcegroup>", resourceGroup)
                    .Replace("{resourceGroupName}", resourceGroup)
                    .Replace("stamp=<stamp>&", geoProxyStampParameter)
                    .Replace("<api-version>", defaultApiVersion);

                bodyColoredTextBox.Text = string.IsNullOrWhiteSpace(template.Body) ? string.Empty :
                    template.Body
                        .Replace("<subscription>", subscription)
                        .Replace("{subscriptionId}", subscription)
                        .Replace("<resourcegroup>", resourceGroup)
                        .Replace("{resourceGroupName}", resourceGroup)
                        .Replace("<location>", location)
                        .Replace("{location}", location);

                if (MessageBox.Show(string.Format("Would you like to run the '{0}' template?", template.Name), this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SendRequest();
                }
            }
        }

        private void addRecipeToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (AddTemplateDialog dialog = new AddTemplateDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.Template == null)
                        {
                            MessageBox.Show("No template returned from the dialog", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        if (_customTemplatesDocument == null)
                        {
                            _customTemplatesDocument = new TemplateDocument();
                        }

                        _customTemplatesDocument.AddTemplate(dialog.Template);
                        _customTemplatesDocument.Save();

                        LoadTemplates();

                        MessageBox.Show(
                            string.Format(@"The template '{0}' has been successfully created!", dialog.Template.Name) + Environment.NewLine + Environment.NewLine +
                            @"The custom templates are persisted in %APPDATA%\AzureResourceManagerClient\Templates.txt", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void issuesToolStripStatusLabel_Click(object sender, EventArgs e)
        {
            this.RunCommand(((ToolStripStatusLabel)sender).Text);
        }

        private void createdByToolStripStatusLabel_Click(object sender, EventArgs e)
        {
            this.RunCommand(((ToolStripStatusLabel)sender).Text);
        }

        private void portalToolStripButton_Click(object sender, EventArgs e)
        {
            this.RunCommand(_client.Metadata.PortalEndpoint);
        }

        private void RunCommand(string command)
        {
            try
            {
                Process.Start(command);
            }
            catch
            {
                // DO NOTHING
            }
        }

        private void sitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSitesBatchDialog();
        }

        private void ShowSitesBatchDialog()
        {
            string resourceGroupParameter = this.pathToolStripTextBox.Text;

            if (this.SelectedResourceGroup != null)
            {
                resourceGroupParameter = this.SelectedResourceGroup.Id;
            }


            using (SitesBatchDialog dialog = new SitesBatchDialog(resourceGroupParameter, this.bodyColoredTextBox.Text))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    decimal numberOfSites = dialog.NumberOfSites;
                    string siteNamePrefix = dialog.SiteNamePrefix;

                    for (int i = 0; i < numberOfSites; i++)
                    {
                        string siteName = string.Concat(dialog.SiteNamePrefix, "-", i);
                        this.pathToolStripTextBox.Text = string.Concat(dialog.ResourceGroup, "/providers/Microsoft.Web/sites/", siteName, "?api-version=2016-09-01");
                        this.bodyColoredTextBox.Text = dialog.RequestBodyTemplate.Replace("<sitename>", siteName);
                        SendRequest();
                    }
                }
            }
        }

        private void AddRequest(Request newRequest)
        {
            if (this._requests.Count == MaxRequestsToKeep)
            {
                this._requests.RemoveAt(0);
            }

            this._requests.Add(newRequest);
        }

        private void UpdateRequestResponse(Guid RequestId, string response)
        {
            Request requestToEdit = this._requests.SingleOrDefault(r => r.Id == RequestId);

            if (requestToEdit != null)
            {
                requestToEdit.Response = response;
            }
        }


        private void clearRequestsToolStripButton_Click(object sender, EventArgs e)
        {
            this._requests.Clear();
        }

        private void requestLogListBox_DoubleClick(object sender, EventArgs e)
        {
            this.bodyColoredTextBox.Text = ((Request)this.requestLogListBox.SelectedItem).Body;
            this.verbToolStripComboBox.Text = ((Request)this.requestLogListBox.SelectedItem).Verb;
            this.pathToolStripTextBox.Text = ((Request)this.requestLogListBox.SelectedItem).Path;
            this.traceColoredTextBox.Text = ((Request)this.requestLogListBox.SelectedItem).Response;
        }

        private void verbToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool excludeBody;
            string selectedVerb = verbToolStripComboBox.Text.ToUpper();
            if (selectedVerb == "GET" || selectedVerb == "DELETE" || selectedVerb == "HEAD")
            {
                excludeBody = true;
            }
            else
            {
                excludeBody = false;
            }

            excludeBodyToolStripButton.Checked = excludeBody;
        }

        private void wordWrapToolStripButton_Click(object sender, EventArgs e)
        {
            traceColoredTextBox.WordWrap = wordWrapToolStripButton.Checked;
        }

        private void cancelToolStripButton_Click(object sender, EventArgs e)
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _cts = new CancellationTokenSource();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit!", "VisualARM", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
