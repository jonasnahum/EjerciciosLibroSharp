using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesCap7
{
    public class Nino: IPersona
    {
        public string Trabajar()
        {
            return "Jardin  de ninos";
 
        }
        public string IrAlaEscuela() 
        {
            return "ir a la escuela";
        }
        public string Comer()
        {
            return "come papilla";
        }
    }
    public class Adulto : IPersona
    {
        public string Trabajar()
        {
            return "Trabajando en la oficina";

        }
        public string Comer()
        {
            return "come bistek";
        }
    }
}
