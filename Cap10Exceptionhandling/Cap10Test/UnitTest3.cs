using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap10Exceptionhandling;

namespace Cap10Test
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void ArrayMethod()
        {
            IArrays prueba = MetodoDePrueba();//un objeto guardado en variable prueba que tiene las propiedades de IArray, sus metodos. una variable prueba de tipo IArrays a la que se le asigna el metodo que regresa la clase ArrayJonas(). es como hacer un objeto de la ArrayJonas que implementa la interface.
            string textoParaEnviar = "hola";     
            char[] resultadoDelMetodo;
            char[] resultadoEsperado = new char[] {'h','o','l','a' };
            resultadoDelMetodo = prueba.ConvertirEnArray(textoParaEnviar);
            for(int i = 0; i < resultadoEsperado.Length; i++)//los Assert.AreEqual en array se comparan ási.
            {
                Assert.AreEqual(resultadoEsperado[i], resultadoDelMetodo[i]);
            }
            

            
            string[] arrayDePalabrasParaEnviar=new string []{"jonas","nahum","jimenez","garcia"};
            int[] resultadoMetodoContar = prueba.ContarLetrasEnpalabras(arrayDePalabrasParaEnviar);
            int[] yoEspero = new int[] { 5, 5, 7, 6 };
            for (int i = 0; i < yoEspero.Length; i++)
            {
                Assert.AreEqual(yoEspero[i], resultadoMetodoContar[i]);                
            }



            char[,] arrayParaEnviar = new char[3, 3]{
                                                 {'X','O','O'},
                                                 {'O','X','O'},
                                                 {'O','O','X'}
                                                             };
            char resultadoMetodo = prueba.LetraGanadora(arrayParaEnviar);
            char resultadoEsperadoGato = 'X';
            Assert.AreEqual(resultadoEsperadoGato, resultadoMetodo);

        }
        public IArrays MetodoDePrueba()
        {
            return new Arrayjonas();//cuando no hay nada en la clase, throw new not implementedexception();

        }

    }
}
