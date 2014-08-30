using System;
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
        public ActionResult CalculadoraPartial(CalculadoraModel model)//recibe un modelo ya posteado de la clase partial que se encuentra en la carpeta Shared.
        {
            model.Sumar();
            return PartialView(model);//y aqui que hace?
        }

    }
}
