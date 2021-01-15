namespace Vano.Tools.Azure.Dialogs
{
    partial class CloudConnectionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloudConnectionDialog));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.environmentTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.azureResourceManagerEndpointTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.certificateComboBox = new System.Windows.Forms.ComboBox();
            this.moreInfoLinkLabel = new System.Windows.Forms.LinkLabel();
            this.privateGeoEndpointTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(358, 232);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(439, 232);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 63);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connect to Cloud";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 37);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // environmentTypeComboBox
            // 
            this.environmentTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.environmentTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.environmentTypeComboBox.FormattingEnabled = true;
            this.environmentTypeComboBox.Items.AddRange(new object[] {
            "Azure [Public]",
            "Azure [China]",
            "Azure [Fairfax]",
            "Azure [Dogfood] (Private Geo with GeoProxy)",
            "Azure [CSM-Direct] (Private Geo)",
            "Azure Stack [ASDK Tenant]",
            "Azure Stack [ASDK Admin]",
            "Azure Stack [DEV Tenant]",
            "Azure Stack [DEV Admin]"});
            this.environmentTypeComboBox.Location = new System.Drawing.Point(123, 86);
            this.environmentTypeComboBox.Name = "environmentTypeComboBox";
            this.environmentTypeComboBox.Size = new System.Drawing.Size(391, 21);
            this.environmentTypeComboBox.TabIndex = 0;
            this.environmentTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.environmentTypeComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Environment Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ARM endpoint:";
            // 
            // azureResourceManagerEndpointTextBox
            // 
            this.azureResourceManagerEndpointTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.azureResourceManagerEndpointTextBox.Enabled = false;
            this.azureResourceManagerEndpointTextBox.Location = new System.Drawing.Point(123, 122);
            this.azureResourceManagerEndpointTextBox.Name = "azureResourceManagerEndpointTextBox";
            this.azureResourceManagerEndpointTextBox.Size = new System.Drawing.Size(391, 20);
            this.azureResourceManagerEndpointTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Certificate:";
            // 
            // certificateComboBox
            // 
            this.certificateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.certificateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.certificateComboBox.Enabled = false;
            this.certificateComboBox.FormattingEnabled = true;
            this.certificateComboBox.Location = new System.Drawing.Point(123, 196);
            this.certificateComboBox.Name = "certificateComboBox";
            this.certificateComboBox.Size = new System.Drawing.Size(391, 21);
            this.certificateComboBox.TabIndex = 3;
            // 
            // moreInfoLinkLabel
            // 
            this.moreInfoLinkLabel.AutoSize = true;
            this.moreInfoLinkLabel.Location = new System.Drawing.Point(120, 232);
            this.moreInfoLinkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.moreInfoLinkLabel.Name = "moreInfoLinkLabel";
            this.moreInfoLinkLabel.Size = new System.Drawing.Size(51, 13);
            this.moreInfoLinkLabel.TabIndex = 4;
            this.moreInfoLinkLabel.TabStop = true;
            this.moreInfoLinkLabel.Text = "More info";
            this.moreInfoLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreInfoLinkLabel_LinkClicked);
            // 
            // privateGeoEndpointTextBox
            // 
            this.privateGeoEndpointTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.privateGeoEndpointTextBox.Enabled = false;
            this.privateGeoEndpointTextBox.Location = new System.Drawing.Point(123, 161);
            this.privateGeoEndpointTextBox.Name = "privateGeoEndpointTextBox";
            this.privateGeoEndpointTextBox.Size = new System.Drawing.Size(391, 20);
            this.privateGeoEndpointTextBox.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Stamp for GeoProxy:";
            // 
            // CloudConnectionDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(526, 267);
            this.Controls.Add(this.privateGeoEndpointTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.moreInfoLinkLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.certificateComboBox);
            this.Controls.Add(this.azureResourceManagerEndpointTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.environmentTypeComboBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloudConnectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to Cloud";
            this.Load += new System.EventHandler(this.AzureStackConnectionDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox environmentTypeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox azureResourceManagerEndpointTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox certificateComboBox;
        private System.Windows.Forms.LinkLabel moreInfoLinkLabel;
        private System.Windows.Forms.TextBox privateGeoEndpointTextBox;
        private System.Windows.Forms.Label label5;
    }
}