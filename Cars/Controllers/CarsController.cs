using Cars.Domain;
using Cars.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.API.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly DataContext _context;
        public CarsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            return await _context.Cars.FindAsync(id);
        }
    }
}
