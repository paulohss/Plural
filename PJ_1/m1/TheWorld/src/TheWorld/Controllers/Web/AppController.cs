﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Services;
using TheWorld.Models;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailServices _mailservices;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        /// <summary>
        /// COnstructor
        /// </summary>
        /// <param name="mailservices"></param>
        /// <param name="config"></param>
        /// <param name="context"></param>
        public AppController(IMailServices mailservices, IConfigurationRoot config, IWorldRepository repository, ILogger<AppController> logger)
        {
            _mailservices = mailservices;
            _config = config;
            _repository = repository;
            _logger = logger;
        }


        public IActionResult Index()
        {
            try
            {
                var data = _repository.GetAllTrips();

                return View(data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Redirect("/error");
            }
        }


        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ViewModels.ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailservices.SendMail(_config["MailSettings:ToAddress"], model.Email, "From Bla", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
