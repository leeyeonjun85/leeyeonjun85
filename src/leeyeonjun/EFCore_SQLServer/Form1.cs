using EFCore_SQLServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System;
using System.Xml.Linq;
using leeyeonjun;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Forms;

namespace EFCore_SQLServer
{
    public partial class Form1 : Form
    {
        SQLServerContext _context;

        public Form1()
        {
            InitializeComponent();
            string a1 = Util.GetJsonData("myName");
            Util.connectionString = Util.GetJsonData("myDataBase", "SqlServer", "ConnectionString");
            _context = new SQLServerContext(Util.connectionString);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_context.Database.CanConnect())
            {
                lbStatus.Text = $"상태 : 연결 완료";
                _context.Database.EnsureCreated();

                _context.Pizzas.Load();
                _context.Sauces.Load();
                _context.Toppings.Load();
                _context.PizzaTopping.Load();
                dataGridView1.DataSource = _context.Pizzas.Local.ToBindingList();
                dataGridView2.DataSource = _context.Sauces.Local.ToBindingList();
                dataGridView3.DataSource = _context.Toppings.Local.ToBindingList();
                dataGridView4.DataSource = _context.PizzaTopping.Local.ToBindingList();
            }
            else
            {
                _context.Database.EnsureCreated();
                lbStatus.Text = $"상태 : 연결 실패";
            }
        }

        private void btnAddPizza_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxName.Text.IsNullOrEmpty())
                    throw new Exception("이름을 입력하세요");

                Sauce selectedSauce = (Sauce)dataGridView2.SelectedRows[0].DataBoundItem;
                List<Topping> addToppings = new List<Topping>();

                foreach (DataGridViewRow item in dataGridView3.SelectedRows)
                {
                    addToppings.Add((Topping)item.DataBoundItem);
                }
                
                var addData = new Pizza
                {
                    Name = tbxName.Text,
                    Sauce = selectedSauce,
                    Toppings = addToppings
                };

                _context.Pizzas.Add(addData);
                _context.SaveChanges();
                tbxName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"에러가 발생하였습니다.{Environment.NewLine}" + ex.Message);
            }
        }

        private void btnAddSauce_Click(object sender, EventArgs e)
        {
            var addData = new Sauce
            {
                Name = tbxName.Text,
                IsVegan = cbxIsVegan.Checked
            };

            _context.Sauces.Add(addData);
            _context.SaveChanges();
            tbxName.Clear();
        }

        private void btnAddTopping_Click(object sender, EventArgs e)
        {
            var addData = new Topping
            {
                Name = tbxName.Text,
                Calories = Convert.ToDecimal(tbxCalory.Text)
            };

            _context.Toppings.Add(addData);
            _context.SaveChanges();
            tbxName.Clear();
            tbxCalory.Clear();
        }
    }
}