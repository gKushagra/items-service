using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using items_service.Models;
using Microsoft.EntityFrameworkCore;

namespace items_service.AddControllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ItemContext _context;

        public OrdersController(ItemContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            IQueryable<Order> orders = _context.Orders;

            return Ok(
                await orders.Join(
                _context.Suppliers,
                o => o.SupplierID,
                s => s.ID,
                (o, s) => new
                {
                    ID = o.ID,
                    Supplier = s,
                    ItemID = o.ItemID,
                    Quantity = o.Quantity,
                    Date = o.Date
                }
                ).Join(
                _context.Items,
                or => or.ItemID,
                i => i.ID,
                (or, i) => new
                {
                    ID = or.ID,
                    Supplier = or.Supplier,
                    Item = i,
                    Quantity = or.Quantity,
                    Date = or.Date
                }
                ).ToListAsync()
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            order.ID = Guid.NewGuid().ToString();
            order.Date = DateTime.Now;

            try
            {
                _context.Orders.Add(order);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok(new { ID = order.ID });
        }
    }
}

