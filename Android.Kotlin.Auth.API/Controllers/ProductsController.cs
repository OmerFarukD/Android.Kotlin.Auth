using Android.Kotlin.Auth.API.Contexts;
using Android.Kotlin.Auth.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Android.Kotlin.Auth.API.Controllers;

[Authorize]
public class ProductsController : ODataController
{
    private readonly BaseDbContext _context;

    public ProductsController(BaseDbContext context)
    {
        _context = context;
    }
    [EnableQuery(PageSize = 5)]
    public IActionResult Get()
    {
        return Ok(_context.Products.AsQueryable());
    }

    public IActionResult Get([FromODataUri] int key)
    {
        return Ok(_context.Products.Where(x => x.Id.Equals(key)));
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPut]
    public async Task<IActionResult> PutProduct([FromODataUri]int key, [FromBody]Product product)
    {
        product.Id = key;
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromODataUri]int key)
    {
        var product = await _context.Products.FindAsync(key);
        if (product is null) return NotFound();
        _context.Products.Remove(product);
         await _context.SaveChangesAsync();
         return NoContent();
    }

}