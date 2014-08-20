using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cap19
{
    public class LockWithMonitor
    {
        int _Total = int.MaxValue;
        long _Count = 0;//esta variable se accesa al mismo tiempo por dos threads.
        readonly static object _Sync = new object();//objeto de tipo referencia estatico, no guarda ningun valor es un ancla.

        public void RaceCondition() 
        {
           
            Task task = Task.Factory.StartNew(() =>//este nuevo hilo hace un decremento, mientras el hilo main hace un aumento.
                {
                    //decrement.
                    for (int i = 0; i < _Total; i++)
                    {
                        bool lockTaken = false;//aqui es falso, en la siguente linea, es verdadero, porque entra el bloqueo y se vuelve true.
                        Monitor.Enter(_Sync, ref lockTaken);//recibe el objeto ancla. y bloquea esta parte para que solo un thead accese a estas variables, hasta el exit.
                        try
                        {
                            _Count--;
                        }
                        finally//si ocurre una exepcion aseguara que se llame el exit.
                        {
                            if (lockTaken)
                            {
                                Monitor.Exit(_Sync);//sale del bloqueo, y comienza otra vez el loop.
                            }
                        }
                    }
                });

            for (int i = 0; i < _Total; i++)
            {
                bool lockTaken = false;
                Monitor.Enter(_Sync, ref lockTaken);//recibe el objeto ancla. y bloquea esta parte para que solo un thead accese a estas variables, hasta el exit.
                try//este try solo es para poder poner el finally.
                {
                    _Count++;
                }
                        finally//si ocurre una exepcion aseguara que se llame el exit.
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(_Sync);
                    }
                }
                
            }

            task.Wait();
            Debug.Print("{0}, count", _Count);

        }
    }
}
