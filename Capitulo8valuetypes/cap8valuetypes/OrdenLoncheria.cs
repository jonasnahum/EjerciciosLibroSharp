using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap8valuetypes
{
    public class Loncheria
    {
        public Comida PrepararComida(Menu orden)//metodo que regresa un valor de tipo Comida(Class) y recibe un dato de tipo Menu(Enum).
        {
            Comida comida = null;//variable de tipo comida.
            switch (orden)
            {
                case Menu.torta://en caso de que la orden sea Menú.torta.
                    comida= HacerTorta();//la variable comida de tipo comida es igual al metodo de esta clase HacerTorta(), mismo que es de tipo Torta(Class), y que tiene su propia clase que deriva de la base Comida.
                    break;
                case Menu.sandwich:
                    comida = HacerSandwich();//variable comida es igual al metodo de tipo Sandwich que a su vez hereda de comida.
                    break;
                case Menu.Licuado:
                    comida = HacerLicuadoDeFresa();//metodo que al final viene siendo de tipo comida.
                    break;
                default:
                    comida = null;
                    break;

            }
            return comida;//regresar la variable comida.
 
        }
        private Torta HacerTorta()//em esta clase, tambien se tiene la funcion HacerTorta(), de tipo Torta que hereda de Comida.
        {
            Torta cubana = new Torta();//se crea un objeto de tipo Torta.
            cubana.Ingredientes = ListaDeIngredientes.bolillo |// .Ingredientes,  la propiedad que pertenece a la Clase base de Torta(Comida) y que es de tipo ListaDeIngredientes(Enum).
                                ListaDeIngredientes.cebolla |
                                ListaDeIngredientes.jamon |
                                ListaDeIngredientes.jitomate |
                                ListaDeIngredientes.lechuga |
                                ListaDeIngredientes.salsa;
            return cubana;//regresa objeto de tipo Torta y que a su vez, deriva de Comida.


        }
        private Sandwich HacerSandwich()//metodo de tipo Sandwich, que a su vez deriva de Comida.
        {
            Sandwich sandwich = new Sandwich();//se crea un objeto de la clase Sandwich.
            sandwich.Ingredientes = ListaDeIngredientes.panBimbo |//se manda llamar la propiedad Ingredientes que es de tipo ListaDeIngredientes(enum)
                                  ListaDeIngredientes.lechuga |
                                  ListaDeIngredientes.jitomate |
                                  ListaDeIngredientes.jamon |
                                  ListaDeIngredientes.cebolla;
            return sandwich;//regresa un objeto de tipo Sandwich, que a su vez, hereda de la clase Comida.
        }
        private Licuado HacerLicuadoDeFresa()//metodo de que va regresar un tipo Licuado, que a su vez hereda de la clase Comida.
        {
            Licuado DeFresa = new Licuado();//se crea un objeto de la clase Licuado, no dentro de la clase, sino dentro de un metodo de su tipo.
            DeFresa.Ingredientes = ListaDeIngredientes.fresa |//de fresa objeto, su propiedad Ingredientes, de tipo ListaDeIngredientes se pone un punto y se elige el ingrediente, y luego el signo de OR, puesto que funciona con bytes.
                                 ListaDeIngredientes.leche;
            return DeFresa;//regresa un dato de tipo Licuado que a su vez hereda de Comida.
        }
    }
    
    public class Comida//clase comida, que hereda a Sandwich, Torta y Licuado.
    {
        public ListaDeIngredientes Ingredientes{get;set;}//Lista de Ingredientes es un enum, e Ingredientes una propiedad de tipo ListaDeIngredientes.
    }
    public enum Menu//un listado que va al metodo PrepararComida de la clase Loncheria, y ahi se convierte en orden, solo se puede ordenar 3 cosas.
    {
        torta,
        sandwich,
        Licuado//la ultima coma no se escribe.
    }
    [Flags]//un listado del que se pueden escoger varios elementos, salteados.
    public enum ListaDeIngredientes
    {
        jitomate =1<<0,//el uno se pone para que el numero binario se recorra 0 posiciones.
        cebolla = 1 << 1,//el numero binario se recorre 1 posicion.
        lechuga = 1 << 2,
        salsa = 1 << 3,
        leche = 1 << 4,
        fresa = 1 << 5,
        jamon = 1 << 6,
        panBimbo = 1 << 7,
        bolillo = 1 << 8
    }
    public class Torta : Comida//estas clases son al fin de tipo Comida tambien.
    {                           //es decir, a torta, sandwich y licuado, se les puede llamar su Lista de Ingredientes.
 
    }
    public class Sandwich : Comida
    {
 
    }
    public class Licuado : Comida 
    {
    }
}
