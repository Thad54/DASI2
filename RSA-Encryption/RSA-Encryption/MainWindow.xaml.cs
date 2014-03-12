using System;
using System.IO;
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
using System.Security.Cryptography;

namespace RSA_Encryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        RSACryptoServiceProvider rsa = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
           /// dlg.Filter = "Text documents (.txt|*.txt)";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileTextBox.Text = filename;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            rsa = new RSACryptoServiceProvider(2048);

            var privKey = rsa.ExportParameters(true);

            var pubKey = rsa.ExportParameters(false);

            string pubKeyStr;
            string privKeyStr;

            var sw = new StringWriter();

            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

            xs.Serialize(sw, pubKey);
            pubKeyStr = sw.ToString();
  //          publicKey.Text = pubKeyStr;

            xs.Serialize(sw, privKey);
            privKeyStr = sw.ToString();
 //           privateKey.Text = privKeyStr;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (rsa == null)
            {
                MessageBox.Show("You have to load or generate keys before you can utilize the encryption mechanism", "Error");
            }


        }


    }
}
