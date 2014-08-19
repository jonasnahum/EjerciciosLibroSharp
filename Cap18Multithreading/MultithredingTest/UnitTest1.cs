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
        {//quite las // para que pueda ir viendo los ejemplos en ejecucion.
            Examples example = new Examples();
            //example.TaskUsage();//como el metodo Task pide ayuda de otro thread , se demuestra en el output el trabajo alternado entre dos threads. 

            //example.TaskReturningValue();//es un Task<string>, se crea un nuevo hilo que va a cierto metodo y cuando lo termina retorna un resultado, en este caso, de tipo string.
            //example.TaskReturningValueStatus();//retorna cuando esta ha sido created, cuando is running y RanToCompletion.
            //example.NotificationsContinueWith();//encadena tasks. invica cual va a continuar despues de un task padre y bajo que condiciones.
            //example.ExeptionHandling();//manejo con try catch y explicacion de bubbling.
            //example.ExeptionHandlingContinueWith();//manejo de exepcion con continue.
            //example.CancellingTask();//cancelacion de un thread a travez de un Token.
            //example.LongRunningTask();//un parametro que se puede pasar para indicarle que la tarea se va a tardar y que considere si se crea un thread o se saca del pool, lo que se evalue pertinente.
            //example.ParallelIterations();//utiliza varios threads para ejecutar un loop, pero hay riesgo de hacer un race condition.,ademas, puede ser que unos hilos terminen primero que otros.
            //example.ParallelIterationsExceptions();//como cachar una exepcion en parallel.
            //example.ParallelLoopCancelation();//una variable va afuera y envia un mensaje de cancelacion a otra variable que va dentro del parallel loop , estas dos variables se comunican y entonces se cancela el loop.
            example.ParallelOptions();//se le puede poner un break al loop y tambien otra opcion es indicarle con cuantos hilos de ejecucion se va ayudar el loop.
        }
    }
}
