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
        // un evento, es un tipo de delegado pero que puede guardar varios metodos.
        //los eventos, tienen una parte en donde se declara el evento, otra en donde se dispara el evento, otra en donde se suscriben los handlers al evento con +=, mismos que a su vez, son metodos que cumplen con el delegado del evento, cuando se lanza un evento, se tienen 2 parametros, generalmento el object sender, que generalmente es this, y que se refiere a la clase que envia el evento, y Args, que es una clase que puede derivar de EventArgs y que es donde se guardan los parametros que se quieren enviar a los subscritos al evento, se guardan como variables en esta clase. otra cosa es que antes de enviar un evento, hay que checar que sea null.
        public void MetronomeTest()
        {
            MetronomeMachine m = new MetronomeMachine(10, 50);//se crea instancia,(contador, duracion).

            TextListener t = new TextListener();// se crea una instancia, esta escucha a metronememachine e imprime en pantalla la duracion y frecuencia.
            t.Subscribe(m);//El este metodo recibe un MetronomeMachine y a éste, al evento que se encuentra en el objeto, le asigna una variable, un metodo que hace imprime en pantalla la duracion y frecuencia.

            SoundListener l = new SoundListener();//se crea una instancia.
            l.Subscribe(m);//este metodo le asigna recibe un metronomemachine, busca su evento y le suscribe una variable que a su vez es un metodo que emite un beep cada vez que se displara el evento en la clase metronomemachine.

            //m.Tick -= l.EmitSound; remover handler
            m.Start();//dispara el evento.
            
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
            e.OnMethodCalled += a;//este bloque son otro metodo que se asigna otra vez al mismo evento, para demostrar que se pueden registrar varios metodos en un solo evento.
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
