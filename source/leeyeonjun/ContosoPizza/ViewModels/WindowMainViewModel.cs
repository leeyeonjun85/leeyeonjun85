using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ContosoPizza.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<int>>
    {
        [ObservableProperty]
        private string _windowTitle = @"Contos Pizza Manager!";
        [ObservableProperty]
        private ObservableCollection<Pizza> _allPizza = [];
        [ObservableProperty]
        private ObservableCollection<Sauce> _allSauce = [];
        [ObservableProperty]
        private ObservableCollection<Topping> _allTopping = [];

        [ObservableProperty]
        private Pizza? _selectedPizza = new();
        [ObservableProperty]
        private string? _selectedPizzaName;

        [ObservableProperty]
        private Sauce? _selectedPizzaSauce;
        [ObservableProperty]
        private string? _selectedPizzaSauceName;
        [ObservableProperty]
        private int _selectedPizzaSauceIndex;

        [ObservableProperty]
        private int _selectedPizzaIndex;
        private IPizzaService PizzaService { get; set; }
        private IViewService ViewService { get; set; }

        public WindowMainViewModel(IViewService viewService, IPizzaService pizzaService)
        {
            IsActive = true;
            ViewService = viewService;
            PizzaService = pizzaService;
        }

        [RelayCommand]
        private void BtnMakePizzaClick(object? obj)
        {
            if (SelectedPizza is not null)
            {
                string messageContent = $"Name : {SelectedPizza.Name}{Environment.NewLine}";
                messageContent += $"Sauce : {SelectedPizza.Sauce?.Name}{Environment.NewLine}";
                messageContent += $"Toppings : ";
                if (SelectedPizza.Toppings is not null)
                {
                    foreach (var topping in SelectedPizza.Toppings)
                    {
                        messageContent += $"{topping.Name}, ";
                    }
                }
                messageContent = messageContent[..^2];

                MessageBox.Show(messageContent, "Wow! Pizza~!");
            }
        }


        [RelayCommand]
        private void BtnNewPizzaClick(TextBox? newPizzaTextBox)
        {
            if (newPizzaTextBox is not null && !string.IsNullOrEmpty(newPizzaTextBox.Text))
            {
                Pizza newPizza = new()
                {
                    Name = newPizzaTextBox.Text
                };

                PizzaService.AddNewPizza(newPizza);
                newPizzaTextBox.Text = string.Empty;
            }
            else
                MessageBox.Show("Insert Correct Pizza Name Please!");
        }

        [RelayCommand]
        private void DeletePizzaClick(object? obj)
        {
            if (SelectedPizza is not null)
            {
                PizzaService.DeletePizzaById(SelectedPizza.Id);
            }
        }

        [RelayCommand]
        private void BtnShowWindowSubClick(object? obj)
        {
            ViewService.ShowView<WindowSub, WindowSubViewModel>(SelectedPizzaIndex);
        }

        [RelayCommand]
        private void SelectionChangedSauce(ComboBox? comboBoxPizzaSauce)
        {
            if (SelectedPizza is not null && comboBoxPizzaSauce is not null)
            {
                var selectedPizzaSauce = comboBoxPizzaSauce.SelectedItem as Sauce;
                if (selectedPizzaSauce is not null)
                    PizzaService.UpdatePizzaSauce(SelectedPizza.Id, selectedPizzaSauce.Id);
            }
        }

        [RelayCommand]
        private void SelectionChangedPizza(WrapPanel? wrapPanel)
        {
            if (wrapPanel is not null && SelectedPizza is not null)
            {
                Pizza? selectedPizza = PizzaService.GetPizzaById(this.SelectedPizza.Id);
                if (selectedPizza is not null)
                {
                    wrapPanel.Children.Clear();
                    SelectedPizzaName = $"{selectedPizza.Name}";
                    SelectedPizzaSauce = selectedPizza.Sauce;
                    if (selectedPizza.Sauce is not null)
                        SelectedPizzaSauceIndex = AllSauce.Select(x => x.Id).ToList().IndexOf(selectedPizza.Sauce.Id);
                    else
                        SelectedPizzaSauceIndex = -1;
                    if (selectedPizza.Toppings is not null)
                    {
                        foreach (Topping topping in AllTopping)
                        {
                            var findThing = selectedPizza.Toppings
                                            .Where(x => x.Id == topping.Id)
                                            .ToList();

                            if (findThing.Count > 0)
                            {
                                wrapPanel.Children.Add(new CheckBox()
                                {
                                    Margin = new Thickness(10, 3, 10, 3),
                                    Content = $"{topping.Name}({topping.Calories}cal)",
                                    IsChecked = true,
                                    VerticalContentAlignment = VerticalAlignment.Center
                                });
                            }
                            else
                            {
                                wrapPanel.Children.Add(new CheckBox()
                                {
                                    Margin = new Thickness(10, 3, 10, 3),
                                    Content = $"{topping.Name}({topping.Calories}cal)",
                                    IsChecked = false,
                                    VerticalContentAlignment = VerticalAlignment.Center
                                });
                            }
                        }
                    }
                }
            }
        }

        public void Receive(ValueChangedMessage<int> pizzaIndex)
        {
            SelectedPizzaIndex = -1;
            SelectedPizzaIndex = pizzaIndex.Value;
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            PizzaService.Initialize();

            AllPizza = PizzaService.GetAllPizza();
            AllSauce = PizzaService.GetAllSauce();
            AllTopping = PizzaService.GetAllTopping();

            SelectedPizzaIndex = 0;
            if (sender is WindowMain window)
            {
                WrapPanel? wrapPanel = FindChild<WrapPanel>(window, "ToppingsWrapPanel");
                SelectionChangedPizza(wrapPanel);
            }
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }


        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <wrapPanel name="parent">A direct parent of the queried item.</wrapPanel>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <wrapPanel name="childName">x:Name or Name of child. </wrapPanel>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public T? FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T? foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                if (child is not T)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    // If the child's name is set for search
                    if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }
}
