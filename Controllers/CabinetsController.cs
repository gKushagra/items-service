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
    public class CabinetsController : ControllerBase
    {
        private readonly ItemContext _context;

        public CabinetsController(ItemContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        // Get all cabinets
        [HttpGet]
        public async Task<IActionResult> GetCabinets()
        {
            IQueryable<Cabinet> cabinets = _context.Cabinets;

            return Ok(await cabinets.ToArrayAsync());
        }

        // Add new cabinet
        [HttpPost]
        public async Task<IActionResult> AddCabinet([FromBody] Cabinet cabinet)
        {
            cabinet.ID = new Guid().ToString();

            try
            {
                _context.Cabinets.Add(cabinet);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw;
            }

            return Ok(new { ID = cabinet.ID });
        }

        // Get bins in cabinet with id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCabinetBins(string id)
        {
            IQueryable<Bin> bins = _context.Bins;

            var binList = await bins.Where(b => b.CabinetID == id).ToListAsync();

            if (binList == null)
            {
                return NotFound();
            }

            return Ok(binList);
        }

        // Add new bins
        [HttpPost]
        [Route("bin")]
        public async Task<IActionResult> AddBins([FromBody] Bin bin)
        {
            bin.ID = new Guid().ToString();

            try
            {
                await _context.Bins.AddAsync(bin);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw;
            }

            return Ok(new { ID = bin.ID });
        }

        // Add items to bin with id
        [HttpPost]
        [Route("bin/{id}")]
        public async Task<IActionResult> AddItemsToBin([FromRoute] string id, [FromBody] List<BinItem> binItems)
        {
            binItems.ForEach(bi =>
            {
                bi.ID = new Guid().ToString();
            });

            try
            {
                await _context.BinItems.AddRangeAsync(binItems);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw;
            }

            return Ok();
        }

        // Update bin quantity with binItem id
        [HttpPut("bin/{id}")]
        public async Task<IActionResult> UpdateBinItems([FromRoute] string id, [FromBody] BinItem binItem)
        {
            if (id != binItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(binItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.BinItems.Find(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            // return NoContent();
            return Ok();
        }
    }
}

