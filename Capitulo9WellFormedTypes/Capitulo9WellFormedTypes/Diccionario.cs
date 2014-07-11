using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitulo9WellFormedTypes
{
    public class Diccionario
    {

    }
    public class Palabra
    {
        /// <summary>
        /// 
        /// </summary>
        public string Valor { get; set; }
        public override int GetHashCode()//entero que identifica a la palabra, para id mas rapido.
        {
            int hashCode = Valor.GetHashCode();
            hashCode ^= base.GetHashCode();
            return hashCode;
        }
        public override bool Equals(object obj)
        {
            string valor= ((Palabra)obj).Valor;
            if (valor == Valor + ".")
                return true;
            else 
                return false;

        }        public static bool operator == (Palabra p1,Palabra p2)        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Palabra p1, Palabra p2)
        {
            return !p1.Equals(p2);
        }
    }
}
