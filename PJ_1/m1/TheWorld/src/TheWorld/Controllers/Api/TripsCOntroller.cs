using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController: Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(Models.IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail: {ex}");
                return BadRequest("Error: " + ex.Message);
            }

       }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if(ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(trip);
                _repository.AddTrip(newTrip);

                if(await _repository.SaveChangesAsync())
                   return Created($"api/trips/{trip.Name}", newTrip);
                else
                   return BadRequest("Fail saving.");
            }

            return BadRequest(ModelState);
        }
    }
}
