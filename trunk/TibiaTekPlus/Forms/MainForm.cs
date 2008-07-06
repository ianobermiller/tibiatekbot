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
            menuWebBrowser.Url = new System.Uri(System.IO.Path.Combine(Kernel.Skin.Path, @"menu.htm"));
        }

        private void menuWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.Scheme.Equals("internal"))
            {
                e.Cancel = true;
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(e.Url.Query);
                switch (e.Url.AbsolutePath.ToLower()){
                    case "":
                        {
                            string name = nvc.GetValues("name")[0];
                            string action = nvc.GetValues("action")[0];
                            switch(name){
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
                                    switch(action){
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
                    element.Children[0].Click += new HtmlElementEventHandler(evthdr);
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
                            a.MouseLeave += new HtmlElementEventHandler(a_MouseLeave);
                            try
                            {
                                if (plugin.MainForm is Form) a.SetAttribute("href", String.Format("internal:plugins?type={0}&action=show",plugin.GetType()));
                            } catch(NotImplementedException){ }
                            
                            HtmlElement divlist = document.GetElementById(String.Concat("list",category));
                            HtmlElement divtitle = document.GetElementById(String.Concat("title", category));

                            //divlist.Style = "display: block;";
                            divtitle.Style = "display: block;";
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

        void a_MouseLeave(object sender, HtmlElementEventArgs e)
        {
            statusBarLabel.Text = Language.statusBarLabelText_Ready;
        }

        void a_MouseEnter(object sender, HtmlElementEventArgs e)
        {
            try
            {
                statusBarLabel.Text = ((HtmlElement)sender).GetAttribute("title");
            }catch(Exception){
            }
        }

        private void evthdr(object sender, HtmlElementEventArgs  args)
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

        private void optionsSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void skinsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            string[] directories = Directory.GetDirectories("Skins");
            string output = "";
            foreach (string dir in directories)
            {
                output += dir + "\n";
            }
            MessageBox.Show(output);
            //ToolStripMenuItem 
        }


    }
}
