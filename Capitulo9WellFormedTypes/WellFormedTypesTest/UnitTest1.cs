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

        [TestMethod]
        public void GarbageCollectorTest()
        {
            //Estudiante estudiante = new Estudiante();
            //estudiante.Leer();
            //estudiante.Leer();
           
        }

        [TestMethod]
        public void GarbageCollectorTest1()
        {
            Diccionario d = null;
            try
            {
                //Estudiante estudiante = new Estudiante();
                //estudiante.Leer();
                d = new Diccionario();
                int error = int.Parse("hola");
                string palabra = d.Palabras[1000].Valor;
            }
            catch (Exception e)
            {

            }
            finally 
            {
                d.Dispose();//libera los recursos
            }

            //using (Diccionario d1 = new Diccionario(), d2 = new Diccionario())//una abreviacion de lo anterior, en este caso, si marca o no marca error lo que esta adentro, finalmente se mandara llamar el metodo dispose de la interface Disposable. las clases deben implementar Idisposable, y si son varios, separados por coma, poniendo el tipo al principio.
            //{
            //    int error = int.Parse("hola");
            //    string palabra = d1.Palabras[1000].Valor;
            //}
        }
        [TestMethod]
        public void Lazyloading()
        {
            LectorDiccionario lector = new LectorDiccionario();
            lector.Leer();
            string hola = "hola";
        }
        
    }
}
