using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Multithreding
{
    public class PiCalculator 
    {       
        public static string Calculate() //regresa un string despues de 5 segundos.
        {
            Thread.Sleep(5000);
            return "El valor de PI";
        }

        public static IEnumerable<char> BusySymbols() 
        {
            string busysymbols = @"-\|/-\|/";
            int next = 0;
            while (true)//osea mientras(siempre).
            {
                yield return busysymbols[next];//regresar lo que hay en el indice 0, string se comporta como un array.
                next = (++next) % busysymbols.Length;//mod, se refiere a lo que sobra de una division. en este caso next es igual a uno, solo hasta que se alcanza la divicion 8/8=1 y sobran 0 , se lo asigna a next y vuelve a empezar. 
                yield return '\b';
            }
        }
    }
    public class Examples
    {
        public void TaskUsage()//crea un nuevo thread 
        {
            const int repetition = 10000;//const se refiere a una variable que no va a cambiar.
            Task newThread = new Task(() =>//se crea un thread se saca del pool, y si no hay en el pool, se crea uno nuevo.
                {
                    for (int count = 0; count < repetition; count++)
                    {
                        System.Diagnostics.Debug.Print("-");
                    }
                });
            newThread.Start();//se inicia un nuevo tread que se creo newThread y empieza a iterar, mientras el trhead normal sigue su curso debajo de esta linea.
            for (int count = 0; count < repetition; count++)
            {
                System.Diagnostics.Debug.Print(".");
            }
            newThread.Wait();//el thread que llegue aqui va esperar a que termine newThread y continua un solo thread.
        }


        public void TaskReturningValue()
        {
            Task<string> otroHilo = Task.Factory.StartNew(//se construye otro thread que regresa un string y se inicia en la misma linea.
                () => PiCalculator.Calculate());//aqui empiza a trabajar otro thread llamado otroHilo. Calculate() es un metodo que regresa un string.

            foreach (char busySymbol in PiCalculator.BusySymbols())//imprime cada busy symbol hasta que otroHilo is completed.
            {
                if (otroHilo.IsCompleted)//cuando otrohilo termina, is complete se vuelve true. y pone break.
                {
                    System.Diagnostics.Debug.Print("\b");
                    break;//break termina el foreach.
                }
                System.Diagnostics.Debug.Print(busySymbol.ToString());
            }
            System.Diagnostics.Debug.Print("------------------Resultado------------------");
            System.Diagnostics.Debug.Print(otroHilo.Result);//el Result guarda el valor de retorno de la funcion que recibio el thread.
            System.Diagnostics.Trace.Assert(otroHilo.IsCompleted);
        }
        public void TaskReturningValueStatus()
        {

            Task otroHilo = new Task(
                () =>
                {
                    System.Diagnostics.Debug.Print("Current ID {0}-------------------", Task.CurrentId);//vale uno pero hasta que se le aplica Start vale 1.
                    PiCalculator.Calculate();

                });
            System.Diagnostics.Debug.Print("Hilo id {0}-------------------", otroHilo.Id);//id=1
            System.Diagnostics.Debug.Print(otroHilo.Status.ToString());//status:created
         
            otroHilo.Start();
            System.Diagnostics.Debug.Print(otroHilo.Status.ToString());//status:running.
            while (true)//el thread principal se queda en este ciclo, no tiene id. mientras el id1 se inicia.
            {
                if (otroHilo.IsCompleted)//cuando otrohilo termina, is complete se vuelve true. y pone break.
                {
                    System.Diagnostics.Debug.Print(otroHilo.Status.ToString());//RanToCompletion.
                    break;//break termina el foreach.
                }

            }

        }
        public void NotificationsContinueWith() //encadena tasks
        {
            Task<string> task = Task.Factory.StartNew<string>(() => PiCalculator.Calculate());

            Task faultedTask = task.ContinueWith(
                (antecedentTask) =>//antecedentTask es un parametro de este lambda y se refiere a task.//y el predicado se va ejecutar solo si el thread de task falla, lo cual no va a pasar, porque no hay exeptionss.
                {
                    Trace.Assert(task.IsFaulted);
                    Debug.Print("Task State: Faulted");
                }, TaskContinuationOptions.OnlyOnFaulted);//onlyonfaulted, se refiere a task.

            Task canceledTask = task.ContinueWith(//canceledTask va contunuar despues de task, pero solo si es cancelada.
                (antecedentTask) =>
                {
                    //Trace.Assert(task.IsCanceled);
                    Debug.Print("Task State: Canceled");
                }, TaskContinuationOptions.OnlyOnCanceled);//solo que task se cancele.

            Task completedTask = task.ContinueWith(
                (antecedentTask) => //este lambda recibe como parametro task, que es el atecentTask.
                {
                    Debug.Print("{0}---id de task--", task.Id);//id=2
                    Debug.Print("{0}---id de antecedentTask--", antecedentTask.Id);//id=2 es el mismo thread antecedentTask y task.
                    Debug.Print("{0}---id de Current--", Task.CurrentId);//id=3.


                    Trace.Assert(task.IsCompleted);
                    Debug.Print("Task State: Completed");
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            completedTask.Wait();
        }
        public void ExeptionHandling()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                throw new ApplicationException();//aqui se lanza la excepcion, pero no pasa nada hasta wait.
            });

            try
            {
                task.Wait();//cuando task completa la tarea, se lanza la exepcion, y hace bubbling porque el tread de task se pasa al hijo actual.
            }
            catch (AggregateException exception)//un tipo de dato que guarda las excepcion de todos los threads creados por el thread actual y que lanzaron una excepcion.
            {
                foreach (Exception item in exception.InnerExceptions)
                {
                    Debug.Print("ERROR: {0}", item.Message);
                }
            }
        }
        public void ExeptionHandlingContinueWith() //intercambiable con try, catch.
        {
            bool parentTaskfaulted = false;
            Task task = new Task(() =>
                {
                    throw new ApplicationException();

                });

            Task faultedTask = task.ContinueWith(//se ejecuta solo si falla task que es parent task.
                (parentTask) =>
                {
                    parentTaskfaulted = parentTask.IsFaulted;// si parent task is  faulted, cambia bool parentTaskfaulted por true.
                }, TaskContinuationOptions.OnlyOnFaulted);
            
            task.Start();//empieza el hilo de task. y tira exception.
            faultedTask.Wait();//nadamas espera a faultedTask, porque no va a fallar y task si fallara., para continuar con un solo hilo de ejecucion.
            
            Trace.Assert(parentTaskfaulted);//es un assert que dice true.
            
            if (!task.IsFaulted)//si no falla task.
            {
                task.Wait();// WAIT nunca se ejecuta en dos casos: 1) Fault y 2) Cancelled
            }
            else//de lo contrario, si falla...
            {
                Debug.Print("ERROR: {0}", task.Exception.Message);
            }
        }

        public string Write(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Debug.Print("*");        
            }
            return "hola";//este return va despues del while.
        }

        public void CancellingTask()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            source.Token.Register(() => Debug.Print("Adios"));//con register, se manda llamar un metodo cuando source se cancela.

            Task<string> task = Task<string>.Factory.StartNew(() =>
            {   
                return Write(source.Token);                
            }, source.Token);


            Thread.Sleep(3000);
            source.Cancel();//source tiene un metodo cancel que pone en true la propiedad iscancellationRequested a travez de su propiedad token.
            
            task.Wait();
            Console.WriteLine();
        }
        public void LongRunningTask() //un parametro que se puede pasar para indicarle que la tarea se va a tardar y que considere si se crea un thread o se saca del pool, lo que se evalue pertinente.
        {
            Task hilo = new Task(() =>//using es para hacerle dispouse y de esta forma ya no es necesario el wait, limpia la memoria. es mejor fire and forget, iniciar el hilo y continuar, pero hay que asegurarse que se cierre y no este gastando memoria.
                Thread.Sleep(100000),
                TaskCreationOptions.LongRunning);
            
                hilo.Start();
            
            //hilo.Wait();//hace que el hilo principal espere a l hilo creado con Task, es mejor poner wait, porque limpia la memoria, implementa IDisposable para cuando pase el garbage collector.
        }
        public void ParallelIterationsExceptions() 
        {//como cachar una exepcion en parallel.
            string abecedario = "aeiou";
            int[] enteros = new int[5];
            try
            {
                Parallel.For(0, 5, i =>
                    {
                        enteros[i] = int.Parse(abecedario[i].ToString());
                    });
            }
            catch(AggregateException e)//es como una variable que recibe los exeption de try. 
            {
                foreach(Exception item in e.InnerExceptions)
                {
                    Debug.Print("Error de cada uno de los threads en try {0}", item.Message);
                }

            }
        }
        public void ParallelIterations() 
        {
            string[] arregloStrings=new string[100];
            int x = 0;
           
            Parallel.For(0, 100, i =>//toda la funcion, se va a ejecutar en paralelo.//este equivale a un for, solo con varios threads.
            {
                x++;//varios threads tienen acceso a esta variable en este loop, es un race condition. y se imprimen en el orden segun vaya terminando cada thread, aleatoreo.
                arregloStrings[i] = x.ToString();               
            });

            //string valores = string.Join(",", arregloStrings);
            //Debug.Print(valores);

            List<int> enteros = new List<int>();
            Parallel.ForEach(arregloStrings, cadaElementoDelArreglo =>
                {
                    enteros.Add(int.Parse(cadaElementoDelArreglo));
                });

            string valores = string.Join(",", enteros.ToArray());
            Debug.Print(valores);
            
        }

        public void ParallelLoopCancelation()
        {
            string[] arregloStrings = new string[100];
            int x = 0;

            CancellationTokenSource cts = new CancellationTokenSource();//mecanismo para pasar el mensaje de cancelacion al parallel for.
            ParallelOptions options = new ParallelOptions();//sirve para pasar el token al parallel for.
            options.CancellationToken = cts.Token;//propiedad que guarda el cancelation token del cts.
            cts.Token.Register(()=>Debug.Print("cancelando...."));//metodo que se inicia cuando se cancela.

            Task otroHilo = new Task(() =>
            {
                Parallel.For(0, 100, options, i =>//toda la funcion, se va a ejecutar en paralelo.//este equivale a un for, solo con varios threads.
                {
                    Thread.Sleep(1000);
                    x++;//varios threads tienen acceso a esta variable en este loop, es un race condition. y se imprimen en el orden segun vaya terminando cada thread, aleatoreo.
                    arregloStrings[i] = x.ToString();
                });
            });
            otroHilo.Start();

            bool salir = false;
            while (!salir)
            {                
                if (x > 50)
                {
                    cts.Cancel();//cancela los threads que estan en el foreach., aunque estemos en otro hilo y fuera del foreach.
                    salir = true;
                }
            }

            //otroHilo.Wait();
            string valores = string.Join(",", arregloStrings);
            Debug.Print(valores);
        }
        public void ParallelOptions()
        {
            string[] arregloStrings = new string[100];


            ParallelOptions options = new ParallelOptions();//sirve para pasar el token al parallel for.
            options.MaxDegreeOfParallelism = 2;//el numero de threads que va usar en el parallelfor.

            ParallelLoopState state = new ParallelLoopState();//se crea afuera para ejecutarse el break dentro del parallel for.
            Parallel.For(0, 100, options, i =>//toda la funcion, se va a ejecutar en paralelo.//este equivale a un for, solo con varios threads.
            {
                arregloStrings[i] = Task.CurrentId.ToString();
                
                //state.Break();//break;
            });

            string valores = string.Join(",", arregloStrings);
            Debug.Print(valores);
        }

    }
}
