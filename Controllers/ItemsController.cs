using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using items_service.Models;
using Microsoft.EntityFrameworkCore;

namespace items_service.AddControllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemContext _context;

        public ItemsController(ItemContext context)
        { 
            _context = context;

            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            IQueryable<Item> items = _context.Items;

            return Ok(await items.ToArrayAsync()); 
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetProduct(int id){
        //     var product = await _context.Products.FindAsync(id);
        //     if (product == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(product);
        // }

        // [HttpPost]
        // public async Task<ActionResult<Product>> PostProduct([FromBody]Product product)
        // {
        //     _context.Products.Add(product);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(
        //         "GetProduct",
        //         new { id = product.Id },
        //         product
        //     );
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        // {
        //     if (id != product.Id) {
        //         return BadRequest();
        //     }

        //     _context.Entry(product).State = EntityState.Modified;

        //     try 
        //     {
        //         await _context.SaveChangesAsync();
        //     } 
        //     catch (DbUpdateConcurrencyException) 
        //     {
        //         if (_context.Products.Find(id) == null) {
        //             return NotFound();
        //         }

        //         throw;
        //     }

        //     return NoContent();

        // }
    }
}

