using ContosoPizza.Models;
using System.Collections.ObjectModel;

namespace ContosoPizza.Services
{
    public interface IPizzaService
    {
        ObservableCollection<Pizza> GetAllPizza(PizzaContext context);
        ObservableCollection<Sauce> GetAllSauce(PizzaContext context);
        List<Topping> GetAllTopping(PizzaContext context);
    }
}