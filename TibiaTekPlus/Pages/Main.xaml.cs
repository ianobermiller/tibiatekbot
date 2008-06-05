using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TibiaTekPlus;
using TibiaTekPlus.Pages;

namespace TibiaTekPlus
{
    

    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        PluginManager pluginManager = null;
        public SortedDictionary<string, object> treeViewHandlers = null;


        public Main()
        {
            InitializeComponent();
            treeViewHandlers = new SortedDictionary<string, object>();

            pluginManager = new PluginManager();
            treeViewHandlers["PluginManagerItem"] = (object)pluginManager;
        }

        public void MainTreeView_SelectedItemChange(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (MainTreeView.SelectedItem == null)
                return;
            TreeViewItem item = (TreeViewItem)MainTreeView.SelectedItem;
            if (!treeViewHandlers.ContainsKey(item.Name) || treeViewHandlers[item.Name] == null)
                return;
            ContentCell.Content = treeViewHandlers[item.Name];
        }
    }

    
}
