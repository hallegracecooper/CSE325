using Microsoft.AspNetCore.Mvc;
using PizzaApi.Models;
using PizzaApi.Services;

namespace PizzaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzasController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Pizza>> GetAll() => PizzaService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        return pizza is null ? NotFound() : pizza;
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Create(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id) return BadRequest();
        var existing = PizzaService.Get(id);
        if (existing is null) return NotFound();
        PizzaService.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = PizzaService.Get(id);
        if (existing is null) return NotFound();
        PizzaService.Delete(id);
        return NoContent();
    }
}
