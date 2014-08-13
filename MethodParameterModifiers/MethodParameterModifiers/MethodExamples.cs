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

        public int MaximoMinimo(out int max, params int[] array) 
        {
            max = array.Max();
            return array.Min();
        }

        public int Edad(int yearOfBirth = 1984)
        {
            return DateTime.Now.Year - yearOfBirth;
        }
    }

    public static class Extentions
    {
        public static double Promedio(this int[] calificaciones)
        {
            return calificaciones.Sum() / calificaciones.Length;

        }

    }
}
