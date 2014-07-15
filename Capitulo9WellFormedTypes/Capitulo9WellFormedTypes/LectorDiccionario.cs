using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitulo9WellFormedTypes
{
    public class LectorDiccionario
    {
        private Diccionario mDiccionario;//un field.
        public Diccionario Diccionario//property.
        {
            get
            {
                if (mDiccionario==null)//cuando alguien mande llamar la property, si el contenido del fiel es null.
                {
                    mDiccionario = new Diccionario();//se va a crear un nuevo diccionario, un objeto y se guarda en le field.                
                }
 
                return mDiccionario;//de lo contrario, osea, a partir de la segunda vez, regresa el objeto ya creado en el field.
            }
        }
        public void Leer() 
        {
            string[] palabras = new string[Diccionario.Palabras.Length];//ir a esta propiedad de tipo Diccionario, si es null, crea un objeto, si no, usa el que ya existe.
            for (int i = 0; i < Diccionario.Palabras.Length; i++)//iterar a traves de los 5000000.
            {
                palabras[i] = Diccionario.Palabras[i].Valor;//la variable valor que es un string, se va guardar en palabras, en este array de string.
            }
        }
        

    }
}
