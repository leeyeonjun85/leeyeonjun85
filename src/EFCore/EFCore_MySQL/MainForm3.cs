using EFCore_MySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace EFCore_MySQL
{
    public partial class MainForm3 : Form
    {
        private readonly ILogger _logger;
        private readonly ModelContext context;

        MySqlConnection sqlConnection;
        MySqlCommand sqlCommand;
        DataTable dataTable;

        public MainForm3(ILogger<MainForm3> logger, ModelContext _context)
        {
            InitializeComponent();
            _logger = logger;
            _logger.Log(LogLevel.Information, "프로그램이 시작되었습니다.");
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                

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
                //var addData = new Student
                //{
                //    Name = tbName.Text,
                //};

                //context.Students.Add(addData);
                //context.SaveChanges();
                //lblStatus.Text = $"상태 : 데이터 추가";
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
                //int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //Student foundStudent = context.Students.Find(foundId);

                //var updateName = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);

                //foundStudent.Name = updateName;

                //context.Entry(foundStudent).State = EntityState.Modified;
                //context.SaveChanges();
                //lblStatus.Text = $"상태 : 데이터 수정";
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
                //lblStatus.Text = $"상태 : 데이터 삭제";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}