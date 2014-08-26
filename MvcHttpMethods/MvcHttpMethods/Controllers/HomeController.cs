using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHttpMethods.Models;

namespace MvcHttpMethods.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index(string var1, string var2)//con url
        {
            ViewBag.Nombre = var1;//sin modelo con dynamic.
            ViewData["Apellido"] = var2;
            return View();
        }

        //
        // GET: /Home/
        [HttpGet]
        public ActionResult PaginaConModelo(string nombre, string apellido)// en el url se le pasan las variables de nombre y apellido.
        {
            PersonaModel modelo = new PersonaModel();//clase que esta en carpeta Models.
            modelo.Nombre = nombre;
            modelo.Apellido = apellido;

            return View(modelo);//se pasa un objeto como parametro a la vista. la view tiene el mismo nombre que este action.
        }

        [HttpGet]
        public ActionResult MiPagina() 
        {
            PersonaModel modelo = new PersonaModel();//una clase que solo tiene 2 propiedades, nombre y apellido.
            return View(modelo);//se le pasa el objeto vacio a la vista que tiene una implementacion y que coincide con el nombre de este action.
        }
        
        [HttpPost]
        public ActionResult MiPagina(PersonaModel modelo)//modelo posteado por el submit del form en el action MiPagina get.
        {
            return View(modelo);
        }
    }

}
