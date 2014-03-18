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

        private void privKey_Import(object sender, RoutedEventArgs e)
        {

            //open a windows file browser dialogue to seelct the key file
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            /// dlg.Filter = "Text documents (.txt|*.txt)";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileTextBox.Text = filename;

                // 
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                StreamReader reader = new StreamReader(filename);

                try
                {
                    // deserialize the xml to get the key data
                    RSAParameters privParam = (RSAParameters)xs.Deserialize(reader);

                    //initialize the rsa object 
                    if (rsa == null)
                    {
                    //the key length is only 1 since it will immedaitly be overwritten
                        rsa = new RSACryptoServiceProvider(1);
                    }

                    //import the key
                    rsa.ImportParameters(privParam);
                }
                //the ke could not be imported
                catch (CryptographicException)
                {
                    MessageBox.Show("The imported file is not a valid RSA key.", "Error");
                    rsa = null;
                }
                //the selected file was invalid
                catch (InvalidOperationException)
                {
                    MessageBox.Show("The imported file is not a valid RSA key.", "Error");
                    rsa = null;
                }

            }
        }

        private void Generate_Key(object sender, RoutedEventArgs e)
        {
            //initialize the rsa object and generate a 2048 bit keypair
            try
            {
                rsa = new RSACryptoServiceProvider(2048);
            }
            //the object could not be created
            catch (CryptographicException)
            {
                MessageBox.Show("There was an error during the key generation. Please try again", "Error");
                rsa = null;
            }


        }

        private void encrypt(object sender, RoutedEventArgs e)
        {
            //check if the rsa object exists
            if (rsa == null)
            {
                MessageBox.Show("You have to load or generate keys before you can utilize the encryption mechanism", "Error");
                return;
            }
            //check if there is text to be encrypted
            if (inputBox.Text == "")
            {
                MessageBox.Show("The input textbox is empty", "Error");
                return;
            }

            string input = inputBox.Text;

            //convert the string input into a byte array
            var byteInput = System.Text.Encoding.Unicode.GetBytes(input);

            //check if the text exceeds the data limit
            if (byteInput.Length > 245)
            {
                MessageBox.Show("The input is too long, you can at most encrypt 245 bytes", "Error");
                return;
            }
            try
            {
                //encrypt the text
                var cryptBytes = rsa.Encrypt(byteInput, false);
                //convert the resulting byte array to a base 64 string representation
                var resultStr = Convert.ToBase64String(cryptBytes);

                resultBox.Text = resultStr;
 
            }
            catch (CryptographicException)
            {
                MessageBox.Show("The input data could not be encrypted with the currently loaded key", "Error");
            }

            

        }

        private void decrypt(object sender, RoutedEventArgs e)
        {
            //check if the rsa object exists
            if (rsa == null)
            {
                MessageBox.Show("You have to load or generate keys before you can utilize the decryption mechanism", "Error");
                return;
            }
            //check if there is text to be decrypted
            if (inputBox.Text == "")
            {
                MessageBox.Show("The input textbox is empty", "Error");
                return;
            }

            var cryptText = inputBox.Text;

            //convert the string input into a byte array
            var cryptByte = Convert.FromBase64String(cryptText);

            try
            {
                //decrypt the data
                var resultByte = rsa.Decrypt(cryptByte, false);

                //convert the result into a unicode string representation
                string resultStr = System.Text.Encoding.Unicode.GetString(resultByte);

                resultBox.Text = resultStr;
            }
            // decryption error
            catch (CryptographicException)
            {
                MessageBox.Show("The input data could not be decrypted with the currently loaded key", "Error");
            }
        

        }

        private void export_privKey(object sender, RoutedEventArgs e)
        {
            //check if an rsa obejct exists
            if (rsa == null)
            {
                MessageBox.Show("You have to load or generate keys before you can export them", "Error");
                return;
            }
            //open a save file dialog to select the save location and filename
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
                    //export the private + public key and serialize them into an xml format
                    var privKey = rsa.ExportParameters(true);
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                    xs.Serialize(writer, privKey);
                }

            }            

        }

        private void export_pubKey(object sender, RoutedEventArgs e)
        {
            //check if an rsa obejct exists
            if (rsa == null)
            {
                MessageBox.Show("You have to load or generate keys before you can export them", "Error");
                return;
            }

            //open a save file dialog to select the save location and filename
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
                    //export the public key and serialize them into an xml format
                    var pubKey = rsa.ExportParameters(false);
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                    xs.Serialize(writer, pubKey);
                }

            }
        }

        private void export_result(object sender, RoutedEventArgs e)
        {
            //check if the resut textbox is empty
            if (resultBox.Text == "")
            {
                MessageBox.Show("The result textbox is empty", "Error");
                return;
            }

            //open a save file dialog to select the save location and filename
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

            //open a windows file browser dialogue to seelct the input text file
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
