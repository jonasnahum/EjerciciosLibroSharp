using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Exceptionhandling
{
    public class EjercicioArrays : IEjercicioArrays
    {
        public int[] ObtenerArreglo(int n)
        {
            
            int [] arreglo = new int[n];
            return arreglo;
        }

        public void LlenarArreglo(int[] arreglo)
        {
          
            for (int i = 0; i <arreglo.Length; i++)
            {
                int contador = arreglo.Length - i;//esta i es igual al contador.
                arreglo[i] = contador;//se pone[i] cuando va seguida del nombre de la variable array.
            }

        }
    }
}
