using EFCore_MySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Relational;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace EFCore_MySQL
{
    public partial class MainForm3 : Form
    {
        private readonly ILogger _logger;
        private readonly ModelContext context;

        MySqlConnection conn;
        MySqlCommand cmd;
        DataTable dt;


        string connStringn;
        string newId;

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
                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = tbxIP.Text;
                conn_string.Port = Convert.ToUInt32(tbxPort.Text);
                conn_string.UserID = tbxID.Text;
                conn_string.Password = tbxPW.Text;
                conn_string.Database = tbxDbName.Text;

                using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
                {
                    conn.Open();

                    string selectSql = $"SELECT * FROM TEST_MAUI";
                    cmd = new MySqlCommand(selectSql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    SetData(rdr);

                    conn.Clone();
                }
            }
            catch (Exception)
            {

            }
        }

        public void SetData(MySqlDataReader reader)
        {
            System.Data.DataTable table = reader.GetSchemaTable();
            dt = new System.Data.DataTable();
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

            dataGridView1.DataSource = dt;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStringn))
                {
                    conn.Open();

                    byte[] ImageData = null;
                    OpenFileDialog of = new OpenFileDialog();

                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        string FileName = of.FileName;
                        var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                        var br = new BinaryReader(fs);
                        //ImageData = br.ReadBytes((int)fs.Length);

                        ImageData = new byte[(UInt32)fs.Length];
                        fs.Read(ImageData, 0, (int)fs.Length);
                        br.Close();
                        fs.Close();
                    }

                    string CmdString = "INSERT INTO TEST_MAUI(ANIM_IDNT, ANIM_NAME, ANIM_DESC, ANIM_PICT) VALUES(?ANIM_IDNT, ?ANIM_NAME, ?ANIM_DESC, ?ANIM_PICT)";
                    cmd = new MySqlCommand(CmdString, conn);
                    newId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                    cmd.Parameters.Add(new MySqlParameter("?ANIM_IDNT", newId));
                    cmd.Parameters.Add(new MySqlParameter("?ANIM_NAME", "잠자는 치타"));
                    cmd.Parameters.Add(new MySqlParameter("?ANIM_DESC", ""));
                    cmd.Parameters.Add(new MySqlParameter("?ANIM_PICT", ImageData));

                    int RowsAffected = cmd.ExecuteNonQuery();

                    if (RowsAffected > 0)
                    {
                        string selectSql = $"SELECT * FROM TEST_MAUI";
                        cmd = new MySqlCommand(selectSql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        SetData(rdr);
                    }

                    conn.Clone();
                }




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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //var rows = dt.Rows;

            //foreach (var item in rows)
            //{

            //    var row = (DataRow)item;

            var picID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //var picNAme = (string)row.ItemArray[0];
            //var a1 = (string)row.ItemArray[1];


            using (MySqlConnection conn = new MySqlConnection(connStringn))
            {
                conn.Open();

                string selectSql = $"SELECT * FROM TEST_MAUI WHERE ANIM_IDNT = {picID}";
                cmd = new MySqlCommand(selectSql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                //SetData(rdr);


                while (rdr.Read())
                {
                    var picArray = rdr["ANIM_PICT"];
                    var picImage = new ImageConverter().ConvertFrom(picArray) as Image;

                    pictureBox1.Image = picImage;

                    //row = dt.NewRow();
                    //for (int i = 0; i < aList.Count; i++)
                    //{
                    //    row[((System.String)aList[i])] = rdr[(System.String)aList[i]];
                    //}
                    //dt.Rows.Add(row);
                }

                conn.Clone();
            }



            //if (picNAme == null) return;

            //var a6 = new ImageConverter().ConvertFrom(picNAme) as Image;

            //pictureBox1.Image = a6;



            //int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            //Student foundStudent = context.Students.Find(foundId);
            //var updateName = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);

        }
    }
}