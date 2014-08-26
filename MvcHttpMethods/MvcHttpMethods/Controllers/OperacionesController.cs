﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHttpMethods.Models;

namespace MvcHttpMethods.Controllers
{
    public class OperacionesController : Controller
    {
        //
        // GET: /Operaciones/
        [HttpPost]
        public ActionResult CalculadoraPartial(CalculadoraModel model)
        {
            model.Sumar();
            return PartialView(model);
        }

    }
}