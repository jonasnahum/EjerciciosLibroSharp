using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodParameterModifiers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MethodExamples examples = new MethodExamples();
            int a = 8;
            int max = 0;
            int min = 0;
            
            examples.RefExample(ref a);//ref hace un tipo por referencia, almenos durante este metodo.this a vale entonces 10.
            min = examples.MaximoMinimo(out max, 2, 1, 0, 6, 4, 90);//out te obliga a signar un valor dentro del metodo, tambien es por referencia. es decir , el metodo tiene su valor de retorno, pero tambien tiene que retornar out, serian entonces 2 valores de retorno.

            Assert.AreEqual(10, a);
            Assert.AreEqual(0, min);
            Assert.AreEqual(90, max);

            int[] calificaciones = new int[] { 10, 9, 10 };
            Assert.AreEqual(29 / 3, calificaciones.Promedio());//a la variable calificaciones, se le aplica un metodo estatico.

            Assert.AreEqual(10, examples.Edad(2004));
            Assert.AreEqual(30, examples.Edad());//pone el parametro por default , el que se le indico en el metodo.
        }
    }
}
