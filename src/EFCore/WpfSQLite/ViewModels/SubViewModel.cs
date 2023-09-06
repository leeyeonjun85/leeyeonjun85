using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using WpfSQLite.Models;

namespace WpfSQLite.ViewModels
{
    public class SubViewModel : ViewModelBase, IParameterReceiver
    {
        #region Private Fields
        private readonly ModelContext? _context;

        #endregion


        #region Properties
        public SubData SubData { get; set; } = default!;

        private ObservableCollection<Category> _categoryList = new();
        public ObservableCollection<Category> CategoryList
        {
            get { return _categoryList; }
            set { SetProperty(ref _categoryList, value); }
        }

        private ObservableCollection<Product> _productList = new();
        public ObservableCollection<Product> ProductList
        {
            get { return _productList; }
            set { SetProperty(ref _productList, value); }
        }

        private List<string> _categoryNameList = new();
        public List<string> CategoryNameList
        {
            get { return _categoryNameList; }
            set { SetProperty(ref _categoryNameList, value); }
        }

        private string _selectedCategory = "";
        public string? SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory!, value); }
        }

        private string _statusBar1 = "Status: Ready";
        public string StatusBar1
        {
            get { return _statusBar1; }
            set { SetProperty(ref _statusBar1, value); }
        }

        private UInt32 _statusBarProgressBar = 0;
        public UInt32 StatusBarProgressBar
        {
            get { return _statusBarProgressBar; }
            set { SetProperty(ref _statusBarProgressBar, value); }
        }

        #endregion


        public SubViewModel(ModelContext context)
        {
            _context = context;
        }



        public void ReceiveParameter(object parameter)
        {
            if (parameter is SubData subData)
            {
                SubData = subData;
                //await Task.Delay(10);
                //Thread.Sleep(10);
            }
        }

        private void ConnectDB(object? obj)
        {
            try
            {
                if (_context != null)
                {
                    _context.Database.EnsureCreated();

                    _context.Products.Load();
                    ProductList = _context.Products.Local.ToObservableCollection();

                    _context.Categories.Load();
                    CategoryList = _context.Categories.Local.ToObservableCollection();
                    StatusBarProgressBar = 50;

                    CategoryNameList = CategoryList
                        .Select(data => data.Name)
                        .Distinct()
                        .ToList();

                    StatusBarProgressBar = 100;
                    StatusBar1 = "Status : Connected";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.Source}");
                throw;
            }
        }

        private void InsertData(object? obj)
        {
            if (obj is TextBox insertTextBox && _context != null)
            {
                Product addData;
                foreach (var c in CategoryList)
                {
                    if (c.Name == SelectedCategory)
                    {
                        addData = new Product
                        {
                            Name = insertTextBox.Text,
                            CategoryId = c.CategoryId
                        };
                        _context.Products.Add(addData);
                        _context.SaveChanges();
                        ConnectDB(obj);

                        SelectedCategory = null;
                        insertTextBox.Text = "";
                    }
                }
            }
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            //MessageBox.Show("SubWindow Closing");
        }


        public ICommand SaveChangesDataGridCommand => new RelayCommand<object>(SaveChangesDataGrid);

        private void SaveChangesDataGrid(object? obj)
        {
            try
            {
                _context?.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.Source}");
                throw;
            }

        }

        public ICommand InsertCommand => new RelayCommand<object>(InsertData);

        public ICommand ConnectCommand => new RelayCommand<object>(ConnectDB);

        public ICommand CloseCommand => new RelayCommand<object>(_ => Window?.Close());
    }
}
