using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap13Events;
using System.Threading.Tasks;
using System.Threading;

namespace Cap13EventsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EventsTest()
        {
            BoilerAutomatico boiler = new BoilerAutomatico();
            boiler.DespacharAguaInicio();
            Thread.Sleep(1000);
            boiler.DespacharAguaFin();
            Thread.Sleep(5000);
            boiler.DespacharAguaInicio();
            Thread.Sleep(5000);
            boiler.DespacharAguaFin();
            Console.WriteLine();
        }
    }
}
