using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace ContosoPizza.Services
{
    public class PizzaService(PizzaContext context) : IPizzaService
    {
        private PizzaContext _context { get; set; } = context;

        public void AddPizzaTopping(int pizzaId, int toppingId)
        {
            var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
            var toppingToAdd = _context.Toppings.Find(toppingId);

            if (pizzaToUpdate is null || toppingToAdd is null)
            {
                throw new InvalidOperationException("Pizza or topping does not exist");
            }

            pizzaToUpdate.Toppings ??= new List<Topping>();

            pizzaToUpdate.Toppings.Add(toppingToAdd);

            _context.SaveChanges();
        }

        public void RemovePizzaTopping(int pizzaId, int toppingId)
        {
            var pizzaToppingToRemove = GetPizzaToppingById(pizzaId, toppingId);

            if (pizzaToppingToRemove is not null)
            {
                _context.PizzaTopping.Remove(pizzaToppingToRemove);
                _context.SaveChanges();
            }

        }

        public Pizza? GetPizzaById(int id)
        {
            Pizza? pizza = _context.Pizzas
                    .Include(p => p.Toppings)
                    .Include(p => p.Sauce)
                    .SingleOrDefault(p => p.Id == id);

            return pizza;
        }

        public PizzaTopping? GetPizzaToppingById(int pizzaId, int toppingId)
        {
            PizzaTopping? pizzaTopping = _context.PizzaTopping
                    //.AsNoTracking()
                    .Where(p => p.PizzaId == pizzaId)
                    .SingleOrDefault(p => p.ToppingId == toppingId);

            return pizzaTopping;
        }

        public Pizza AddNewPizza(Pizza newPizza)
        {
            _context.Pizzas.Add(newPizza);
            _context.SaveChanges();


            return newPizza;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdatePizzaSauce(int pizzaId, int sauceId)
        {
            var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
            var sauceToUpdate = _context.Sauces.Find(sauceId);

            if (pizzaToUpdate is null || sauceToUpdate is null)
            {
                throw new InvalidOperationException("Pizza or sauce does not exist");
            }

            pizzaToUpdate.Sauce = sauceToUpdate;

            _context.SaveChanges();
        }

        public void DeletePizzaById(int id)
        {
            var toDeletePizza = _context.Pizzas.Find(id);
            if (toDeletePizza is not null)
            {
                _context.Pizzas.Remove(toDeletePizza);
                _context.SaveChanges();
            }
        }

        public void DeleteSauceById(int id)
        {
            var toDeleteSauce = _context.Sauces.Find(id);
            if (toDeleteSauce is not null)
            {
                _context.Sauces.Remove(toDeleteSauce);
                _context.SaveChanges();
            }
        }

        public void DeleteToppingById(int id)
        {
            var toDeleteTopping = _context.Toppings.Find(id);
            if (toDeleteTopping is not null)
            {
                _context.Toppings.Remove(toDeleteTopping);
                _context.SaveChanges();
            }
        }

        public ObservableCollection<Pizza> GetAllPizza()
        {
            ObservableCollection<Pizza> returnData = [];

            //enumData.Pizzas
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            _context.Pizzas.Load();
            returnData = _context.Pizzas.Local.ToObservableCollection();

            return returnData;
        }

        public ObservableCollection<Sauce> GetAllSauce()
        {
            ObservableCollection<Sauce> returnData = [];

            //enumData.Sauces
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            _context.Sauces.Load();
            returnData = _context.Sauces.Local.ToObservableCollection();

            return returnData;
        }
        public ObservableCollection<Topping> GetAllTopping()
        {
            ObservableCollection<Topping> returnData = [];

            //enumData.Toppings
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            _context.Toppings.Load();
            returnData = _context.Toppings.Local.ToObservableCollection();

            return returnData;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();

            if (_context.Pizzas.Any()
                && _context.Toppings.Any()
                && _context.Sauces.Any())
            {
                return;   // DB has been seeded
            }

            // Clear All Rows
            foreach (var item in _context.Pizzas) _context.Pizzas.Remove(item);
            foreach (var item in _context.Toppings) _context.Toppings.Remove(item);
            foreach (var item in _context.Sauces) _context.Sauces.Remove(item);

            // DB Setting
            DbConnection _conn = _context.Database.GetDbConnection();
            _conn.Open();
            DbCommand _cmd = _conn.CreateCommand();
            _cmd.CommandText = "PRAGMA journal_mode=Off;";
            _cmd.ExecuteNonQuery();

            // Init Data
            var pepperoniTopping = new Topping { Name = "Pepperoni", Calories = 130 };
            var sausageTopping = new Topping { Name = "Sausage", Calories = 100 };
            var hamTopping = new Topping { Name = "Ham", Calories = 70 };
            var chickenTopping = new Topping { Name = "Chicken", Calories = 50 };
            var pineappleTopping = new Topping { Name = "Pineapple", Calories = 75 };

            var tomatoSauce = new Sauce { Name = "Tomato", IsVegan = true };
            var alfredoSauce = new Sauce { Name = "Alfredo", IsVegan = false };

            var pizzas = new Pizza[]
            {
                new()
                {
                    Name = "Meat Lovers",
                    Sauce = tomatoSauce,
                    Toppings = new List<Topping>
                        {
                            pepperoniTopping,
                            sausageTopping,
                            hamTopping,
                            chickenTopping
                        }
                },
                new()
                {
                    Name = "Hawaiian",
                    Sauce = tomatoSauce,
                    Toppings = new List<Topping>
                        {
                            pineappleTopping,
                            hamTopping
                        }
                },
                new()
                {
                    Name="Alfredo Chicken",
                    Sauce = alfredoSauce,
                    Toppings = new List<Topping>
                        {
                            chickenTopping
                        }
                }
            };

            _context.Pizzas.AddRange(pizzas);
            _context.SaveChanges();

            _conn.Dispose();
            _cmd.Dispose();
        }

        public ObservableCollection<T> ConvertEnumToObservableCollection<T>(IEnumerable<T> enumData)
        {
            ObservableCollection<T> returnData = [];

            foreach (var data in enumData)
            {
                returnData.Add(data);
            }

            return returnData;
        }
    }
}
