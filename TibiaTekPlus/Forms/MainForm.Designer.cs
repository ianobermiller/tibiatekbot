using System.IO;

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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plugInManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.skinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWebBrowser = new System.Windows.Forms.WebBrowser();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panels = new System.Windows.Forms.ToolStripContainer();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.Panels.ContentPanel.SuspendLayout();
            this.Panels.TopToolStripPanel.SuspendLayout();
            this.Panels.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.AllowItemReorder = true;
            this.mainMenu.AllowMerge = false;
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainMenu.ShowItemToolTips = true;
            this.mainMenu.Size = new System.Drawing.Size(194, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plugInManagerToolStripMenuItem,
            this.toolStripMenuItem2,
            this.skinsToolStripMenuItem,
            this.optionsSettingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // plugInManagerToolStripMenuItem
            // 
            this.plugInManagerToolStripMenuItem.Name = "plugInManagerToolStripMenuItem";
            this.plugInManagerToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.plugInManagerToolStripMenuItem.Text = "&Plug-In Manager";
            this.plugInManagerToolStripMenuItem.Click += new System.EventHandler(this.plugInManagerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 6);
            // 
            // skinsToolStripMenuItem
            // 
            this.skinsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem});
            this.skinsToolStripMenuItem.Name = "skinsToolStripMenuItem";
            this.skinsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.skinsToolStripMenuItem.Text = "&Skins";
            // 
            // emptyToolStripMenuItem
            // 
            this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
            this.emptyToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.emptyToolStripMenuItem.Text = "Empty";
            // 
            // optionsSettingsToolStripMenuItem
            // 
            this.optionsSettingsToolStripMenuItem.Name = "optionsSettingsToolStripMenuItem";
            this.optionsSettingsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.optionsSettingsToolStripMenuItem.Text = "&Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersGuideToolStripMenuItem,
            this.documentationToolStripMenuItem,
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // usersGuideToolStripMenuItem
            // 
            this.usersGuideToolStripMenuItem.Name = "usersGuideToolStripMenuItem";
            this.usersGuideToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.usersGuideToolStripMenuItem.Text = "&User\'s Guide";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.documentationToolStripMenuItem.Text = "&Documentation";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&About TibiaTek Plus";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuWebBrowser
            // 
            this.menuWebBrowser.AllowWebBrowserDrop = false;
            this.menuWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuWebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.menuWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.menuWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.menuWebBrowser.Name = "menuWebBrowser";
            this.menuWebBrowser.Size = new System.Drawing.Size(194, 292);
            this.menuWebBrowser.TabIndex = 3;
            this.menuWebBrowser.WebBrowserShortcutsEnabled = false;
            this.menuWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.menuWebBrowser_Navigating);
            this.menuWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.menuWebBrowser_DocumentCompleted);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 316);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(194, 22);
            this.statusBar.TabIndex = 4;
            this.statusBar.Text = "statusBar";
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(42, 17);
            this.statusBarLabel.Text = global::TibiaTekPlus.Language.statusBarLabelText_Ready;
            // 
            // Panels
            // 
            // 
            // Panels.BottomToolStripPanel
            // 
            this.Panels.BottomToolStripPanel.Tag = "BottomPanel";
            // 
            // Panels.ContentPanel
            // 
            this.Panels.ContentPanel.Controls.Add(this.menuWebBrowser);
            this.Panels.ContentPanel.Size = new System.Drawing.Size(194, 292);
            this.Panels.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // Panels.LeftToolStripPanel
            // 
            this.Panels.LeftToolStripPanel.Tag = "LeftPanel";
            this.Panels.Location = new System.Drawing.Point(0, 0);
            this.Panels.Name = "Panels";
            // 
            // Panels.RightToolStripPanel
            // 
            this.Panels.RightToolStripPanel.Tag = "RightPanel";
            this.Panels.Size = new System.Drawing.Size(194, 316);
            this.Panels.TabIndex = 5;
            this.Panels.Text = "toolStripContainer1";
            // 
            // Panels.TopToolStripPanel
            // 
            this.Panels.TopToolStripPanel.Controls.Add(this.mainMenu);
            this.Panels.TopToolStripPanel.Tag = "TopPanel";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 338);
            this.Controls.Add(this.Panels);
            this.Controls.Add(this.statusBar);
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(214, 2000);
            this.MinimumSize = new System.Drawing.Size(210, 36);
            this.Name = "MainForm";
            this.Text = "TibiaTek Plus";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.Panels.ContentPanel.ResumeLayout(false);
            this.Panels.TopToolStripPanel.ResumeLayout(false);
            this.Panels.TopToolStripPanel.PerformLayout();
            this.Panels.ResumeLayout(false);
            this.Panels.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.WebBrowser menuWebBrowser;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.ToolStripContainer Panels;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plugInManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem skinsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsSettingsToolStripMenuItem;

    }
}

