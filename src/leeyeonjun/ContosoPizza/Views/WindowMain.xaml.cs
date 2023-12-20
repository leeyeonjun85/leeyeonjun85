using ContosoPizza.Models;
using ContosoPizza.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            string? checkBoxContent = checkBox?.Content.ToString();
            Pizza? selectedPizza = viewModel?.SelectedPizza;
            List<Topping>? allToppings = viewModel?.AllTopping;

            if (viewModel is not null 
                && checkBox is not null 
                && checkBoxContent is not null
                && selectedPizza is not null
                && allToppings is not null)
            {
                foreach(Topping topping in allToppings)
                {
                    if (checkBoxContent.Contains($"{topping.Name}"))
                    {
                        if ((bool)checkBox.IsChecked!)
                        {
                            viewModel.AddTopping(selectedPizza.Id, topping.Id);
                            break;
                        }
                        else
                        {
                            viewModel.RemoveTopping(selectedPizza.Id, topping.Id);
                            break;
                        }
                    }
                }
            }
            
        }
    }
}
