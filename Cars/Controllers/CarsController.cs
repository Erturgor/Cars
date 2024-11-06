using Cars.Domain;
using Cars.Application.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.API.Controllers
{
    public class CarsController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            return await Mediator.Send(new List.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            return await Mediator.Send(new Details.Query
            {
                Id = id
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCar(Guid id, Car car)
        {
            car.Id = id;
            await Mediator.Send(new Edit.Query { Car = car });
            return Ok();
        }
        [HttpPost] // /api/cars
        public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return await Mediator.Send(new Create.Query { Car = car });
        }
        [HttpDelete("{id}")] // DELETE /api/cars/{id}
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            // Pobranie obiektu car o podanym id z bazy danych
           await Mediator.Send(new Delete.Query { Id = id });
            return Ok();
        }
    }

}
