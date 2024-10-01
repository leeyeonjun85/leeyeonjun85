using DevFlyout.Models;
using System.Collections.ObjectModel;

namespace DevFlyout.ViewModels
{
    public class ChartsViewModel : BaseViewModel
    {
        public ChartsViewModel()
        {
            Title = "ChartsView";
            Items = new ObservableCollection<Item>();
            LoadData();
        }

        public ObservableCollection<Item> Items { get; private set; }

        public void OnAppearing()
        {
            LoadData();
        }

        async void LoadData()
        {
            IEnumerable<Item> items = await DataStore.GetItemsAsync(true);
            Items.Clear();
            foreach (Item item in items)
            {
                Items.Add(item);
            }
        }
    }
}
