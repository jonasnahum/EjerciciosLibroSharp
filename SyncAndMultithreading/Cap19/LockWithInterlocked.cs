using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cap19
{
    public class LockWithInterlocked
    {
        int _Total = int.MaxValue;
        long _Count = 0;//esta variable se accesa al mismo tiempo por dos threads.//volatile, dice al cpu compilador que lo lea tal cual el codigo sin optimizacion interna cuando lea esta vaariable.
        
        public void RaceCondition()
        {

            Task task = Task.Factory.StartNew(() =>
            {
                //decrement.
                for (int i = 0; i < _Total; i++)
                {
                    Interlocked.Decrement(ref _Count);//hace lo mismo que lock. mas eficiente.                                        
                }
            });

            for (int i = 0; i < _Total; i++)
            {

                Interlocked.Add(ref _Count, 1);    
            }

            task.Wait();
            Debug.Print("{0}, count", _Count);

        }
    }
}
