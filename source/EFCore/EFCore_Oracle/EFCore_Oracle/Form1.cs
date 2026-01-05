using EFCore_Oracle.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Oracle_EFCore.Models;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace EFCore_Oracle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                if (context.Database.CanConnect())
                {
                    getOracleDataTable getOracleDT = new getOracleDataTable(dataGridView1, textBox1);
                    textBox1.Text += $"{Environment.NewLine}연결 성공{Environment.NewLine}{context.Database.GetConnectionString()}";
                }
                else
                    textBox1.Text += $"{Environment.NewLine}연결에 문제가 발생하였습니다.";
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new ModelContext())
                {
                    RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                    databaseCreator.CreateTables();
                    textBox1.Text += $"{Environment.NewLine}테이블 생성 완료";

                    var school1 = new School { Name = "서울고" };
                    context.Schools?.Add(school1);
                    context.SaveChanges();

                    var room1_1 = new Room { SchoolId = school1.Id, Name = "1반" };
                    var room1_2 = new Room { SchoolId = school1.Id, Name = "2반" };
                    context.Rooms?.AddRange(new Room[] { room1_1, room1_2 });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                textBox1.Text += $"{Environment.NewLine}에러가 발생하였습니다.{Environment.NewLine}{ex.Message}";
            }
        }

        private void btnAddOneStudent_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                var addStudent = new Student { Name = addName.Text, Birthday = DateTime.Now.Date, RoomId = 1 };
                context.Students?.Add(addStudent);
                context.SaveChanges();
                textBox1.Text += $"{Environment.NewLine}학생 추가 : {addName.Text}";
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                var sqlSelect = context.Students.Select(s => s.Name);
                var allStudents = sqlSelect.ToList();
                dataGridView1.DataSource = allStudents;
                textBox1.Text += $"{Environment.NewLine}읽어오기 완료";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                Student foundStudent = context.Students.Find(foundId);
                context.Students.Remove(foundStudent);
                context.SaveChanges();
                textBox1.Text += $"{Environment.NewLine}삭제 완료 : {foundStudent.Id} / {foundStudent.Name}";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                Student foundStudent = context.Students.Find(foundId);

                var updateName = Convert.ToString(dataGridView1.CurrentRow.Cells["name"].Value);
                var updateBirthday = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["birthday"].Value);
                var updateRoomId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["room_id"].Value);

                textBox1.Text += $"{Environment.NewLine}수정 완료 : {foundStudent.Name} -> {updateName}";
                textBox1.Text += $"{Environment.NewLine}수정 완료 : {foundStudent.Birthday} -> {updateBirthday}";
                textBox1.Text += $"{Environment.NewLine}수정 완료 : {foundStudent.RoomId} -> {updateRoomId}";

                foundStudent.Name = updateName;
                foundStudent.Birthday = updateBirthday;
                foundStudent.RoomId = updateRoomId;

                context.Entry(foundStudent).State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}