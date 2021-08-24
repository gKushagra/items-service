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

            return Ok(
            await items.Join(
                _context.Suppliers,
                i => i.SupplierID,
                s => s.ID,
                (i, s) => new
                {
                    ID = i.ID,
                    Quantity = i.Quantity,
                    Expiry = i.Expiry,
                    DateAdded = i.DateAdded,
                    BrandName = i.BrandName,
                    GenericName = i.GenericName,
                    Code = i.Code,
                    Supplier = s
                }
            ).ToListAsync()
            );
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] List<Item> items)
        {
            try
            {
                await _context.Items.AddRangeAsync(items);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw;
            }

            return Ok();
        }

        // Update the Quantity of items, when added/removed from bins
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] string id, [FromBody] Item item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Items.Find(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return Ok();
        }
    }
}

