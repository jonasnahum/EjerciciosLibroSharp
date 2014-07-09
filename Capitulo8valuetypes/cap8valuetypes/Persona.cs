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
        public void Sumar(int noDeAnos, int edad)//aqui no hay persona, no hay objeto, ni referencia. sino simplemente un valor de 10.esto es un tipo por valor.
        {
            edad = noDeAnos + edad;//aqui recibe la variable edad. y Juan.Edad,  y las asigna a edad, aqui en este metodo si hace la operacion y el resultado es 20, sin embargo la variable edad que se encuentra en UnitTest, no cambia, porque es un tipo por valor.
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
    public struct PersonaStruct// estruct es una clase, que se comporta como un tipo por valor
    {
        public int Edad { get; set; }
        public string Nombre { get; set; }

    }
}
