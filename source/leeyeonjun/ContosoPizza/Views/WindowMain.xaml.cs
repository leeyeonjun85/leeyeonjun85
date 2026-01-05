using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.ViewModels;

namespace ContosoPizza.Views
{
    /// <summary>
    /// WindowMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
        }

        private void OnClickCheckBox(object sender, RoutedEventArgs e)
        {
            WindowMainViewModel? viewModel = this.DataContext as WindowMainViewModel;
            CheckBox? checkBox = sender as CheckBox;
            IPizzaService pizzaService = (IPizzaService)Ioc.Default.GetService(typeof(IPizzaService))!;
            string? checkBoxContent = checkBox?.Content.ToString();
            Pizza? selectedPizza = viewModel?.SelectedPizza;
            ObservableCollection<Topping>? allToppings = viewModel?.AllTopping;

            if (pizzaService is not null
                && checkBox is not null
                && checkBoxContent is not null
                && selectedPizza is not null
                && allToppings is not null)
            {
                foreach (Topping topping in allToppings)
                {
                    if (checkBoxContent.Contains($"{topping.Name}"))
                    {
                        if ((bool)checkBox.IsChecked!)
                        {
                            pizzaService.AddPizzaTopping(selectedPizza.Id, topping.Id);
                            break;
                        }
                        else
                        {
                            pizzaService.RemovePizzaTopping(selectedPizza.Id, topping.Id);
                            break;
                        }
                    }
                }
            }

        }
    }
}
