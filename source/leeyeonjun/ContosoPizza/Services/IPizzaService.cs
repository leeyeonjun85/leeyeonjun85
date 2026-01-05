using ContosoPizza.Models;
using System.Collections.ObjectModel;

namespace ContosoPizza.Services
{
    public interface IPizzaService
    {
        Pizza AddNewPizza(Pizza newPizza);
        void AddPizzaTopping(int pizzaId, int toppingId);
        ObservableCollection<T> ConvertEnumToObservableCollection<T>(IEnumerable<T> enumData);
        void DeletePizzaById(int id);
        void DeleteSauceById(int id);
        void DeleteToppingById(int id);
        ObservableCollection<Pizza> GetAllPizza();
        ObservableCollection<Sauce> GetAllSauce();
        ObservableCollection<Topping> GetAllTopping();
        Pizza? GetPizzaById(int id);
        PizzaTopping? GetPizzaToppingById(int pizzaId, int toppingId);
        void Initialize();
        void RemovePizzaTopping(int pizzaId, int toppingId);
        void SaveChanges();
        void UpdatePizzaSauce(int pizzaId, int sauceId);
    }
}