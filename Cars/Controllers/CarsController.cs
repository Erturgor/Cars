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
            var car = await _context.Cars.FindAsync(id);

            // Sprawdzenie, czy obiekt istnieje
            if (car == null)
                return NotFound();
            return car;
        }
        // Endpoint do tworzenia nowego samochodu (POST)
        [HttpPost] // /api/cars
        public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
        { 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Cars.Add(car);
           await _context.SaveChangesAsync();
        // Zwraca 201 Created z nowo utworzonym zasobem
          return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }
        [HttpDelete("{id}")] // DELETE /api/cars/{id}
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            // Pobranie obiektu car o podanym id z bazy danych
            var car = await _context.Cars.FindAsync(id);

            // Sprawdzenie, czy obiekt istnieje
            if (car == null)
                return NotFound(); // Zwraca 404 jeśli obiekt nie istnieje

            // Usunięcie obiektu z bazy danych
            _context.Cars.Remove(car);

            // Asynchroniczne zapisanie zmian
            await _context.SaveChangesAsync();

            // Zwrócenie kodu 204 No Content, co oznacza sukces bez zwracania treści
            return NoContent();
        }
        [HttpPut("{id}")] // PUT /api/cars/{id}
        public async Task<IActionResult> UpdateCar(Guid id, [FromBody] Car updatedCar)
        {
            // Sprawdzenie, czy ID w ścieżce i w obiekcie są zgodne
            if (id != updatedCar.Id)
                return BadRequest("ID in path and ID in body do not match.");

            // Sprawdzenie, czy model jest prawidłowy
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Sprawdzenie, czy samochód o podanym ID istnieje w bazie danych
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null)
                return NotFound(); // Zwraca 404, jeśli samochód nie istnieje

            // Aktualizacja właściwości istniejącego samochodu na podstawie obiektu updatedCar
            existingCar.Brand = updatedCar.Brand;
            existingCar.Model = updatedCar.Model;
            existingCar.DoorsNumber = updatedCar.DoorsNumber;
            existingCar.LuggageCapacity = updatedCar.LuggageCapacity;
            existingCar.EngineCapacity = updatedCar.EngineCapacity;
            existingCar.FuelType = updatedCar.FuelType;
            existingCar.ProductionDate = updatedCar.ProductionDate;
            existingCar.CarFuelConsumption = updatedCar.CarFuelConsumption;
            existingCar.BodyType = updatedCar.BodyType;


            // Zapisanie zmian w bazie danych
            await _context.SaveChangesAsync();

            // Zwraca 204 No Content, co oznacza sukces bez zwracania treści
            return NoContent();
        }


    }

}
