using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cap8valuetypes;

namespace Capitulo8valuetypes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValueTypesTest()
        {
            int edad = 10;// tipo por valor.
            
            Persona juan = new Persona();//tipo por referencia.
            Persona pedro = juan;//son la misma referencia, los valores que cambie uno, los cambiara el otro.
            Persona pablo=new Persona();

            Assert.AreEqual(0, pedro.Edad);
            juan.Sumar(edad, juan);//le cambia la edad a todo el objeto, porque es por referencia.
            juan.Sumar(edad, juan.Edad);//no le cambia la edad, porque le manda una copia de la edad de juan, que es por valor.

            Assert.AreEqual(10, pedro.Edad);
            Assert.AreEqual(10, juan.Edad);
            Assert.AreEqual(10, juan.Edad);
            Assert.AreEqual(0, pablo.Edad);

            Assert.AreEqual(pedro, juan);
            Assert.AreNotEqual(pablo, juan);//son referencias, pero referencias distintas.

            pedro.Edad = 20;
            Assert.AreEqual(20, juan.Edad);
        }
        [TestMethod]
        public void BoxingTest()
        {
            object pedro = new Persona();
            object uno = 1;// este es por referencia, object pertenece a una clase general.
            int valoruno = 1;// este es por valor.
            
            Persona Juanito = new Persona();
            
            Juanito.SumarUno(valoruno);
            Assert.AreEqual(1, valoruno);//como es por valor, hace una copia y en el metodo de la clase persona, se hace la suma=2, pero aqui, esta variable sigue siendo 1.
            
            Juanito.SumarUno(uno);
            //Assert.AreEqual(2, uno);


            
        }
        [TestMethod]
        public void EnumTest()
        {
            UnaPalabra escoger = new UnaPalabra();
            string palabra = escoger.QueEmpieceCon(Vocales.a);
            Assert.AreEqual("avion", palabra);

            Vocales vocal = (Vocales)Enum.Parse(typeof(Vocales), "a");
            Assert.AreEqual(Vocales.a, vocal);
        }
    }
}
