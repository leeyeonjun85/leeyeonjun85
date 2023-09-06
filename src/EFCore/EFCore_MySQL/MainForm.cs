using EFCore_MySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
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

            string selectSql = $"SELECT * FROM STUDENT";

            sqlConnection = new SqlConnection("Server=localhost;Database=testdb;Uid=root;Pwd=0316165110;");
            sqlConnection.Open();
            sqlCommand = new SqlCommand(selectSql, sqlConnection);

            sqlDependency = new SqlDependency(sqlCommand);
            adapter = new SqlDataAdapter(selectSql, sqlConnection);
            dataTable = new DataTable();
            //adapter.Fill(dataTable);
            //dataGridView1.DataSource = dataTable;

            //context.Students.Load();
            //dataGridView1.DataSource = context.Students.Local.ToBindingList();

            //sqlCommand.ExecuteNonQuery();
            //sqlConnection.Close();
            sqlDependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
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

                context.Students.Load();
                dataGridView1.DataSource = context.Students.Local.ToBindingList();

                //var query = from s in context.Schools
                //            select new { s.Name };
                //var values = query.ToList();

                //var values = context.Schools.Select(p => p.Name).ToList();


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
                var addData = new Student
                {
                    Name = tbName.Text,
                };

                context.Students.Add(addData);
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
                Student foundStudent = context.Students.Find(foundId);

                var updateName = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);

                foundStudent.Name = updateName;

                context.Entry(foundStudent).State = EntityState.Modified;
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
                int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Student foundStudent = context.Students.Find(foundId);
                context.Students.Remove(foundStudent);
                context.SaveChanges();
                lblStatus.Text = $"���� : ������ ����";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}