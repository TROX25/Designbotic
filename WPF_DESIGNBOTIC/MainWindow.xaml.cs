using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WPF_DESIGNBOTIC
{
    public partial class MainWindow : Window
    {
        public MainWindow(string json)
        {
            InitializeComponent();
            // przekazuje dane json do metody
            LoadJsonTree(json);
        }

        private void LoadJsonTree(string json)
        {
            // zmiana schematu json na obiekt
            var jToken = JToken.Parse(json);
            // czyszcze liste
            JsonTreeView.Items.Clear();
            // dodaje elementy do TreeView
            JsonTreeView.Items.Add(CreateTreeViewItem(jToken));
            
        }
        // sposob dodania
        private TreeViewItem CreateTreeViewItem(JToken token)
        {
            // pusto
            if (token == null)
            {
                return null;
            }
            TreeViewItem item = new TreeViewItem();

            if (token.Type == JTokenType.Object)
            {
                item.Header = "Obiekt";
                foreach (var prop in token.Children<JProperty>())
                {
                    var childItem = new TreeViewItem { Header = prop.Name };
                    childItem.Items.Add(CreateTreeViewItem(prop.Value));
                    item.Items.Add(childItem);
                }
            }
            // Grupowanie
            else if (token.Type == JTokenType.Array)
            {
                item.Header = "Tablica";
                int index = 0;
                foreach (var child in token.Children())
                {
                    var childItem = CreateTreeViewItem(child);
                    childItem.Header = $"[{index}]";
                    item.Items.Add(childItem);
                    index++;
                }
            }
            else
            {
                item.Header = token.ToString();
            }

            return item;
        }
    }
}
