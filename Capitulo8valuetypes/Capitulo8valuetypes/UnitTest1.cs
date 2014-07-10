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
            
            Persona juan = new Persona();//tipo por referencia., las clases y objetos son tipos por referencia.
            Persona pedro = juan;//son la misma referencia, los valores que cambie un objeto o variable, los cambiara el otro.
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
            Loncheria loncheria = new Loncheria();//se crea un objeto de loncheria.
            Torta hawaiana = (Torta)loncheria.PrepararComida(Menu.torta);//una variable de tipo torta es igual a loncheria.PrepararComida(Menu.torta), esto es de tipo loncheria, ahora entre parentesis, se le convierte a tipo torta para poderla guardar en la variable.
            Assert.                                                      //ahora hawaiana tiene todos los ingredientes de una torta.
        }
        [TestMethod]
        public void StructTest()
        {
            Persona Juan = new Persona();//se hace un objeto Juan
            Persona Pedro = Juan;//Juan y pedro, por referencia.
            PersonaStruct JuanStruct = new PersonaStruct();//se crea un objeto con las caracteristicas de struct.
            PersonaStruct PedroStruct = JuanStruct;//juanstruct se asigna a pedrostruct, pero no se altera pedro, porque se comporta como datos por valor y lo que tiene juanstruct, no se le pasa a pedrostruct.

            Juan.Nombre = "juan";
            Juan.Edad = 22;
            JuanStruct.Nombre = "juan";
            JuanStruct.Edad = 22;
            // los estruct son inmutables, siempre hacen una copia del dato no se afecta su valor por referencia.
            Assert.AreEqual("juan", Pedro.Nombre);//es por referencia, por lo tanto pedro se modifica.
            Assert.AreEqual(22, Pedro.Edad);
            Assert.IsNull(PedroStruct.Nombre);//es null porque juan struct se comporta como tipo por valor y no afecta a pedrostruct.
            Assert.AreEqual(0,PedroStruct.Edad);//es 0 porque cuando se inicializa la clase, todos los enteros se inicializan en 0, no copia el contenido de jaunstruct.
        }
    }
}
