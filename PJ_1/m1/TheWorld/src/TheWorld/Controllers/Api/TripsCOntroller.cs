﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private IWorldRepository _repository;

        public TripsController(Models.IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllTrips());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]TripViewModel trip)
        {
            if(ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(trip);
                return Created("", newTrip);
            }


            return BadRequest(ModelState);
        }
    }
}
