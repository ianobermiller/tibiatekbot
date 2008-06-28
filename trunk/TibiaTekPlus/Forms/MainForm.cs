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
using Microsoft.CSharp;
using System.Text;
using System.CodeDom.Compiler;
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            menuWebBrowser.Url = new System.Uri(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"HTML\menu.htm"));
        }

        private void menuWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.Scheme.Equals("internal"))
            {
                e.Cancel = true;

                foreach (IPlugin plugin in Kernel.plugins)
                {
                    if (e.Url.PathAndQuery.Equals(plugin.GetType().ToString())) {
                        plugin.MainForm.Show();
                        break;
                    }
                }
                
            }
        }



        private void menuWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument document = menuWebBrowser.Document;
            HtmlElementCollection elements = document.GetElementsByTagName("div");
            string category = "";
            foreach (HtmlElement element in elements)
            {
                if (element.Id != null && element.Id.StartsWith("title")) {
                    category = element.Id.Substring(5);
                    foreach (IPlugin plugin in Kernel.plugins) {
                        if (plugin.Category.Equals(category))
                        {
                            HtmlElement li = document.CreateElement("li");
                            try
                            {
                                if (plugin.MainForm is Form)
                                {
                                    HtmlElement a = document.CreateElement("a");
                                    a.SetAttribute("href", String.Format("internal:{0}",plugin.GetType()));
                                    a.InnerText = plugin.Title;
                                    li.AppendChild(a);
                                }

                            } catch(NotImplementedException){
                            }
                            HtmlElement ul = document.GetElementById(String.Concat("list",category));
                            if (li.Children.Count == 0)
                            {
                                li.InnerText = plugin.Title;
                            }
                            ul.AppendChild(li);
                            HtmlElement divcontent = document.GetElementById(String.Concat("content",category));

                        }
                    }
                }
            }
             
        }

    }
}
