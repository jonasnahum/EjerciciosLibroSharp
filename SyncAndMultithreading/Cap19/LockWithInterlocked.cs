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
        long _Count = 0;//esta variable se accesa al mismo tiempo por dos threads. uno que es creado y otro es el main, en un for, solicitan varias veces esta variable, lo cual se evita con interlocked.
        
        public void RaceCondition()
        {

            Task task = Task.Factory.StartNew(() =>
            {
               
                for (int i = 0; i < _Total; i++)
                {
                    Interlocked.Decrement(ref _Count);//hace lo mismo que lock. mas eficiente.el .Decrement es u metodo de la clase.                                        
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
