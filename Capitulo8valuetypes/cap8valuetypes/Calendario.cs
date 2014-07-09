using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap8valuetypes
{
    public enum Meses //una coleccion de nombres de valores, por default empiezan en 0.
    {
        Enero=1,
        Febrero,
        Marzo,
        Abril,
        Mayo,
        Junio,
        Julio,
        Agosto,
        Sep,
        Octubre,
        Nov,
        Dic
 
    }
    public enum Vocales
    {
        a='a',
        e='e',
        i='i',
        o='o',
        u='u'
    }

    public class UnaPalabra
    {
        public string QueEmpieceCon(Vocales vocal) 
        {
            switch (vocal)
            {
                case Vocales.a:
                    return "avion";
                case Vocales.e:
                    return "elefante";
                case Vocales.i:
                    return "iguana";
                case Vocales.o:
                    return "oso";
                case Vocales.u:
                    return "ursula";
                default:
                    return "ninguno";
            }
        }
    }
}
