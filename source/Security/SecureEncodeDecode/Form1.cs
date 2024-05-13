using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SecureEncodeDecode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHMAC_Click(object sender, EventArgs e)
        {
            string hashedBase64 = GetHMAC(tbxKey.Text, tbxMessage.Text);

            //Type Log
            string addString = string.Empty;
            addString += $"■ Hash : Hash-based Message Authentication Code{Environment.NewLine}";
            addString += $"■ Key : {tbxKey.Text}{Environment.NewLine}";
            addString += $"■ Message : {tbxMessage.Text}{Environment.NewLine}";
            addString += $"■ Hashed : {hashedBase64}{Environment.NewLine}";
            AddLineToTextBox(tbxLog, addString);
        }

        private void btnSHA256_Click(object sender, EventArgs e)
        {
            string hashedBase64 = GetSHA256(tbxMessage.Text);

            //Type Log
            string addString = string.Empty;
            addString += $"■ Hash : Secure Hashing Algorithm 256{Environment.NewLine}";
            addString += $"■ Message : {tbxMessage.Text}{Environment.NewLine}";
            addString += $"■ Hashed : {hashedBase64}{Environment.NewLine}";
            AddLineToTextBox(tbxLog, addString);
        }

        private void btnSHA384_Click(object sender, EventArgs e)
        {
            string hashedBase64 = GetSHA384(tbxMessage.Text);

            //Type Log
            string addString = string.Empty;
            addString += $"■ Hash : Secure Hashing Algorithm 384{Environment.NewLine}";
            addString += $"■ Message : {tbxMessage.Text}{Environment.NewLine}";
            addString += $"■ Hashed : {hashedBase64}{Environment.NewLine}";
            AddLineToTextBox(tbxLog, addString);
        }

        private void btnSHA512_Click(object sender, EventArgs e)
        {
            string hashedBase64 = GetSHA512(tbxMessage.Text);

            //Type Log
            string addString = string.Empty;
            addString += $"■ Hash : Secure Hashing Algorithm 512{Environment.NewLine}";
            addString += $"■ Message : {tbxMessage.Text}{Environment.NewLine}";
            addString += $"■ Hashed : {hashedBase64}{Environment.NewLine}";
            AddLineToTextBox(tbxLog, addString);
        }

        private void btnMD5_Click(object sender, EventArgs e)
        {
            string hashedBase64 = GetMD5(tbxMessage.Text);

            //Type Log
            string addString = string.Empty;
            addString += $"■ Hash : Message-Digest algorithm 5{Environment.NewLine}";
            addString += $"■ Message : {tbxMessage.Text}{Environment.NewLine}";
            addString += $"■ Hashed : {hashedBase64}{Environment.NewLine}";
            AddLineToTextBox(tbxLog, addString);
        }

        private string GetHMAC(string key, string message)
        {
            //Hashing
            byte[] msg_buffer = new ASCIIEncoding().GetBytes(message);
            //MemoryStream msg_buffer = new MemoryStream(new ASCIIEncoding().GetBytes(message));
            byte[] key_buffer = new ASCIIEncoding().GetBytes(key);
            //var hashFunction = new HMACMD5(key_buffer);           // 128bit
            //var hashFunction = new HMACSHA1(key_buffer);          // 160bit
            //var hashFunction = new HMACSHA256(key_buffer);        // 256bit
            //var hashFunction = new HMACSHA384(key_buffer);        // 384bit
            var hashFunction = new HMACSHA512(key_buffer);         // 512bit
            byte[] hashed_buffer = hashFunction.ComputeHash(msg_buffer);
            
            string hashed_Base64 = Convert.ToBase64String(hashed_buffer);

            return $"{hashed_Base64}({hashed_buffer.Length})";
            //return hashed_Base64;
        }

        private string GetSHA256(string message)
        {
            //Hashing
            byte[] msg_buffer = new ASCIIEncoding().GetBytes(message);
            SHA256 hashFunction = SHA256.Create();
            //byte[] hashed_buffer = hashFunction.ComputeHash(msg_buffer);
            byte[] hashed_buffer = SHA256.HashData(msg_buffer);
            string hashed_Base64 = Convert.ToBase64String(hashed_buffer);
            //string hashed_Base64 = Convert.ToHexString(hashed_buffer);

            return $"{hashed_Base64}({hashed_buffer.Length})";
            //return hashed_Base64;
        }

        private string GetSHA384(string message)
        {
            //Hashing
            byte[] msg_buffer = new ASCIIEncoding().GetBytes(message);
            SHA384 hashFunction = SHA384.Create();
            byte[] hashed_buffer = hashFunction.ComputeHash(msg_buffer);
            string hashed_Base64 = Convert.ToBase64String(hashed_buffer);

            return $"{hashed_Base64}({hashed_buffer.Length})";
            //return hashed_Base64;
        }

        private string GetSHA512(string message)
        {
            //Hashing
            byte[] msg_buffer = new ASCIIEncoding().GetBytes(message);
            SHA512 hashFunction = SHA512.Create();
            byte[] hashed_buffer = hashFunction.ComputeHash(msg_buffer);
            string hashed_Base64 = Convert.ToBase64String(hashed_buffer);

            return $"{hashed_Base64}({hashed_buffer.Length})";
            //return hashed_Base64;
        }

        private string GetMD5(string message)
        {
            //Hashing
            byte[] msg_buffer = new ASCIIEncoding().GetBytes(message);
            MD5 hashFunction = MD5.Create();
            byte[] hashed_buffer = hashFunction.ComputeHash(msg_buffer);
            string hashed_Base64 = Convert.ToBase64String(hashed_buffer);

            return $"{hashed_Base64}({hashed_buffer.Length})";
            //return hashed_Base64;
        }


        public bool IsSameHash(string text, string oldHash, HashType type)
        {
            string newHash = null;
            switch (type)
            {
                case HashType.MD5:
                    newHash = GetMD5(text);
                    break;
                case HashType.SHA256:
                    newHash = GetSHA256(text);
                    break;
                case HashType.SHA384:
                    newHash = GetSHA384(text);
                    break;
                case HashType.SHA512:
                    newHash = GetSHA512(text);
                    break;
                default:
                    return false;
            }
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(newHash, oldHash) == 0;
        }





        private void AddLineToTextBox(TextBox textBox, string addText)
        {
            try
            {
                addText += Environment.NewLine;
                textBox.Text = $"{addText}{textBox.Text}";
                Application.DoEvents();
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"에러발생{Environment.NewLine}{e}",
                    "에러발생",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                throw;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            tbxMessage.Text = $"Test Message {DateTime.Now}";
        }

        public enum HashType { MD5, SHA256, SHA384, SHA512 }
    }
}
