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
        bool priv = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void privKey_Import(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            /// dlg.Filter = "Text documents (.txt|*.txt)";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileTextBox.Text = filename;

                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                StreamReader reader = new StreamReader(filename);

                RSAParameters privParam = (RSAParameters)xs.Deserialize(reader);

                if (rsa == null)
                {
                    rsa = new RSACryptoServiceProvider(2048);
                }

                rsa.ImportParameters(privParam);
                priv = true;
            }
        }

        private void Generate_Key(object sender, RoutedEventArgs e)
        {
            rsa = new RSACryptoServiceProvider(2048);
            priv = true;


        }

        private void encrypt(object sender, RoutedEventArgs e)
        {
            if (rsa == null)
            {
                MessageBox.Show("You have to load or generate keys before you can utilize the encryption mechanism", "Error");
                return;
            }
            if (inputBox.Text == "")
            {
                MessageBox.Show("The input textbox is empty", "Error");
                return;
            }

            string input = inputBox.Text;

            var byteInput = System.Text.Encoding.Unicode.GetBytes(input);

            if (byteInput.Length > 254)
            {
                MessageBox.Show("The input is too long, you can at most encrypt 254 bytes", "Error");
                return;
            }

            var cryptBytes = rsa.Encrypt(byteInput, false);

            var resultStr = Convert.ToBase64String(cryptBytes);

            resultBox.Text = resultStr;

        }

        private void decrypt(object sender, RoutedEventArgs e)
        {
            if (rsa == null || priv == false)
            {
                MessageBox.Show("You have to load or generate keys before you can utilize the decryption mechanism", "Error");
                return;
            }
            if (inputBox.Text == "")
            {
                MessageBox.Show("The input textbox is empty", "Error");
                return;
            }

            var cryptText = inputBox.Text;

            var cryptByte = Convert.FromBase64String(cryptText);

            try
            {
                var resultByte = rsa.Decrypt(cryptByte, false);

                string resultStr = System.Text.Encoding.Unicode.GetString(resultByte);

                resultBox.Text = resultStr;
            }
            catch (CryptographicException)
            {
                MessageBox.Show("The input data could not be decrypted with the currently loaded key", "Error");
            }
        

        }

        private void export_privKey(object sender, RoutedEventArgs e)
        {

            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "rsa_priv&pubKey";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Xml documents (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string fileName = dlg.FileName;

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    var privKey = rsa.ExportParameters(true);
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                    xs.Serialize(writer, privKey);
                }

            }            

        }

        private void export_pubKey(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "rsa_pubKey";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Xml documents (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string fileName = dlg.FileName;

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    var pubKey = rsa.ExportParameters(false);
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                    xs.Serialize(writer, pubKey);
                }

            }
        }

        private void pubKey_Import(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            /// dlg.Filter = "Text documents (.txt|*.txt)";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileTextBox.Text = filename;

                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                /* using(System.Xml.XmlReader reader = System.Xml.XmlReader.Create(filename)){
                     privParam = (RSAParameters)xs.Deserialize(reader);
                 }*/
                StreamReader reader = new StreamReader(filename);

                RSAParameters privParam = (RSAParameters)xs.Deserialize(reader);

                if (rsa == null)
                {
                    rsa = new RSACryptoServiceProvider(2048);
                }

                rsa.ImportParameters(privParam);

            }
        }

        private void export_result(object sender, RoutedEventArgs e)
        {
            if (resultBox.Text == "")
            {
                MessageBox.Show("The result textbox is empty", "Error");
                return;
            }
            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "rsa_text";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string fileName = dlg.FileName;

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write(resultBox.Text);
                }

            }
        }

        private void import_text(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            /// dlg.Filter = "Text documents (.txt|*.txt)";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                StreamReader reader = new StreamReader(filename);

                inputBox.Text = reader.ReadToEnd();
            }
        }
    }
}
