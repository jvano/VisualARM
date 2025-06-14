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
            this.pathTextBox.Text = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}";
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
