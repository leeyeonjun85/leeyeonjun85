using MainForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms;

namespace MainForm
{
    public partial class MainForm : Form
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;


        public MainForm(ApplicationDbContext context)
        {
            _context = context;
            InitializeComponent();
        }

        //public MainForm(IDbContextFactory<ApplicationDbContext> contextFactory)
        //{
        //    _contextFactory = contextFactory;
        //    InitializeComponent();
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Load products and display them in the UI(GridView, ListBox, etc.)
            //var products = _context.GetAllProducts();
            //dataGridView1.DataSource = products;
            //Populate your UI elements with the product data

            _context.Database.EnsureCreated();
            _context.Products.Load();
            dataGridView1.DataSource = _context.Products.Local.ToBindingList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addData = new Products
            {
                Name = "이연준",
            };

            _context.Products.Add(addData);
            _context.SaveChanges();
        }
    }


    [Table("Products")]
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
    }

    public interface IProductService
    {
        List<Products> GetAllProducts();

        void Open(); void Close();
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Products> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }
        public void Close()
        {
            Console.WriteLine("데이타베이스에 연결이 닫혔습니다.");
        }

        public void Open()
        {
            Console.WriteLine("데이타베이스에 연결이 열렸습니다");
        }
    }
}