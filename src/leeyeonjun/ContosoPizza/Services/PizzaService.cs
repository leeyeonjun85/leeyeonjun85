using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Logging;
using ContosoPizza.ViewModels;
using ContosoPizza.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class PizzaService : IPizzaService
    {
        public ObservableCollection<Pizza> GetAllPizza(PizzaContext context)
        {
            ObservableCollection<Pizza> returnData = new();

            //context.Pizzas
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            context.Pizzas.Load();
            returnData = context.Pizzas.Local.ToObservableCollection();

            return returnData;
        }

        public ObservableCollection<Sauce> GetAllSauce(PizzaContext context)
        {
            ObservableCollection<Sauce> returnData = new();

            //context.Sauces
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            context.Sauces.Load();
            returnData = context.Sauces.Local.ToObservableCollection();

            return returnData;
        }
        public List<Topping> GetAllTopping(PizzaContext context)
        {
            List<Topping> returnData = new();

            //context.Toppings
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            context.Toppings.Load();
            returnData = context.Toppings.Local.ToList();

            return returnData;
        }
    }
}
