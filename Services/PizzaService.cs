using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;
// public static class PizzaService
// {
//     static List<Pizza> Pizzas { get; }
//     static int nextId = 3;
//     static PizzaService()
//     {
//         Pizzas = new List<Pizza>
//         {
//             new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
//             new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
//         };
//     }

//     public static List<Pizza> GetAll() => Pizzas;

//     public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

//     public static void Add(Pizza pizza)
//     {
//         pizza.Id = nextId++;
//         Pizzas.Add(pizza);
//     }

//     public static void Delete(int id)
//     {
//         var pizza = Get(id);
//         if(pizza is null)
//             return;

//         Pizzas.Remove(pizza);
//     }

//     public static void Update(Pizza pizza)
//     {
//         var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
//         if(index == -1)
//             return;

//         Pizzas[index] = pizza;
//     }
// }

public class PizzaService
{
    private readonly PizzaContext _context;
    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public Pizza Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();

        return newPizza;
    }
    public void AddTopping(int pizzaId, int toppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var toppingToAdd = _context.Toppings.Find(toppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        if (pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        _context.SaveChanges();
    }

        public void UpdateSauce(int pizzaId, int sauceId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        // var sauceToUpdate = _context.Sauce.Find(sauceId);
        var sauceToUpdate = _context.Sauces.Find(sauceId);


        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or sauce does not exist");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if (pizzaToDelete is not null)
        {
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
    }

}