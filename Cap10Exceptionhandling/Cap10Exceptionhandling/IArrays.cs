using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Exceptionhandling
{
    public interface IArrays
    {
        char[] ConvertirEnArray(string text);
        int[] ContarLetrasEnpalabras(string[] arrayDePalabras);
        char LetraGanadora(char[,] arrayBidimensional);
    }

}
