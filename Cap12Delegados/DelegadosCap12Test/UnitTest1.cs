using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap12Delegados;

namespace DelegadosCap12Test
{
    [TestClass]
    public class UnitTest1
    {
        public decimal PrecioPaLosCompas(Pedido pedido)
        {
            return pedido.Garrafones * 9;
        }
        [TestMethod]
        public void TestMethod1()
        {
            Pedido ped = new Pedido();
            ped.Garrafones = 10;
            CalculadoraDePrecios cdp = new CalculadoraDePrecios(10M, 1.16M);//se le manda precio unitario e iva.
            decimal total = ped.CalcularTotal(cdp.CalcularPrecio);//CalcularTotal es un metodo que recibe como parametros otro metodo que cumple con la firma del delegado, que regrese un decimal y reciba un pedido.//CalcularPrecio es como una variable cuando no se le ponen los parentesis.
            Assert.AreEqual(116, total);


            Pedido Nahum = new Pedido();
            Nahum.Garrafones = 5;
            decimal total2 = Nahum.CalcularTotal(PrecioPaLosCompas);
            Assert.AreEqual(45, total2);
        }
        public void ImprimirEnConsola(string a) 
        {
            System.Diagnostics.Debug.Print(a);
        }
        [TestMethod]
        public void EjemplosDelegados()
        {
            DelegadosEjemplo ejemplo = new DelegadosEjemplo();
            ejemplo.M1 = delegate(int x, int y) // delegate suple al nombre del metodo, este es un delegado anonimo, no tiene nombre, y hace su implementacion.
            { 
                return x + y; 
            };
            ejemplo.M2 = delegate(string a, string b)//aqui es un metodo declarado inline, no se mandan valores, adem[as es un metodo anonimo.cumple con el delegado de M2.
            {
                return a + b;
            };//lleva ; porque es una sola linea toda la implementacion.
            ejemplo.M3 = ImprimirEnConsola;

            int resultadoSuma= ejemplo.M1(10, 12);
            string resultString=ejemplo.M2("jonas", "nahum");
            ejemplo.M3("Hola");

            Assert.AreEqual(22, resultadoSuma);
            Assert.AreEqual("jonasnahum", resultString);
        }
    }
}
