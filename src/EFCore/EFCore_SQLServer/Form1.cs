using EFCore_SQLServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System;
using System.Xml.Linq;

namespace EFCore_SQLServer
{
    public partial class Form1 : Form
    {
        SQLServerContext context;

        public Form1()
        {
            InitializeComponent();
            context = new SQLServerContext();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (context.Database.CanConnect())
            {
                lbStatus.Text = $"���� : ���� �Ϸ�";
                context.Database.EnsureCreated();

                context.Products.Load();
                dataGridView1.DataSource = context.Products.Local.ToBindingList();
            }
            else
            {
                lbStatus.Text = $"���� : ���� ����";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Random rnd = new Random();
                var addData = new Product
                {
                    Name = tbAddName.Text,
                    Price = (decimal)((decimal)rnd.Next(0,10) / (decimal)10),
                };

                context.Products.Add(addData);
                context.SaveChanges();
                lbStatus.Text = $"���� : ������ �߰�";
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ �߻��Ͽ����ϴ�." + ex.Message);
            }
        }
    }
}