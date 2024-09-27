using EFCore_MySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace EFCore_MySQL
{
    public partial class MainForm : Form
    {
        private readonly ILogger _logger;
        private readonly ModelContext context;

        MySqlConnection sqlConnection;
        MySqlCommand sqlCommand;
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

            //string selectSql = $"SELECT * FROM STUDENT";

            //string connString = $"Server={tbxIP}:{tbxPort};Database={tbxDbName};Uid={tbxID};Pwd={tbxPW};";

            //sqlConnection = new SqlConnection(connString);
            //sqlConnection.Open();
            //sqlCommand = new SqlCommand(selectSql, sqlConnection);

            //sqlDependency = new SqlDependency(sqlCommand);
            //adapter = new SqlDataAdapter(selectSql, sqlConnection);
            //dataTable = new DataTable();
            //adapter.Fill(dataTable);
            //dataGridView1.DataSource = dataTable;

            //context.Students.Load();
            //dataGridView1.DataSource = context.Students.Local.ToBindingList();

            //sqlCommand.ExecuteNonQuery();
            //sqlConnection.Close();
            //sqlDependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
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
            try
            {
                //string connString = $"Server={tbxIP.Text}:{tbxPort.Text};Database={tbxDbName.Text};Uid={tbxID.Text};Pwd={tbxPW.Text};";
                string connStringn = "server=112.166.89.34;port=52131;user id=uurang;password=0415673848;database=uurangdb";

                //MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                //conn_string.Server = tbxIP.Text;
                //conn_string.Port = Convert.ToUInt32(tbxPort.Text);
                //conn_string.UserID = tbxID.Text;
                //conn_string.Password = tbxPW.Text;
                //conn_string.Database = tbxDbName.Text;


                sqlConnection = new MySqlConnection(connStringn);
                sqlConnection.Open();


                string selectSql = $"SELECT * FROM TEST_MAUI";
                sqlCommand = new MySqlCommand(selectSql, sqlConnection);
                MySqlDataReader rdr = sqlCommand.ExecuteReader();

                dataTable = GetTable(rdr);
                dataGridView1.DataSource = dataTable;



                //sqlDependency = new SqlDependency(sqlCommand);
                //adapter = new SqlDataAdapter(selectSql, sqlConnection);
                //dataTable = new DataTable();


                //if (context.Database.CanConnect())
                //{
                //    lblStatus.Text = $"상태 : 연결 완료";
                //    context.Database.EnsureCreated();

                //    context.Students.Load();
                //    dataGridView1.DataSource = context.Students.Local.ToBindingList();

                //    //var query = from s in context.Schools
                //    //            select new { s.Name };
                //    //var values = query.ToList();

                //    //var values = context.Schools.Select(p => p.Name).ToList();


                //    _logger.Log(LogLevel.Information, "데이터베이스 연결");
                //}
                //else
                //{
                //    lblStatus.Text = $"상태 : 연결 실패";
                //    _logger.Log(LogLevel.Error, "데이터베이스 연결 실패");
                //}
            }
            catch (Exception)
            {

            }


            
        }

        public System.Data.DataTable GetTable(MySqlDataReader reader)
        {
            System.Data.DataTable table = reader.GetSchemaTable();
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataColumn dc;
            System.Data.DataRow row;
            System.Collections.ArrayList aList = new System.Collections.ArrayList();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dc = new System.Data.DataColumn();

                if (!dt.Columns.Contains(table.Rows[i]["ColumnName"].ToString()))
                {
                    dc.ColumnName = table.Rows[i]["ColumnName"].ToString();
                    dc.Unique = Convert.ToBoolean(table.Rows[i]["IsUnique"]);
                    dc.AllowDBNull = Convert.ToBoolean(table.Rows[i]["AllowDBNull"]);
                    dc.ReadOnly = Convert.ToBoolean(table.Rows[i]["IsReadOnly"]);
                    aList.Add(dc.ColumnName);
                    dt.Columns.Add(dc);
                }
            }

            while (reader.Read())
            {
                row = dt.NewRow();
                for (int i = 0; i < aList.Count; i++)
                {
                    row[((System.String)aList[i])] = reader[(System.String)aList[i]];
                }
                dt.Rows.Add(row);
            }
            return dt;
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