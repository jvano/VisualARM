using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure.Dialogs
{
    public partial class CloudConnectionDialog : Form
    {
        public CloudConnectionDialog()
        {
            InitializeComponent();
        }

        private void AzureStackConnectionDialog_Load(object sender, EventArgs e)
        {
            this.environmentTypeComboBox.SelectedIndex = 0;
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
                    break;
                // Azure China
                case 1:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.core.chinacloudapi.cn";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    break;
                // Azure FairFax
                case 2:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.usgovcloudapi.net";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    break;
                // Azure Dogfood
                case 3:
                    this.ConnectionType = ConnectionType.AzureResourceManagerProxy;
                    this.azureResourceManagerEndpointTextBox.Enabled = false;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "api-dogfood.resources.windows-int.net";
                    this.moreInfoLinkLabel.Enabled = true;
                    this.privateGeoEndpointTextBox.Enabled = true;
                    break;
                // Azure DEV - GeoMaster ARM
                case 4:
                    this.ConnectionType = ConnectionType.GeoMasterStamp;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = true;
                    this.azureResourceManagerEndpointTextBox.Text = "joaquinvvmssgeo.cloudapp.net:444";
                    this.LoadCertificates();
                    this.moreInfoLinkLabel.Enabled = true;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    break;
                // OneBox - Tenant ARM
                case 5:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "management.local.azurestack.external";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    break;
                // OneBox - Admin ARM
                case 6:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "adminmanagement.local.azurestack.external";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    break;
                // DEV - Tenant ARM
                case 7:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "az-vanox:40005";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    break;
                // DEV - Admin ARM
                case 8:
                    this.ConnectionType = ConnectionType.AzureResourceManager;
                    this.azureResourceManagerEndpointTextBox.Enabled = true;
                    this.certificateComboBox.Enabled = false;
                    this.azureResourceManagerEndpointTextBox.Text = "az-vanox:30005";
                    this.moreInfoLinkLabel.Enabled = false;
                    this.privateGeoEndpointTextBox.Enabled = false;
                    break;
            }
        }

        private void LoadCertificates()
        {
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
                    this.certificateComboBox.Items.Add(cert.Thumbprint);
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
    }
}
