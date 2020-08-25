using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
        private bool isValidAddress;
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
                string dateTime = DateTime.Now.ToString("ddMMyyyyhhmmss");
                try
                {
                    if (txtWebAddress.Text.StartsWith("www"))
                    {
                        client.DownloadFile(string.Format("http://{0}", txtWebAddress.Text), string.Format(@"C:..\..\FilesHTML\{0}_{1}.html", fileName, dateTime));
                        MessageBox.Show("You have successfully saved the web page as html file! ");
                    }
                    else if(txtWebAddress.Text.StartsWith("http"))
                    {
                        client.DownloadFile(txtWebAddress.Text, string.Format(@"C:..\..\FilesHTML\{0}_{1}.html", fileName, dateTime));
                        MessageBox.Show("You have successfully saved the web page as html file!");
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }                
            }
        }

        private void btnZipFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string startPath = @"C:..\..\FilesHTML\";
                string dateTime =DateTime.Now.ToString("ddMMyyyyhhmmss");
                string zipPath =string.Format(@"C:..\..\ZipFile\result{0}.zip", dateTime);
                ZipFile.CreateFromDirectory(startPath, zipPath);
                MessageBox.Show("You have successfully zipped the folder!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void IsDownloadHTMLEnabled()
        {
            if (isValidAddress)
            {
                btnSaveHtml.IsEnabled = true;
            }
            else
            {
                btnSaveHtml.IsEnabled = false;
            }
        }

        private void txtWebAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtWebAddress.Focus())
            {
                lblValidationInputWebAddress.Visibility = Visibility.Visible;
                lblValidationInputWebAddress.FontSize = 16;
                lblValidationInputWebAddress.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationInputWebAddress.Content = "Web address must be in valid form \n(http(s)://example.com or \nwww.example.com)!";
            }
           
            string patternUrl = @"^(http|http(s)?://)?([\w-]+\.)+[\w-]+[a-zA-Z0-9]+(\[\?%&=]*)?$";
            Match match = Regex.Match(txtWebAddress.Text, patternUrl, RegexOptions.IgnoreCase);
            
            if (!match.Success)
            {
                txtWebAddress.BorderBrush = new SolidColorBrush(Colors.Red);
                txtWebAddress.Foreground = new SolidColorBrush(Colors.Red);
                isValidAddress = false;
            }
            else
            {
                lblValidationInputWebAddress.Visibility = Visibility.Hidden;
                txtWebAddress.BorderBrush = new SolidColorBrush(Colors.Black);
                txtWebAddress.Foreground = new SolidColorBrush(Colors.Black);
                isValidAddress = true;
            }
            IsDownloadHTMLEnabled();
        }
    }
}

