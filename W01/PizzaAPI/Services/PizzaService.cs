using PizzaApi.Models;

namespace PizzaApi.Services;

public static class PizzaService
{
    private static readonly List<Pizza> _pizzas = new()
    {
        new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
        new Pizza { Id = 2, Name = "Veggie",          IsGlutenFree = true  },

        // ✅ YOUR ADDED RECORD (requirement)
        new Pizza { Id = 3, Name = "BBQ Chicken",     IsGlutenFree = false }
    };

    private static int _nextId = 4;

    public static List<Pizza> GetAll() => _pizzas;
    public static Pizza? Get(int id) => _pizzas.FirstOrDefault(p => p.Id == id);

    public static Pizza Create(Pizza pizza)
    {
        pizza.Id = _nextId++;
        _pizzas.Add(pizza);
        return pizza;
    }

    public static void Update(Pizza pizza)
    {
        var index = _pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1) return;
        _pizzas[index] = pizza;
    }

    public static void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null) return;
        _pizzas.Remove(pizza);
    }
}
