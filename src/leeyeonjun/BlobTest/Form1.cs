using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using static System.Net.WebRequestMethods;

namespace BlobTest
{
    public partial class Form1 : Form
    {
        string ConString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "SQLiteTest.db")}";
        ContextSQLite context;
        DbConnection con;
        DbCommand cmd;

        public Form1()
        {
            InitializeComponent();

            context = new(ConString);
            context.Database.EnsureCreated();
            con = context.Database.GetDbConnection();
            cmd = con.CreateCommand();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image files(*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png | All files (*.*) | *.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.txtPath.Text = openFileDialog1.FileName;
                    pbImage.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(@txtPath.Text, FileMode.Open, FileAccess.Read);
                int FileSize = (int)fs.Length;

                byte[] rawData = new byte[FileSize];
                fs.Read(rawData, 0, FileSize);
                fs.Close();

                con.Open();
                cmd.Connection = con;

                context.blob_tbl.Add(new ModelSQLite()
                {
                    filename = txtPath.Text,
                    filesize = FileSize,
                    file = rawData
                });
                context.SaveChanges();

                MessageBox.Show("File Inserted into database successfully!",
                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string SQL;
                string FileName;
                int FileSize;
                byte[] rawData;
                FileStream fs;

                string query = "SELECT * from blob_tbl";

                con.Open();

                cmd.Connection = con;
                cmd.CommandText = query;

                DbDataReader myData = cmd.ExecuteReader();

                if (!myData.HasRows)
                    throw new Exception("There are no BLOBs to save");

                myData.Read();

                //FileSize = myData.GetUInt32(myData.GetOrdinal("filesize"));
                FileSize = Convert.ToInt32(myData["filesize"]);
                rawData = new byte[FileSize];

                myData.GetBytes(myData.GetOrdinal("file"), 0, rawData, 0, FileSize);

                FileName = @System.IO.Directory.GetCurrentDirectory() + "\\newfile.png";

                fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(rawData, 0, FileSize);
                fs.Close();

                MessageBox.Show("File successfully written to disk!",
                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                pbBlobImg.Image = Image.FromFile(FileName);

                myData.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}