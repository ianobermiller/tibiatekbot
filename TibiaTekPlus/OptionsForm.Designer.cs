namespace TibiaTekPlus
{
    partial class OptionsForm
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
            this.optionsWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // optionsWebBrowser
            // 
            this.optionsWebBrowser.AllowNavigation = false;
            this.optionsWebBrowser.AllowWebBrowserDrop = false;
            this.optionsWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsWebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.optionsWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.optionsWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.optionsWebBrowser.Name = "optionsWebBrowser";
            this.optionsWebBrowser.ScriptErrorsSuppressed = true;
            this.optionsWebBrowser.Size = new System.Drawing.Size(292, 266);
            this.optionsWebBrowser.TabIndex = 0;
            this.optionsWebBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.optionsWebBrowser);
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser optionsWebBrowser;

    }
}