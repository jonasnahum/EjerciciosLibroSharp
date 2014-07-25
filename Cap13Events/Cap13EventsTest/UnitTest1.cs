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
        //[TestMethod]
        //public void EventsTest()
        //{
        //    BoilerAutomatico boiler = new BoilerAutomatico();
        //    boiler.DespacharAguaInicio();
        //    Thread.Sleep(1000);//osea el timer va mandar llamar el metodo 10 veces, porque el timer actua cada 100 milisegundos.
        //    boiler.DespacharAguaFin();//se inhabilita el timer.
        //    Thread.Sleep(5000);
        //    boiler.DespacharAguaInicio();
        //    Thread.Sleep(5000);
        //    boiler.DespacharAguaFin();
        //    Console.WriteLine();
        //}
        [TestMethod]
        public void MetronomeTest()
        {
            MetronomeMachine m = new MetronomeMachine(10, 50);

            TextListener t = new TextListener();
            t.Subscribe(m);

            SoundListener l = new SoundListener();
            l.Subscribe(m);

            //m.Tick -= l.EmitSound; remover handler
            m.Start();
            
        }
        [TestMethod]
        public void EmisorDeEventosTest()
        {
            EmisorDeEventos e = new EmisorDeEventos();
            e.OnCreatedClass += (sender, args) => System.Diagnostics.Debug.Print("clase crada");//los metodos o handlers se registran en los eventos.
            e.OnMethodCalled += (sender, args) => System.Diagnostics.Debug.Print("se llamo el metodo");
            e.OnPropertyGet += (sender, args) => System.Diagnostics.Debug.Print("se solicito la Property y tiene valor de {0}", args.Value);
            e.OnPropertySet += (sender, args) => System.Diagnostics.Debug.Print(" se le asigno {0} a la property", args.Value);

            e.OnCreatedClass += a;//los metodos o handlers se registran en los eventos.
            e.OnMethodCalled += a;
            e.OnPropertyGet += a;
            e.OnPropertySet += a;

            e.Metodo();
            e.Property = 10;
            int valor = e.Property;
        }
        public void a(object sender, System.EventArgs args)
        {
            System.Diagnostics.Debug.Print("evento lanzado");
        }
    }
}
