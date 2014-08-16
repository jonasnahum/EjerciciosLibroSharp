using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodParameterModifiers
{
    public class MethodExamples
    {
        public void RefExample(ref int miEntero) 
        {
           miEntero = miEntero + 2;
        }

        public int MaximoMinimo(out int max, params int[] array)//con params, se recibe directamete un array, sin tener que ponerle la palabra new.
        {
            max = array.Max();
            return array.Min();
        }

        public int Edad(int yearOfBirth = 1984)//si no le mandan parametro, por default pone 1984.
        {
            return DateTime.Now.Year - yearOfBirth;
        }
    }

    public static class Extentions//clase estatica.
    {
        public static double Promedio(this int[] calificaciones)//metodo estatico con this.
        {
            return calificaciones.Sum() / calificaciones.Length;

        }

    }
}
