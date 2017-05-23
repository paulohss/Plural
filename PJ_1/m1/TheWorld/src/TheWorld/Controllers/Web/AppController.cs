using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Services;
using TheWorld.Models;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailServices _mailservices;
        private IConfigurationRoot _config;
        private WordContext _context;

        /// <summary>
        /// COnstructor
        /// </summary>
        /// <param name="mailservices"></param>
        /// <param name="config"></param>
        /// <param name="context"></param>
        public AppController(IMailServices mailservices, IConfigurationRoot config, WordContext context)
        {
            _mailservices = mailservices;
            _config = config;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
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
