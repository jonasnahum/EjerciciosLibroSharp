using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsCap11
{
    public class AutoBase 
    {
        public int NoDePuertas { get; set; }
        public string Color { get; set; }
    }
    public class Tsuru:AutoBase{}
    public class Camioneta : AutoBase { }
    
    public class Moto 
    {
        public int NoDePuertas { get; set; }
        public string Color { get; set; }
    }

    public class Camion:AutoBase
    {
        public Camion(int p, string c) 
        {
            NoDePuertas = p;
            Color = c;
        }
        
    }

    public class FabricaDeCarros
    {
        public T ConstruirCarro<T>(string color, int puertas) where T : AutoBase, new()//constrint obliga a que T herede de Autobase y tenga un constructor vacio osea new(), para poder hacer objetos en el cuerpo de este metodo.
        {
            T  auto = new T();
            auto.Color = color;
            auto.NoDePuertas = puertas;
            return auto;

        }
    }
    
    
}
