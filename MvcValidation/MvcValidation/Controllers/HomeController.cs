using MvcValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcValidation.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //ModelState.AddModelError("", "This is all wrong");
            //ModelState.AddModelError("Title", "what a terrible name");
            return View();
        }
        [HttpPost]
        public ActionResult Index(PersonaModel persona)
        {
                   return View(persona);
        }
    }
}
