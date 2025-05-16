using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure.Dialogs
{
    public partial class CloudConnectionDialog : Form
    {
        private const string AzurePublic = "Azure [Public]";
        private const string AzureCanary = "Azure [Public Canary]";
        private const string AzureChina = "Azure [China]";
        private const string AzureFairfax = "Azure [Fairfax]";
        private const string AzureDogfood = "Azure [Dogfood] (Private Geo with GeoProxy)";
        private const string AzureCsmDirect = "Azure [CSM-Direct] (Private Geo)";
        private const string AzureStackAsdkTenant = "Azure Stack [ASDK Tenant]";
        private const string AzureStackAsdkAdmin = "Azure Stack [ASDK Admin]";
        private const string AzureStackDevTenant = "Azure Stack [DEV Tenant]";
        private const string AzureStackDevAdmin = "Azure Stack [DEV Admin]";

        private static readonly string SettingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VisualARM.settings");

        private readonly string[] ArmEndpoints = new string []
        {
            AzurePublic,
            AzureCanary,
            AzureChina,
            AzureFairfax,
            AzureDogfood,
            AzureCsmDirect,
            AzureStackAsdkAdmin,
            AzureStackAsdkTenant,
            AzureStackDevAdmin,
            AzureStackDevTenant
        };

        // App Service on Azure - Dogfood and CSM-Direct defaults
        private const string DefaultGeoProxyPrivateStampEndpoint = "joaquinvmss1geo";
        private const string DefaultCsmDirectPrivateStampEndpoint = "geomaster.joaquinvmss1.antares-test.windows-int.net:444";

        // App Service on Azure Stack Hub defaults
        private const string DefaultAzureStackAdminArmEndpoint = "az-vanox:30005";
        private const string DefaultAzureStackTenantArmEndpoint = "az-vanox:40005";

        public CloudConnectionDialog()
        {
            InitializeComponent();

            this.environmentTypeComboBox.Items.AddRange(ArmEndpoints);
        }

        private void AzureStackConnectionDialog_Load(object sender, EventArgs e)
        {
            this.environmentTypeComboBox.SelectedIndex = 0;
            this.LoadSavedSettings();
            this.LoadSaveEnvironmentType();
        }

        public string AzureResourceManager
        {
            get
            {
                return this.azureResourceManagerEndpointTextBox.Text;
            }
        }

        public ConnectionType ConnectionType { get; private set; }

        public bool IsAzureStackEndpoint
        {
            get
            {
                string endpoint = (string)this.environmentTypeComboBox.SelectedItem;

                return 
                    endpoint == AzureStackAsdkTenant ||
                    endpoint == AzureStackAsdkAdmin ||
                    endpoint == AzureStackDevTenant ||
                    endpoint == AzureStackDevAdmin;
            }
        }

        public string PrivateGeoEndpoint
        {
            get
            {
                return this.privateGeoEndpointTextBox.Text;
            }
        }

        public Dictionary<string, string> SavedSettings { get; private set; }

        private string GetSetting(string settingName, string defaultValue = "")
        {
            if (this.SavedSettings.ContainsKey(settingName))
            {
                return this.SavedSettings[settingName];
            }

            return defaultValue;
        }

        private void environmentTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEndpoint = (string)this.environmentTypeComboBox.SelectedItem;
            switch (selectedEndpoint)
            {
                // Azure ARM
                case AzurePublic:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = "management.azure.com";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // Azure Canary
                case AzureCanary:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "brazilus.management.azure.com";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // Azure China
                case AzureChina:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.core.chinacloudapi.cn";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // Azure FairFax
                case AzureFairfax:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.usgovcloudapi.net";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // Azure Dogfood
                case AzureDogfood:
                    this.ConnectionType = ConnectionType.AzureResourceManagerProxy;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "api-dogfood.resources.windows-int.net";
                    this.moreInfoLinkLabel.Enabled = true;
                    this.privateGeoEndpointTextBox.Enabled = true;
                    this.privateGeoEndpointTextBox.Text = GetSetting("DefaultGeoProxyPrivateStampEndpoint", defaultValue: DefaultGeoProxyPrivateStampEndpoint);
                    break;
                // Azure DEV - GeoMaster ARM
                case AzureCsmDirect:
                    this.ConnectionType = ConnectionType.GeoMasterStamp;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultCsmDirectPrivateStampEndpoint", defaultValue: DefaultCsmDirectPrivateStampEndpoint);
                    this.moreInfoLinkLabel.Enabled = true;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // OneBox - Tenant ARM
                case AzureStackAsdkTenant:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultAzureStackTenantArmEndpoint", defaultValue: "management.local.azurestack.external");
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // OneBox - Admin ARM
                case AzureStackAsdkAdmin:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultAzureStackAdminArmEndpoint", defaultValue: "adminmanagement.local.azurestack.external");
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // DEV - Tenant ARM
                case AzureStackDevTenant:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultAzureStackTenantArmEndpoint", defaultValue: DefaultAzureStackTenantArmEndpoint);
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // DEV - Admin ARM
                case AzureStackDevAdmin:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultAzureStackAdminArmEndpoint", defaultValue: DefaultAzureStackAdminArmEndpoint);
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
            }

            ValidateForm();
        }

        private void moreInfoLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (InfoDialog dialog = new InfoDialog())
            {
                dialog.ShowDialog();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
        }

        private void CollectSettingsToSave()
        {
            this.SavedSettings["DefaultEnvironmentType"] = this.environmentTypeComboBox.Text;
            switch (this.environmentTypeComboBox.SelectedIndex)
            {
                // Azure Dogfood
                case 4:
                    this.SavedSettings["DefaultGeoProxyPrivateStampEndpoint"] = this.PrivateGeoEndpoint;

                    break;

                // Azure DEV - GeoMaster ARM
                case 5:
                    this.SavedSettings["DefaultCsmDirectPrivateStampEndpoint"] = this.AzureResourceManager;

                    break;

                // ASDK - Tenant ARM
                case 6:
                    this.SavedSettings["DefaultAzureStackTenantArmEndpoint"] = this.AzureResourceManager;

                    break;

                // ASDK - Admin ARM
                case 7:
                    this.SavedSettings["DefaultAzureStackAdminArmEndpoint"] = this.AzureResourceManager;

                    break;

                // DEV - Tenant ARM
                case 8:
                    this.SavedSettings["DefaultAzureStackTenantArmEndpoint"] = this.AzureResourceManager;

                    break;

                // DEV - Admin ARM
                case 9:
                    this.SavedSettings["DefaultAzureStackAdminArmEndpoint"] = this.AzureResourceManager;

                    break;
            }
        }

        private void LoadSavedSettings()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            
            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VisualARM.settings");
            if (File.Exists(settingsFile))
            {
                try
                {
                    foreach (string setting in File.ReadAllLines(settingsFile))
                    {
                        string[] parts = setting.Split(new char[] { '=' }, count: 2, options: StringSplitOptions.RemoveEmptyEntries);
                        settings.Add(parts[0], parts[1]);
                    }
                }
                catch
                {
                    File.Delete(settingsFile);
                }
            }
            
            this.SavedSettings = settings;
        }

        private void LoadSaveEnvironmentType()
        {
            string environmentType = GetSetting("DefaultEnvironmentType");
            if (!string.IsNullOrEmpty(environmentType))
            {
                int index = 0;
                foreach (var envType in this.environmentTypeComboBox.Items)
                {
                    if (envType.ToString() == environmentType)
                    {
                        this.environmentTypeComboBox.SelectedIndex = index;
                        break;
                    }

                    index++;
                }
            }
        }

        private void SaveSettings()
        {
            this.CollectSettingsToSave();

            string[] settings = this.SavedSettings.Select(pair => String.Concat(pair.Key, "=", pair.Value)).ToArray();

            File.WriteAllLines(SettingsFile, contents: settings.ToArray());            
        }

        private void clearSavedSettingsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                File.Delete(SettingsFile);

                MessageBox.Show("Settings file has been deleted successfully", "Visual ARM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Visual ARM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            finally
            {
                this.environmentTypeComboBox.SelectedIndex = 0;
            }
        }

        private void ValidateForm()
        {
            this.okButton.Enabled =
                (
                    this.environmentTypeComboBox.SelectedIndex != -1 &&
                    ((string)this.environmentTypeComboBox.SelectedItem) != AzureDogfood &&
                    ((string)this.environmentTypeComboBox.SelectedItem) != AzureCsmDirect &&
                    !string.IsNullOrWhiteSpace(azureResourceManagerEndpointTextBox.Text)
                ) ||
                (
                    this.environmentTypeComboBox.SelectedIndex != -1 &&
                    ((string)this.environmentTypeComboBox.SelectedItem) == AzureDogfood &&
                    !string.IsNullOrWhiteSpace(azureResourceManagerEndpointTextBox.Text) &&
                    !string.IsNullOrWhiteSpace(privateGeoEndpointTextBox.Text)
                ) ||
                (
                    this.environmentTypeComboBox.SelectedIndex != -1 &&
                    ((string)this.environmentTypeComboBox.SelectedItem) == AzureCsmDirect &&
                    !string.IsNullOrWhiteSpace(azureResourceManagerEndpointTextBox.Text)
                );                
        }

        private void certificateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void azureResourceManagerEndpointTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void privateGeoEndpointTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }
    }
}
