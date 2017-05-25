using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;
using TheWorld.Services;

namespace TheWorld.Controllers.Api
{

    [Route("/api/trips/{tripName}/stops")]
    public class StopsController: Controller
    {
        private GeoCoordService _coordService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger, Services.GeoCoordService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail getting {ex}");
            }
            return BadRequest("Bad bad");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel stop )
        {
            try
            {

                if(ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(stop);

                    _repository.AddStop(tripName, newStop);

                    var result = await _coordService.GetGeoCoordAsync(newStop.Name);
                    if (result.Sucess)
                    {
                        if (await _repository.SaveChangesAsync())
                        {
                            newStop.Latitude = result.Latitude;
                            newStop.Longitude = result.Longitude;
                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                        }
                    }
                    else
                    {
                        _logger.LogError(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
            }

            return BadRequest("Bad bad");
        }
    }
}
