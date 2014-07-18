using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap10Exceptionhandling;

namespace Cap10Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void EjercicioArraysTest()
        {
            int longitud = 6;
            int[] result;
            IEjercicioArrays test = GetEjercico();//una variable de tipo Interface, que es igual a un metodo que regresa una clase.
            
            result = test.ObtenerArreglo(longitud);//la variable longitud la manda como parametro al metodo ObtenerArreglo y el resultado de este metodo lo asigna a la variable result.
            Assert.AreEqual(result.Length, longitud);//evalua si el resultado del metodo es igual a la variable local longitud difinida en esta clase.

            test.LlenarArreglo(result);//el array que regresa el metodo ObtenerArreglo, se manda como parametro.
            for (int i = 0; i < longitud; i++)
            {
                int valorEsperado = longitud - i;
                Assert.AreEqual(result[i], valorEsperado);//result[i], se mando como parametro al metodo en cuestion y cambio alli porque es un valor por referencia, entonces ya se puede comparar con valor esperado.
            }

        }

        public IEjercicioArrays GetEjercico()//metodo que regresa una, una instancia de la clase EjercicioArrays.
        {//hace cast implicito a la interface Interface IEjerciciosArrays.
            return new EjercicioArrays();//se crea la instancia con new.
        }
    }
}
