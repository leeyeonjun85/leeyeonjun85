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
            _logger.Log(LogLevel.Information, "프로그램이 시작되었습니다.");
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
            lblStatus.Text = "Notification 이벤트가 발생하였습니다.";

            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            //context.Students.Load();
            //dataGridView1.DataSource = context.Students.Local.ToBindingList();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (context.Database.CanConnect())
            {
                lblStatus.Text = $"상태 : 연결 완료";
                context.Database.EnsureCreated();

                context.Students.Load();
                dataGridView1.DataSource = context.Students.Local.ToBindingList();

                //var query = from s in context.Schools
                //            select new { s.Name };
                //var values = query.ToList();

                //var values = context.Schools.Select(p => p.Name).ToList();


                _logger.Log(LogLevel.Information, "데이터베이스 연결");
            }
            else
            {
                lblStatus.Text = $"상태 : 연결 실패";
                _logger.Log(LogLevel.Error, "데이터베이스 연결 실패");
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
                lblStatus.Text = $"상태 : 데이터 추가";
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러가 발생하였습니다." + ex.Message);
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
                lblStatus.Text = $"상태 : 데이터 수정";
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
                lblStatus.Text = $"상태 : 데이터 삭제";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}