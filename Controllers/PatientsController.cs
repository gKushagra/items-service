using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using items_service.Models;
using Microsoft.EntityFrameworkCore;

namespace items_service.AddControllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ItemContext _context;

        public PatientsController(ItemContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            IQueryable<Patient> patients = _context.Patients;

            return Ok(await patients.ToArrayAsync());
        }
    }
}

