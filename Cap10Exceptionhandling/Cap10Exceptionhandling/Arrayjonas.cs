using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Exceptionhandling
{
    public class Arrayjonas:IArrays
    {
        public char[] ConvertirEnArray(string text)
        {
            char[] arrayEnChar=text.ToCharArray();
            return arrayEnChar;
        }

        public int[] ContarLetrasEnpalabras(string[] arrayDePalabras)
        {
            int [] numero=new int[arrayDePalabras.Length];

            for (int i = 0; i < arrayDePalabras.Length; i++)
            {
                numero[i]=arrayDePalabras[i].Length;
                     
            }
            return numero;
        }

        public char LetraGanadora(char[,] gato)
        {
            if (Evaluar(gato, 'O', "columnas") || Evaluar(gato, 'O', "filas"))
                return 'O';

            if (Evaluar(gato, 'X', "columnas") || Evaluar(gato, 'X', "filas"))
                return 'X';

            if (Diagonales(gato, 'O'))
                return 'O';

            if (Diagonales(gato, 'X'))
                return 'X';

            return ' ';
        }

        public bool Diagonales(char[,] gato, char letra) 
        {
            if (gato[0, 0] == letra && gato[1, 1] == letra && gato[2, 2] == letra)
                return true;

            if (gato[2, 0] == letra && gato[1, 1] == letra && gato[0, 2] == letra)
                return true;

            return false;
        }

        public bool Evaluar(char[,] gato, char letra, string que) 
        {
            for (int i = 0; i < 3; i++)
            {
                if (que == "columnas")
                {
                    if (EvaluarLinea(gato[i, 0], gato[i, 1], gato[i, 2], letra))
                        return true;
                }
                else 
                {
                    if (EvaluarLinea(gato[0, i], gato[1, i], gato[2, i], letra))
                        return true;
                }
            }
            return false;
        }

        public bool EvaluarLinea(char a, char b, char c, char letra)
        {
            return (a == letra && b == letra && c == letra);
        }

    }
}
