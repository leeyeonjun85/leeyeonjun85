using DevTabbed.Models;
using System.Collections.ObjectModel;

namespace DevTabbed.ViewModels
{
    public class ChartsViewModel : BaseViewModel
    {
        public ChartsViewModel()
        {
            Title = "Charts";
            Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items { get; private set; }

        async public void OnAppearing()
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