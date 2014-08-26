using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncDelegateInvocation
{//e. trae informacion del evento... si fue cancelado, el progreso, etc.
    public class BackgroundWorkerExample: BackgroundWorker//hereda de esta clase.
    {
        public static AutoResetEvent resetEvent;//tiene wait y set
        public BackgroundWorkerExample(string tiempoDeCalculacion):base()
        {
            
            DoWork += CalculatePi;//se registra el long running method.
            ProgressChanged += ReportarAvance;
            WorkerReportsProgress = true;
            RunWorkerCompleted += MostrarResultado;//el metodo que se ejecuta cuando termina el long runnin method.
            RunWorkerAsync(tiempoDeCalculacion);//indica cuando se cancelará el long runnin method.//aqui se indica que ya va empezar el thread en DoWork.Se lanza un evento de manera interna.
        }

        private void MostrarResultado(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) //manejo de errores.
            {
                Console.WriteLine("EROR: {0}", e.Error.Message);
                return;//el return en el void es para que deje de hacer algo y se ssalga que hasta aqui llegue.
            }
            if (e.Cancelled)
            {
                Console.WriteLine("Cancelado");
            }
            else
            {
                Console.WriteLine(e.Result);
            }
        }
        void ReportarAvance(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("{0}% terminado", e.ProgressPercentage);
        }
        void CalculatePi(object sender, DoWorkEventArgs e)
        {
            int digitCount = int.Parse(e.Argument.ToString());

            int decimaParte = digitCount / 10;
            int contadorPorcentaje = -10;

            using (resetEvent = new AutoResetEvent(false))
            {
                while (!resetEvent.WaitOne(decimaParte))//digitCount=1 0000, decimaParte =1000ms. =1 seg. el resetevent nomas se uso para hacerle wait cada determinado tiempo.
                {
                    if (CancellationPending)//check the DoWorkEventArgs.CancellationPending property and exit the method when it is true.
                    {
                        e.Cancel = true;
                        break;
                    }
                    contadorPorcentaje += 10;//aumentar de 10 en 10.
                    ReportarAvance(this, new ProgressChangedEventArgs(contadorPorcentaje, null));//mandar un evento

                    if (contadorPorcentaje > 90)//hasta 100.
                    {
                        break;
                    }
                }
            }
            
            e.Result = "3.1416";

        }
//    Establishing the Pattern
//The process of hooking up the background worker pattern is as follows:
//1. Register the long-running method with the BackgroundWorker.
//DoWork event. In this example, the long-running task is the call to
//CalculatePi().
//2. To receive progress or status notifications, hook up a listener to
//BackgroundWorker.ProgressChanged and set Background-
//Worker.WorkerReportsProgress to true. In Listing 19.8, the
//UpdateDisplayWithMoreDigits() method takes care of updating
//the display as more digits become available.
//3. Register a method (Complete()) with the BackgroundWorker.
//RunWorkerCompleted event.
//4. Assign the WorkerSupportsCancellation property to support
//cancellation. Once this property is assigned the value true,
//a call to BackgroundWorker.CancelAsync will set the
//DoWorkEventArgs.CancellationPending flag.
//5. Within the DoWork-provided method (CalculatePi()), check the
//DoWorkEventArgs.CancellationPending property and exit the
//method when it is true.
//6. Once everything is set up, you can start the work by calling BackgroundWorker.
//RunWorkerAsync() and providing a state parameter
//that is passed to the specified DoWork() method.
//When you break it into steps, the background worker pattern is relatively
//easy to follow and, true to EAP, it provides explicit support for progress
//notification. The drawback is that you cannot use it arbitrarily on any
//method. Instead, the DoWork() method has to conform to a System.ComponentModel.
//DoWorkEventHandler delegate, which takes arguments of type
//object and DoWorkEventArgs. If this isn’t the case, then a wrapper function
//is required—something fairly trivial using anonymous methods. The cancellation-
//and progress-related methods also require specific signatures,
//but these are in control of the programmer setting up the background
//worker pattern.
    }
}
