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
        // App Service on Azure - Dogfood and CSM-Direct defaults
        private const string DefaultGeoProxyPrivateStampEndpoint = "joaquinvvmssgeo";
        private const string DefaultCsmDirectPrivateStampEndpoint = "joaquinvvmssgeo.cloudapp.net:444";

        // App Service on Azure Stack Hub defaults
        private const string DefaultAzureStackAdminArmEndpoint = "az-vanox:30005";
        private const string DefaultAzureStackTenantArmEndpoint = "az-vanox:40005";

        public CloudConnectionDialog()
        {
            InitializeComponent();
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

        public string CertThumbprint
        {
            get
            {
                return this.certificateComboBox.SelectedItem != null ? 
                    this.certificateComboBox.SelectedItem.ToString() :
                    null;
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
            switch (this.environmentTypeComboBox.SelectedIndex)
            {
                // Azure ARM
                case 0:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.azure.com";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // Azure China
                case 1:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.core.chinacloudapi.cn";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // Azure FairFax
                case 2:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.usgovcloudapi.net";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // Azure Dogfood
                case 3:
                    this.ConnectionType = ConnectionType.AzureResourceManagerProxy;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "api-dogfood.resources.windows-int.net";
                    this.moreInfoLinkLabel.Enabled = true;
                    this.privateGeoEndpointTextBox.Enabled = true;
                    this.privateGeoEndpointTextBox.Text = GetSetting("DefaultGeoProxyPrivateStampEndpoint", defaultValue: DefaultGeoProxyPrivateStampEndpoint);
                    break;
                // Azure DEV - GeoMaster ARM
                case 4:
                    this.ConnectionType = ConnectionType.GeoMasterStamp;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultCsmDirectPrivateStampEndpoint", defaultValue: DefaultCsmDirectPrivateStampEndpoint);
                    this.LoadCertificates();
                    this.moreInfoLinkLabel.Enabled = true;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // OneBox - Tenant ARM
                case 5:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.local.azurestack.external";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // OneBox - Admin ARM
                case 6:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "adminmanagement.local.azurestack.external";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // DEV - Tenant ARM
                case 7:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultAzureStackTenantArmEndpoint", defaultValue: DefaultAzureStackTenantArmEndpoint);
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
                // DEV - Admin ARM
                case 8:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = GetSetting("DefaultAzureStackAdminArmEndpoint", defaultValue: DefaultAzureStackAdminArmEndpoint);
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    this.privateGeoEndpointTextBox.Text = string.Empty;
                    break;
            }
        }

        private void LoadCertificates()
        {
            string defaultCertificateThumbprint = GetSetting("DefaultCertificateThumbprint"); 
            
            this.certificateComboBox.Items.Clear();
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            try
            {
                List<X509Certificate2> certs = new List<X509Certificate2>();
                foreach (X509Certificate2 cert in store.Certificates)
                {
                    certs.Add(cert);
                }

                foreach (X509Certificate2 cert in certs.OrderBy(c => c.Thumbprint))
                {
                    int index = this.certificateComboBox.Items.Add(cert.Thumbprint);
                    
                    if (!string.IsNullOrEmpty(defaultCertificateThumbprint) &&
                        cert.Thumbprint == defaultCertificateThumbprint)
                    {
                        this.certificateComboBox.SelectedIndex = index;
                    }
                }
            }
            finally
            {
                store.Close();
            }
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
                case 3:
                    this.SavedSettings["DefaultGeoProxyPrivateStampEndpoint"] = this.PrivateGeoEndpoint;

                    break;

                // Azure DEV - GeoMaster ARM
                case 4:
                    this.SavedSettings["DefaultCsmDirectPrivateStampEndpoint"] = this.AzureResourceManager;
                    this.SavedSettings["DefaultCertificateThumbprint"] = this.CertThumbprint;

                    break;

                // DEV - Tenant ARM
                case 7:
                    this.SavedSettings["DefaultAzureStackTenantArmEndpoint"] = this.AzureResourceManager;

                    break;

                // DEV - Admin ARM
                case 8:
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
                foreach (string setting in File.ReadAllLines(settingsFile))
                {
                    string[] parts = setting.Split(new char[] { '=' }, count: 2, options: StringSplitOptions.RemoveEmptyEntries);
                    settings.Add(parts[0], parts[1]);
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

            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VisualARM.settings");

            string[] settings = this.SavedSettings.Select(pair => String.Concat(pair.Key, "=", pair.Value)).ToArray();

            File.WriteAllLines(settingsFile, contents: settings.ToArray());            
        }
    }
}
