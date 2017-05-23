using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Services;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailServices _mailservices;
        private IConfigurationRoot _config;

        public AppController(IMailServices mailservices, IConfigurationRoot config)
        {
            _mailservices = mailservices;
            _config = config;
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
