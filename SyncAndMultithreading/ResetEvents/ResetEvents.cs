using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResetEvents
{
    public class ResetEvents
    {
        static ManualResetEventSlim MainSignal;
        static ManualResetEventSlim DoWorkSignal;

        public static void DoWork() 
        {
            Debug.Print("DoWork iniciado");
            DoWorkSignal.Set();
            MainSignal.Wait();
            Debug.Print("DoWork Terminado");
        }
        public static void Main()
        {
            using (MainSignal = new ManualResetEventSlim()) 
            {
                using (DoWorkSignal = new ManualResetEventSlim())
                {
                    Debug.Print("Programa Iniciado");
                    Debug.Print("Iniciando Tarea Nueva");
                    Task task = Task.Factory.StartNew(DoWork);
                    //bloquear hasta que DoWork() ha iniciado.
                    DoWorkSignal.Wait();
                    Debug.Print("ejecutando thread");
                    MainSignal.Set();//set es como ya termine, ya puedes continuar. es el signal.
                    task.Wait();
                    Debug.Print("thread terminado");
                    Debug.Print("terminando programa");

                }
            }
        }
    }
}
