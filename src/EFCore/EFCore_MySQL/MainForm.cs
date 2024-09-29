using EFCore_MySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Pkcs;
using Serilog.Sinks.File;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace EFCore_MySQL
{
    public partial class MainForm : Form
    {
        private readonly ILogger _logger;
        private readonly ModelContext context;

        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataAdapter adapter;
        SqlDependency sqlDependency;
        DataTable dataTable;

        public MainForm(ILogger<MainForm> logger, ModelContext _context)
        {
            InitializeComponent();
            _logger = logger;
            _logger.Log(LogLevel.Information, "���α׷��� ���۵Ǿ����ϴ�.");
            context = _context;
            //context.ChangeTracker.DetectChanges();

            //string selectSql = $"SELECT * FROM STUDENT";

            //conn = new SqlConnection("Server=localhost;Database=testdb;Uid=root;Pwd=0316165110;");
            //conn.Open();
            //cmd = new SqlCommand(selectSql, conn);

            //sqlDependency = new SqlDependency(cmd);
            //adapter = new SqlDataAdapter(selectSql, conn);
            //dataTable = new DataTable();
            ////adapter.Fill(dataTable);
            ////dataGridView1.DataSource = dataTable;

            ////context.Students.Load();
            ////dataGridView1.DataSource = context.Students.Local.ToBindingList();

            ////cmd.ExecuteNonQuery();
            ////conn.Close();
            //sqlDependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
        }

        private void OnDependencyChange(object sender, SqlNotificationEventArgs args)
        {
            lblStatus.Text = "Notification �̺�Ʈ�� �߻��Ͽ����ϴ�.";

            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            //context.Students.Load();
            //dataGridView1.DataSource = context.Students.Local.ToBindingList();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (context.Database.CanConnect())
            {
                lblStatus.Text = $"���� : ���� �Ϸ�";
                context.Database.EnsureCreated();

                context.TEST_MAUI.Load();
                //dataGridView1.DataSource = context.TEST_MAUI.Local.ToBindingList();

                var query = from s in context.TEST_MAUI
                            select new { s.Id, s.ANIM_NAME, s.ANIM_DESC };
                var values = query.ToList();

                dataGridView1.DataSource = values;

                //var query = from s in context.TEST_MAUI
                //            select new { s.Name };
                //var values = query.ToList();

                //var values = context.TEST_MAUI.Select(p => p.Name).ToList();

                //var values = context.TEST_MAUI.ToList();


                _logger.Log(LogLevel.Information, "�����ͺ��̽� ����");
            }
            else
            {
                lblStatus.Text = $"���� : ���� ����";
                _logger.Log(LogLevel.Error, "�����ͺ��̽� ���� ����");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] ImageData = null;
                OpenFileDialog of = new OpenFileDialog();

                if (of.ShowDialog() == DialogResult.OK)
                {
                    string FileName = of.FileName;
                    var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                    //var br = new BinaryReader(fs);
                    //ImageData = br.ReadBytes((int)fs.Length);

                    ImageData = new byte[(UInt32)fs.Length];
                    fs.Read(ImageData, 0, (int)fs.Length);
                    //br.Close();
                    fs.Close();
                }

                var addData = new TEST_MAUI
                {
                    ANIM_NAME = "���� ���� ġŸ",
                    ANIM_DESC = "",
                    ANIM_PICT = ImageData
                };

                context.TEST_MAUI.Add(addData);
                context.SaveChanges();
                lblStatus.Text = $"���� : ������ �߰�";
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ �߻��Ͽ����ϴ�." + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                var foundItem = context.TEST_MAUI.Find(foundId);
                foundItem.ANIM_DESC = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                context.Entry(foundItem).State = EntityState.Modified;
                context.SaveChanges();
                lblStatus.Text = $"���� : ������ ����";
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //Student foundStudent = context.Students.Find(foundId);
                //context.Students.Remove(foundStudent);
                //context.SaveChanges();
                //lblStatus.Text = $"���� : ������ ����";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            var foundItem = context.TEST_MAUI.Find(foundId);
            var a1 = foundItem.ANIM_PICT;
        }
    }
}