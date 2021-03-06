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
using System.Xml;
using System.IO;

namespace RiivToISO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal const int RootLevel = 0;
        internal const char Tab = '\t';
        public MainWindow()
        {
            InitializeComponent();
        }

        private void onStartButtonClick(object sender, RoutedEventArgs e)
        {
            //Grab the element
            TextBox riiBox = (TextBox)this.FindName("riiFolderPathTextBox");
            TextBox isoBox = (TextBox)this.FindName("isoFolderPathTextBox");

            //Grab text
            string riiPath = riiBox.Text;
            string isoPath = isoBox.Text;

            if (String.IsNullOrEmpty(riiPath)/* || String.IsNullOrEmpty(isoPath)*/) return;

            string riivXml = File.ReadAllText(riiPath + "\\riivolution\\Deluxe_USA.xml");

            XmlDocument riivDoc = new XmlDocument();
            riivDoc.LoadXml(riivXml);

            //Console.WriteLine(riivDoc.OuterXml);
            Console.WriteLine(riivDoc.FirstChild.Name);
            XmlNode wiidisk = riivDoc.FirstChild;
            List<List<string>> nodesForPatches = new List<List<string>>();
            //Somehow navigate the dom and grab the proper folder definitions and file paths
            if (wiidisk.HasChildNodes)
            {
                for (int i = 0; i < wiidisk.ChildNodes.Count; i++)
                {
                    Console.WriteLine(wiidisk.ChildNodes[i].Name + " : " + wiidisk.ChildNodes[i].InnerXml);
                    List<string> list = wiidisk.ChildNodes[i].InnerXml.Split('<').ToList();
                    for(int j = 0; j < list.Count; j++)
                    {
                        list[j] = list[j].Replace("/>", string.Empty);                    }
                    nodesForPatches.Add(list);
                }
                
                foreach(List<string> node in nodesForPatches)
                {
                    foreach(string s in node){
                        if(string.IsNullOrEmpty(s)) continue;
                        Console.WriteLine(s + " : ");
                    }
                }
            }

        }
    }
}
