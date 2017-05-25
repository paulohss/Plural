﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class GeoCoordService
    {
        private IConfigurationRoot _config;
        private ILogger<GeoCoordService> _logger;

        public GeoCoordService(ILogger<GeoCoordService> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }


        public async Task<GeoCoordResult> GetGeoCoordAsync(string name)
        {
            var result = new GeoCoordResult() { Message = "Fail" };

            var apiKey = _config["Keys:BingKey"];
            //apiKey = Environment.GetEnvironmentVariable("Keys__BingKey");
            var encodedName = WebUtility.UrlEncode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);

            // Read out the results
            // Fragile, might need to change if the Bing API changes
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!results["resourceSets"][0]["resources"].HasValues)
            {
                result.Message = $"Could not find '{name}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{name}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Message = "Success";
                    result.Sucess = true;
                }
            }

            return result;
        }

    }
}
