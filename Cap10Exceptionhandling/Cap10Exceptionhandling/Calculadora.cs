using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Exceptionhandling
{
    public class Calculadora
    {
        public int Dividir(int a, int b)
        {
            return a / b;
 
        }
        public int DividirSeguro(int a, int b)
        {
            int resultado = 0;
            try// aque va lo que probablemente lance una exception.
            {
                resultado = a / b;
                Exception jonas= new Exception(" la excepcion de jonas");//este es un objeto de tipo exception.. lo que se le pasa al constructor entre comillas, se guarda en la propiedad Message del objeto.
                throw jonas;//se lanza la excepcion.
            }
            catch (DivideByZeroException e)// lo que debe hacer cuando encuentre una exencpion. handler.
            {
                Debug.Print(string.Format("Ocurrio una excepcion de tipo({0}, {1})", e.ToString(), e.Message));// e. toString(), indicar el fully quallyfied name.
            }
            catch (Exception e)//recibe un objeto de tipo exception, en este caso jonas. este es el tipo mas general de las excepciones, todos derivan de este. 
            {
                Debug.Print(string.Format("Ocurrio el siguiente error: {0}",e.Message));//el mensaje es el que esta cuando se hizo el objeto.

            }
            return resultado;
        }
    }
}
