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
            IArrays prueba = MetodoDePrueba();
            string textoParaEnviar = "hola";     
            char[] resultadoDelMetodo;
            char[] resultadoEsperado = new char[] {'h','o','l','a' };
            resultadoDelMetodo = prueba.ConvertirEnArray(textoParaEnviar);
            for(int i = 0; i < resultadoEsperado.Length; i++)
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
            return new Arrayjonas();

        }

    }
}
