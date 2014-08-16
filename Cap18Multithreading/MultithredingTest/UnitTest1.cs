using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multithreding;

namespace MultithredingTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Examples example = new Examples();
            //example.TaskUsage();//como el metodo Task pide ayuda de otro thread , se demuestra en el output el trabajo alternado entre dos threads. 

            //example.TaskReturningValue();//es un Task<string>, se crea un nuevo hilo que va a cierto metodo y cuando lo termina retorna un resultado, en este caso, de tipo string.
            //example.TaskReturningValueStatus();//retorna cuando esta ha sido created, cuando is running y RanToCompletion.
            //example.NotificationsContinueWith();//encadena tasks. invica cual va a continuar despues de un task padre y bajo que condiciones.
            //example.ExeptionHandling();//manejo con try catch y explicacion de bubbling.
            //example.ExeptionHandlingContinueWith();//manejo de exepcion con continue.
            //example.CancellingTask();//cancelacion de un thread a travez de un Token.
            //example.LongRunningTask();
            //example.ParallelIterations();
            //example.ParallelIterationsExceptions();

            //example.ParallelLoopCancelation();
            example.ParallelOptions();
        }
    }
}
