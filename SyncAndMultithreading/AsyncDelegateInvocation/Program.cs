using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDelegateInvocation
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("_____________APM EXAMPLE__________________________");

            Console.WriteLine("Application started....");
            Console.WriteLine("Starting thread....");
            Func<int, string> workerMethod = PiCalculator.Calculate;//el metodo se guarda en una variable tipo delegado.
            IAsyncResult asyncResult = workerMethod.BeginInvoke(1000, null, null);//que empiece a ejecutar en otro thread. gracias al IAsyncResult se monitorea el thread.//en este caso el numero 1000 es un parametro que se le pasa al metodo. o metodos, tambien pueden ser varios parametros. 
            
            // Display periods as progress bar.
            while (!asyncResult.AsyncWaitHandle.WaitOne(100, false))//se pone falso cuando el otro thread termina, entre tanto cada 100 milisec, pone el set para este wait.
            {
                Console.Write('.');
            }
            Console.WriteLine();
            
            Console.WriteLine("Thread ending....");
            Console.WriteLine(workerMethod.EndInvoke(asyncResult));//saca el resultado del thead que se envio a calculate.
            Console.WriteLine("Application shutting down....");

            Console.WriteLine("_____________EAP EXAMPLE__________________________");

            PiCalculator calc = new PiCalculator();
            CancellationTokenSource source = new CancellationTokenSource(1000);//se va cancelar a los 1 seg.
            calc.CalculateAsync<int>(2000, source.Token, null);
            calc.CalculateCompleted += ObtenerResultado;//cuando lanza el evento, para notificar los resultados cuando el thread termina.


            //sin cancelation token.
            PiCalculator calc1 = new PiCalculator();
            calc1.CalculateAsync(2000, null);
            calc1.CalculateCompleted += ObtenerResultado;//lanza el evento.

            Thread.Sleep(2000);

            Console.WriteLine("_____________BackgroundWorker EXAMPLE__________________________");
            BackgroundWorkerExample bg = new BackgroundWorkerExample("10000");//el numero 10000es para tardarse.
            bg.WorkerSupportsCancellation = true;//para cancelar se tiene que poner el flag en true.
            Thread.Sleep(2000);//esperar 2 segundos
            bg.CancelAsync();//cancelar.

            Console.ReadLine();
        }

        static void ObtenerResultado(object sender, CalculateCompletedEventArgs e)
        {
            if (e.Cancelled)
                Console.WriteLine("fue cancelado");
            else
                Console.WriteLine("{0}", e.Result);
        }
    }
    public class PiCalculator
    {
        public static string Calculate(int numero)
        {
            System.Threading.Thread.Sleep(numero);
            return "3.1416";
        }


        public void CalculateAsync(int digits) 
        {
            CalculateAsync(digits, null);
        }
        public void CalculateAsync(int digits, object state)
        {
            CalculateAsync<object>(digits, default(CancellationToken), state);
        }
        public void CalculateAsync<T>(int digits, CancellationToken token, object state)
        {
            if (SynchronizationContext.Current == null)
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            }
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task<string>.Factory.StartNew(() =>
            {
                return PiCalculator.Calculate(digits);
            }, token)
            .ContinueWith<string>(
            continueTask =>
            {
                CalculateCompleted(typeof(PiCalculator), 
                    new CalculateCompletedEventArgs(continueTask.Result,continueTask.Exception,token.IsCancellationRequested,state));
                
                return continueTask.Result;
            }, scheduler);
        }


        public event EventHandler<CalculateCompletedEventArgs> CalculateCompleted = delegate { };
    }

}
