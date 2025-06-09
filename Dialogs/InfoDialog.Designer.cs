namespace Vano.Tools.Azure.Dialogs
{
    partial class InfoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoDialog));
            this.infoPanel = new System.Windows.Forms.Panel();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.doogFoodPortalLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoPanel.BackColor = System.Drawing.SystemColors.Info;
            this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoPanel.Controls.Add(this.infoTextBox);
            this.infoPanel.Controls.Add(this.pictureBox3);
            this.infoPanel.Controls.Add(this.label5);
            this.infoPanel.Location = new System.Drawing.Point(8, 8);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(963, 200);
            this.infoPanel.TabIndex = 10;
            // 
            // infoTextBox
            // 
            this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.infoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoTextBox.Location = new System.Drawing.Point(6, 29);
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.ReadOnly = true;
            this.infoTextBox.Size = new System.Drawing.Size(952, 166);
            this.infoTextBox.TabIndex = 5;
            this.infoTextBox.Text = resources.GetString("infoTextBox.Text");
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox3.Location = new System.Drawing.Point(6, 7);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(484, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Additional configuration using antarescmd pointing to Geo RDFE endpoint is needed" +
    "";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(894, 213);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 11;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // doogFoodPortalLinkLabel
            // 
            this.doogFoodPortalLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.doogFoodPortalLinkLabel.AutoSize = true;
            this.doogFoodPortalLinkLabel.Location = new System.Drawing.Point(91, 218);
            this.doogFoodPortalLinkLabel.Name = "doogFoodPortalLinkLabel";
            this.doogFoodPortalLinkLabel.Size = new System.Drawing.Size(171, 13);
            this.doogFoodPortalLinkLabel.TabIndex = 12;
            this.doogFoodPortalLinkLabel.TabStop = true;
            this.doogFoodPortalLinkLabel.Text = "https://df.onecloud.azure-test.net/";
            this.doogFoodPortalLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.doogFoodPortalLinkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Dogfood portal:";
            // 
            // InfoDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 244);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doogFoodPortalLinkLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.infoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InfoDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoDialog";
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.LinkLabel doogFoodPortalLinkLabel;
        private System.Windows.Forms.Label label1;
    }
}