using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap19;
using ResetEvents;  

namespace SyncAndMultithreading
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {LockWithMonitor examples = new LockWithMonitor();//uso de Monitor. para evitar un race condition, y para bloquear yna variable para que solo un hilo accese a ella a la vez.
           // examples.RaceCondition();
            LockWithLock lwl = new LockWithLock();
           // lwl.RaceCondition();//se dice que esta formula es una abreviacion de monitor.
            LockWithInterlocked masEficiente= new LockWithInterlocked();
            //masEficiente.RaceCondition();//mas eficiente que el lock, segun.
            //ResetEvents.ResetEvents.Main();//puede controlar los hilos a gusto, que se detengan y esperen al principal o vice versa.
            ////Mutex permite que solo un programa corra al mismo tiempo, no te deja abrir otro. un ejemplo se encuentra en la clase Program cs..remitase a el.
            //en la clase Timer de este Library o assembly hay un ejemplo de como manejar los eventos para hacerlos safe threading, solo hay que hacer una copia local del evento.
            //volatile, dice al cpu compilador que lo lea tal cual el codigo sin optimizacion interna cuando lea una  variable, volatile se esceibe antes de la variable. la optimizacion interna se refiere a que a veces se leen primero un codigo que otro.
            SemaphoreExample semaphore = new SemaphoreExample();
            //semaphore.Test();//en vez de haber set, hay un temporizador. que es lo que recibe el wait como parametro, el numero de waits esta limitado al numero que recibe en el constructor., ese numero son los threads que saca del pool.
            ThreadLocal threadLocal = new ThreadLocal();
            //threadLocal.RaceCondition();//crea una variable para para cada nuevo thread. no es sincronization es isolation.se evita el race condition.
            AislamientoThreadStatic Aila = new AislamientoThreadStatic();//otra forma de evitar el race condition.
            //Aila.RaceCondition();
            //TimerExample.Main();//el timer funciona en otro thread y ese otro threadse puede detener con un AutoResetEvent.
            //APM async programing .. va avisando el estado del thread, si ya acabo a travez de un handler, que en este caso, cada 100 milisec. evalua si ha acabado. visite la clase Program para un ejemplo.
            // remitase al namespace AsyncDelegateInvocation de este assembly, para ver un ejemplo de AsyncProgramInvocation, APM usando delegagos para monitorear los threads.
            //remitase al anterior para ver un ejemplo de Event Based Async Patern  que lanza un evento cuando termina otro thread.
            //tambien para ver un ejemplo de BackgroundWorker EXAMPLE para 2 trheads nomas, que es otro EAP ademas del status, se puede cancelar, marca errores, progreso, etc.
        }
    }
}
