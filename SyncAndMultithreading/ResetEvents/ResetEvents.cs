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
        static ManualResetEventSlim MainSignal;//variable estatica de tipo Manual.
        static ManualResetEventSlim DoWorkSignal;

        public static void DoWork() //este metodo no empieza sino hasta que task se viene a el.
        {
            Debug.Print("DoWork iniciado");
            DoWorkSignal.Set();//ya acabe.
            MainSignal.Wait();//espera a que alguien le ponga set a la variable MainSignal.
            Debug.Print("DoWork Terminado");
        }
        public static void Main()//digamos que este es el hijo principal de aqui se divide en 2 threads.
        {
            using (MainSignal = new ManualResetEventSlim())//se crea la instancia se va a guardar en la variable. el using es para que el garbage collector mande llamar el dispouse. 
            {
                using (DoWorkSignal = new ManualResetEventSlim())//using es para el dispouse, para que si sale una exception o termina lo que esta entre {}, para el garbage collector lo mas rapido posible y se liberen los recursos.
                {
                    Debug.Print("Programa Iniciado");
                    Debug.Print("Iniciando Tarea Nueva");
                    Task task = Task.Factory.StartNew(DoWork);//ahora si empieza el metodo DoWork con otro thread.
                    DoWorkSignal.Wait();//esperar hasta que pongan set en la variable DoWorkSignal. y lo hacen en el metodo DoWork.
                    Debug.Print("ejecutando thread");
                    MainSignal.Set();//ya acabe. le avisa a la variable Mainsignal.Wait() que esta en el Metodo DoWork() que continue.
                    task.Wait();//esperar a que termite todo el hijo task.
                    Debug.Print("thread terminado");//aqui ya va el hilo principal.
                    Debug.Print("terminando programa");

                }
            }
        }
    }
}
