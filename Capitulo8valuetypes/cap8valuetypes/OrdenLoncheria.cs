using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap8valuetypes
{
    public class Loncheria
    {
        public Comida PrepararComida(Menu orden)
        {
            Comida comida = null;
            switch (orden)
            {
                case Menu.torta:
                    comida= HacerTorta();
                    break;
                case Menu.sandwich:
                    comida = HacerSandwich();
                    break;
                case Menu.Licuado:
                    comida = HacerLicuadoDeFresa();
                    break;
                default:
                    comida = null;
                    break;

            }
            return comida;
 
        }
        private Torta HacerTorta()
        {
            Torta cubana = new Torta();
            cubana.Ingredientes = ListaDeIngredientes.bolillo |
                                ListaDeIngredientes.cebolla |
                                ListaDeIngredientes.jamon |
                                ListaDeIngredientes.jitomate |
                                ListaDeIngredientes.lechuga |
                                ListaDeIngredientes.salsa;
            return cubana;


        }
        private Sandwich HacerSandwich()
        {
            Sandwich sandwich = new Sandwich();
            sandwich.Ingredientes = ListaDeIngredientes.panBimbo |
                                  ListaDeIngredientes.lechuga |
                                  ListaDeIngredientes.jitomate |
                                  ListaDeIngredientes.jamon |
                                  ListaDeIngredientes.cebolla;
            return sandwich;
        }
        private Licuado HacerLicuadoDeFresa()
        {
            Licuado DeFresa = new Licuado();
            DeFresa.Ingredientes = ListaDeIngredientes.fresa |
                                 ListaDeIngredientes.leche;
            return DeFresa;
        }
    }
    
    public class Comida
    {
        public ListaDeIngredientes Ingredientes{get;set;}
    }
    public enum Menu
    {
        torta,
        sandwich,
        Licuado//la ultima coma no se escribe.
    }
    [Flags]
    enum ListaDeIngredientes
    {
        jitomate =1<<0,
        cebolla = 1 << 1,
        lechuga = 1 << 2,
        salsa = 1 << 3,
        leche = 1 << 4,
        fresa = 1 << 5,
        jamon = 1 << 6,
        panBimbo = 1 << 7,
        bolillo = 1 << 8
    }
    public class Torta : Comida
    {
 
    }
    public class Sandwich : Comida
    {
 
    }
    public class Licuado : Comida 
    {
    }
}
