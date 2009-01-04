using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using TibiaTekPlus.Plugins;
using System.Text;
using System.Text.RegularExpressions;

namespace TibiaTekPlus
{
    public partial class MainForm : Form
    {

        private Kernel Kernel
        {
            get
            {
                return global::TibiaTekPlus.Program.kernel;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }


        private void plugInManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kernel.pluginsForm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kernel.aboutForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load menu.htm
            menuWebBrowser.Navigate(System.IO.Path.Combine(Kernel.Skin.Path, @"menu.htm"));


            // Populate Skins dropdown menu
            skinsToolStripMenuItem.DropDownItems.Clear();
            foreach (Skin s in Kernel.Skins)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(s.Name);

                item.ToolTipText = s.Description;

                if (Kernel.Skin.Name.Equals(s.Name))
                {
                    item.Font = new Font(item.Font, FontStyle.Bold);
                    item.Checked = true;
                }
                else
                {
                    item.Click += new EventHandler(item_Click);
                }
                skinsToolStripMenuItem.DropDownItems.Add(item);
            }

            // Load main menu position
            switch (Settings.Default.MainMenuPosition)
            {
                case "BottomPanel":
                    Panels.BottomToolStripPanel.Controls.Add(this.mainMenu);
                    break;
                case "LeftPanel":
                    Panels.LeftToolStripPanel.Controls.Add(this.mainMenu);
                    break;
                case "RightPanel":
                    Panels.RightToolStripPanel.Controls.Add(this.mainMenu);
                    break;
            }
            
            // Load location
            this.Location = Settings.Default.MainFormLocation;

            // Load size
            this.Size = Settings.Default.MainFormSize;

        }

        private void menuWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.Scheme.Equals("internal"))
            {
                e.Cancel = true;
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(e.Url.Query);
                switch (e.Url.AbsolutePath.ToLower())
                {
                    case "":
                        {
                            string name = nvc.GetValues("name")[0];
                            string action = nvc.GetValues("action")[0];
                            switch (name)
                            {
                                case "pluginManager":
                                    {
                                        switch (action)
                                        {
                                            case "show":
                                                {
                                                    Kernel.pluginsForm.Show();
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "plugins":
                        {
                            string type = nvc.GetValues("type")[0];
                            string action = nvc.GetValues("action")[0];
                            foreach (IPlugin plugin in Kernel.plugins)
                            {
                                if (plugin.GetType().ToString().Equals(type))
                                {
                                    switch (action)
                                    {
                                        case "show":
                                            {
                                                plugin.MainForm.Show();
                                            }
                                            break;
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void menuWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument document = menuWebBrowser.Document;
            HtmlElementCollection elements = document.GetElementById("menuz").Children;
            string category = "";
            
            foreach (HtmlElement element in elements)
            {
                if (element.Id != null && element.Id.StartsWith("title")) {
                    element.Children[0].Click += new HtmlElementEventHandler(ExpandCategory);
                    element.Children[0].MouseEnter += new HtmlElementEventHandler(element_MouseEnter);
                    element.Children[0].MouseLeave += new HtmlElementEventHandler(element_MouseLeave);
                    category = element.Id.Substring(5);

                    foreach (IPlugin plugin in Kernel.plugins) {
                        if (plugin.Category.Equals(category))
                        {
                            HtmlElement a = document.CreateElement("a");
                            a.InnerText = plugin.Title;
                            a.SetAttribute("className", "item");
                            a.SetAttribute("title", plugin.Description);
                            a.MouseEnter += new HtmlElementEventHandler(a_MouseEnter);
                            a.MouseLeave += new HtmlElementEventHandler(element_MouseLeave);
                            try
                            {
                                if (plugin.MainForm is Form) a.SetAttribute("href", String.Format("internal:plugins?type={0}&action=show",plugin.GetType()));
                            } catch(NotImplementedException){ }
                            
                            HtmlElement divlist = document.GetElementById(String.Concat("list",category));
                            HtmlElement divtitle = document.GetElementById(String.Concat("title", category));

                            divlist.Style = "display: block;";
                            divtitle.Style = "display: block;";
                            divtitle.Children[0].SetAttribute("className", "category_expand");
                            divlist.AppendChild(a);
                        }
                    }
                }
            }
        }

        void element_MouseLeave(object sender, HtmlElementEventArgs e)
        {
            statusBarLabel.Text = Language.statusBarLabelText_Ready;
        }

        void element_MouseEnter(object sender, HtmlElementEventArgs e)
        {

            if (((HtmlElement)sender).GetAttribute("className").Equals("category_expand"))
            {
                statusBarLabel.Text = Language.menuWebBrowser_Collapse;
            }
            else
            {
                statusBarLabel.Text = Language.menuWebBrowser_Expand;
            }
        }

        void a_MouseEnter(object sender, HtmlElementEventArgs e)
        {
            statusBarLabel.Text = ((HtmlElement)sender).GetAttribute("title");
        }

        private void ExpandCategory(object sender, HtmlElementEventArgs  args)
        {
            HtmlElement elem = (HtmlElement)sender;
            string category = elem.Parent.Id.Substring(5);
            HtmlElement list = elem.Document.GetElementById("list" + category);
            
            if (elem.GetAttribute("className").Equals("category_expand"))
            {
                list.Style = "display: none;";
                elem.SetAttribute("className", "category_compact");
            }
            else
            {
                list.Style = "display: block;";
                elem.SetAttribute("className", "category_expand");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(Language.mainForm_FormClosing, Language.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (MessageBox.Show(String.Format(Language.mainForm_skinSelected, item.Text), Language.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                Settings.Default.Skin = item.Text;
                Application.Restart();
                Application.ExitThread();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.MainMenuPosition = this.mainMenu.Parent.Tag.ToString();
            Settings.Default.MainFormLocation = this.Location;
            Settings.Default.MainFormSize = this.Size;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
