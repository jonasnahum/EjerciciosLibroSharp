using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capitulo9WellFormedTypes;

namespace WellFormedTypesTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            Persona persona = new Persona();
            persona.Nombre = "Juan";
            persona.Apellido = "Perez";
            Mascota mascota = new Mascota();
            Assert.AreEqual("Capitulo9WellFormedTypes.Mascota",mascota.ToString());//fully qualifyed name. en el caso de las clases, te regresa este dato, si no se hace override a tostring.
            Assert.AreEqual("1", 1.ToString());//aqui se usa el tostring que viene de object y que se hereda por default.
            Assert.AreEqual("Juan Perez", persona.ToString());//utilizando override en un metodo de persona class, te regresa una representacion en texto.

            
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            Palabra palabra = new Palabra();
            palabra.Valor = "test";
            Palabra p = palabra;
            Assert.AreEqual(palabra.GetHashCode(), p.GetHashCode());
        }

        [TestMethod]
        public void EqualsTest()
        {
            Palabra p1 = new Palabra();
            p1.Valor = "hola";
            Palabra p2 = new Palabra();
            p2.Valor = "hola.";
            Assert.IsTrue(p1.Equals(p2));
            Assert.IsFalse("hola".Equals("hola."));
       
        }

        [TestMethod]
        public void EqualsOperatorTest()
        {
            Palabra p1 = new Palabra();
            p1.Valor = "hola";
            Palabra p2 = new Palabra();
            p2.Valor = "hola.";
            Palabra p3=new Palabra();
            p3.Valor="hello";
            Assert.IsTrue(p1 == p2);
            Assert.IsTrue(p1 != p3);
            Assert.IsFalse(p1 != p2);
        }

    }
}
