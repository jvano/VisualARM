using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vano.Tools.Azure.Dialogs
{
    public partial class SitesBatchDialog : Form
    {
        public SitesBatchDialog(string resourceGroup, string bodyTemplate)
        {
            InitializeComponent();
            this.resourceGroupTextBox.Text = resourceGroup;
            this.requestBodyText.Text = bodyTemplate;
            this.siteNamePrefixTextBox.Text = "samplesite";
            this.numberOfSitesNumericUpDown.Value = 3;
            this.UpdateSitesCreatedInfo();
        }

        public decimal NumberOfSites
        {
            get
            {
                return this.numberOfSitesNumericUpDown.Value;
            }
        }

        public string ResourceGroup
        {
            get
            {
                return this.resourceGroupTextBox.Text;
            }
        }

        public string SiteNamePrefix
        {
            get
            {
                return this.siteNamePrefixTextBox.Text;
            }

        }

        public string RequestBodyTemplate
        {
            get
            {
                return this.requestBodyText.Text;
            }
        }

        private void siteNamePrefixTextBox_TextChanged(object sender, EventArgs e)
        {
            this.UpdateSitesCreatedInfo();
        }

        private void numberOfSitesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateSitesCreatedInfo();
        }

        private void UpdateSitesCreatedInfo()
        {
            this.labelSiteNameExample.Text = GetSiteNameExampleText((int)this.numberOfSitesNumericUpDown.Value, this.siteNamePrefixTextBox.Text);
        }

        private string GetSiteNameExampleText(int numberOfSites, string siteNamePrefix)
        {
            if (numberOfSites <= 0 )
            {
                return "Specify a number of sites." ;
            }

            if (string.IsNullOrWhiteSpace(siteNamePrefix))
            {
                return "Type a site name prefix" ;
            }

            StringBuilder result = new StringBuilder();
            result.AppendFormat("Will create {0} site{1}: ", numberOfSites, numberOfSites == 1 ? string.Empty : "s");

            for (int i = 0; i < numberOfSites; i++)
            {
                if (i >= 3)
                {
                    result.Append("...");
                    break;
                }

                result.AppendFormat("{0}-{1}, ", siteNamePrefix, i);
            }

            return result.ToString();
        }
    }
}
