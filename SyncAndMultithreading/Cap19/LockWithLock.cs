using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cap19
{
    public class LockWithLock
    {
        int _Total = int.MaxValue;
        long _Count = 0;//esta variable se accesa al mismo tiempo por dos threads.//volatile, dice al cpu compilador que lo lea tal cual el codigo sin optimizacion interna cuando lea esta vaariable.
        readonly static object _Sync = new object();//objeto de tipo referencia estatico, no guarda ningun valor es un ancla.

        public void RaceCondition()
        {//usa lock, que hace lo mismo que el monitor.

            Task task = Task.Factory.StartNew(() =>
            {
                //decrement.
                for (int i = 0; i < _Total; i++)
                {
                    lock (_Sync)
                    {
                        _Count--;
                    }
                }
            });

            for (int i = 0; i < _Total; i++)
            {
                lock (_Sync)
                {
                    _Count++;
                }
            }

            task.Wait();
            Debug.Print("{0}, count", _Count);

        }
    }
}
