namespace TibiaTekPlus
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.userInterfaceHost = new System.Windows.Forms.Integration.ElementHost();
            this.main1 = new TibiaTekPlus.MainFormPresentation();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // userInterfaceHost
            // 
            this.userInterfaceHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userInterfaceHost.Location = new System.Drawing.Point(0, 0);
            this.userInterfaceHost.Name = "userInterfaceHost";
            this.userInterfaceHost.Size = new System.Drawing.Size(500, 424);
            this.userInterfaceHost.TabIndex = 2;
            this.userInterfaceHost.Text = "elementHost1";
            this.userInterfaceHost.Child = this.main1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 424);
            this.Controls.Add(this.userInterfaceHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(506, 456);
            this.MinimumSize = new System.Drawing.Size(506, 456);
            this.Name = "MainForm";
            this.Text = "TibiaTek Plus";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost userInterfaceHost;
        private MainFormPresentation main1;
        private System.Windows.Forms.Timer timer1;
    }
}

