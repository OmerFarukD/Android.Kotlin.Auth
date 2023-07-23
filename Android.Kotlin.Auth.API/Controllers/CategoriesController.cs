using Android.Kotlin.Auth.API.Contexts;
using Android.Kotlin.Auth.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Android.Kotlin.Auth.API.Controllers;
[Authorize]
public class CategoriesController : ODataController
{
    private readonly BaseDbContext _context;

    public CategoriesController(BaseDbContext context)
    {
        _context = context;
    }
    
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Categories.AsQueryable());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Category category)
    {
       await _context.Categories.AddAsync(category);
       await _context.SaveChangesAsync();
       return Ok(category);
    }
}