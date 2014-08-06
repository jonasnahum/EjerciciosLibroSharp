using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap16CustomCollections
{
    public class Alumno:IComparable<Alumno>
    {
        public string Nombre { get; set; }
        public int CompareTo(Alumno other)//un alumno lo va a comparar con otro alumno. regresa un int, Value Meaning Less than zero
        {                                            //     This object is less than the other parameter.Zero This object is equal to
                                                     //     other. Greater than zero This object is greater than other.        
            int LongOther = other.Nombre.Length;
            int LongActual = this.Nombre.Length;
            if (LongActual < LongOther)
            {
                return 0 - 1;
            }
            if (LongActual == LongOther)
            {
                return 0;
            }
            if (LongActual > LongOther)
            {
                return 1;
            }
            return 0;//valor que se regresa si no entra ninguna de las condiciones. nunca se va llamar.
        }
        public Alumno(string nombre) 
        {
            Nombre = nombre;
        }
    }
}
