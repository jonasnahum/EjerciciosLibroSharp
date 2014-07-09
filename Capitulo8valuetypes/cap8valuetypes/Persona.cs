using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap8valuetypes
{
    public class Persona
    {
        public int Edad { get; set; }
        public string Nombre { get; set; }
        
        public void Sumar(int noDeAnos, Persona persona)//aqui esta la referencia, en persona, que es juan, cuando se envia un valor.
        {
            persona.Edad = noDeAnos + persona.Edad;
            
        }
        public void Sumar(int noDeAnos, int edad)//aqui no hay persona, sino simplemente un valor de 10.
        {
            edad = noDeAnos + edad;
        }
        public void SumarUno(object objeto)
        {
            objeto = "text";

        }
        public void SumarUno(int objeto)
        {
            objeto=objeto + 1;
        }
    }
    
}
