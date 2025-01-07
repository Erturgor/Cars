using Cars.Domain;
using Cars.Application.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Cars.API.Controllers
{
    public class CarsController : BaseApiController
    {
        [Authorize(Policy = "UserPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            var result =  await Mediator.Send(new List.Query());
            if (result == null)
                return NotFound();
            if (result.isSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.isSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
        [Authorize(Policy = "UserPolicy")]
        [HttpGet("{id}")] // /api/cars/id
        public async Task<IActionResult> GetCar(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });
            if (result == null)
                return NotFound();
            if (result.isSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.isSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
        [Authorize(Policy = "UserPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCar(Guid id, Car car)
        {
            car.Id = id;
           var result =  await Mediator.Send(new Edit.Query { Car = car });
            if (result == null)
                return NotFound();
            if (result.isSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.isSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
        [Authorize(Policy = "UserPolicy")]
        [HttpPost] // /api/cars
        public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
        {
            var result =  await Mediator.Send(new Create.Query { Car = car });
            if (result == null)
                return NotFound();
            if (result.isSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.isSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")] // DELETE /api/cars/{id}
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            // Pobranie obiektu car o podanym id z bazy danych
           var result = await Mediator.Send(new Delete.Query { Id = id });
            if (result == null)
                return NotFound();
            if (result.isSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.isSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }

}
