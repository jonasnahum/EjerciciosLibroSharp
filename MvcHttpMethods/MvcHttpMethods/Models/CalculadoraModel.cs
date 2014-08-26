using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcHttpMethods.Models
{
    public class CalculadoraModel
    {
        public int A { get; set; }
        public int B { get; set; }
        public int Resultado { get; set; }
        public void Sumar() 
        {
            Resultado = A + B;
        }
    }
}