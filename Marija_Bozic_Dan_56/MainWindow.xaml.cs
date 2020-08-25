using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
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

namespace Marija_Bozic_Dan_56
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSaveHtml_Click(object sender, RoutedEventArgs e)
        {
            string[] inputAddress = txtWebAddress.Text.Split('.');
            string fileName = inputAddress[1].ToString();

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(txtWebAddress.Text, string.Format(@"C:..\..\FilesHTML\{0}.html", fileName));
            }
        }

        private void btnZipFile_Click(object sender, RoutedEventArgs e)
        {
            using (ZipArchive zip = ZipFile.Open("test.zip", ZipArchiveMode.Create))
            {
                zip.CreateEntryFromFile(@"C:..\..\FilesHTML\blic.html", @"C:..\..\ZipFile\blic.html");
            }
        }
    }
}
