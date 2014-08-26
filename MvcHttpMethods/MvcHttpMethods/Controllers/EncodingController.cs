using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcHttpMethods.Controllers
{
    public class EncodingController : Controller
    {
        //
        // GET: /Encoding/

        public ActionResult Index(string var1)
        {
            ViewBag.Nombre = var1;
            return View();
        }
        public ActionResult LayOut() 
        {
            return View();
        }
        public ActionResult UsandoLayOut() 
        {
            return View();
        }
    }
}
