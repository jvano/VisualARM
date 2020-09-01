using System.Windows.Forms;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure.Dialogs
{
    public partial class AddTemplateDialog : Form
    {
        public AddTemplateDialog()
        {
            InitializeComponent();

            this.templateNameTextBox.Text = "Get Web App";
            this.verbsComboBox.SelectedIndex = 0;
            this.pathTextBox.Text = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>?api-version=2016-09-01";
        }

        public Template Template
        {
            get
            {
                return new Template()
                {
                    Custom = true,
                    Category = "Custom",
                    Name = this.templateNameTextBox.Text.Trim(),
                    Verb = this.verbsComboBox.SelectedItem?.ToString(),
                    Path = this.pathTextBox.Text.Trim(),
                    Body = this.bodyColoredTextBox.Text.Trim()
                };
            }
        }
    }
}
