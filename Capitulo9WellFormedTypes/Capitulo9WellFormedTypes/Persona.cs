using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitulo9WellFormedTypes
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", Nombre, Apellido);
            
        }
    }
    public class Mascota
    {
        public string Nombre { get; set; }
       
    }
}
