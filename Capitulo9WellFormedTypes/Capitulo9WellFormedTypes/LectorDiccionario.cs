using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitulo9WellFormedTypes
{
    public class LectorDiccionario
    {
        private Diccionario mDiccionario;
        public Diccionario Diccionario
        {
            get
            {
                if (mDiccionario==null)
                {
                    mDiccionario = new Diccionario();                   
                }
 
                return mDiccionario;
            }
        }
        public void Leer() 
        {
            string[] palabras = new string[Diccionario.Palabras.Length];
            for (int i = 0; i < Diccionario.Palabras.Length; i++)
            {
                palabras[i] = Diccionario.Palabras[i].Valor;
            }
        }
        

    }
}
