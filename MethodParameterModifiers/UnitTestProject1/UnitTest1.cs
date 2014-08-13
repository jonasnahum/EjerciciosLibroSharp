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
            
            examples.RefExample(ref a);//ref hace un tipo por referencia, almenos por este metodo.
            min = examples.MaximoMinimo(out max, 2, 1, 0, 6, 4, 90);//out te obliga a signar un valor dentro del metodo, tambien es por referencia.

            Assert.AreEqual(10, a);
            Assert.AreEqual(0, min);
            Assert.AreEqual(90, max);

            int[] calificaciones = new int[] { 10, 9, 10 };
            Assert.AreEqual(29 / 3, calificaciones.Promedio());

            Assert.AreEqual(10, examples.Edad(2004));
            Assert.AreEqual(30, examples.Edad());
        }
    }
}
