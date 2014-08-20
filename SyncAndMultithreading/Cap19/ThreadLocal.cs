using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cap19
{
    public class ThreadLocal
    {
        int _Total = 100;
        static ThreadLocal<long> _Count = new ThreadLocal<long>(()=>0, true);

        public void RaceCondition()
        {

            Task task = Task.Factory.StartNew(() =>
            {
                //decrement.
                for (int i = 0; i < _Total; i++)
                {
                    _Count.Value--;
                }
                
                Debug.Print("{0}, Task count", _Count.Value);

            });

            for (int i = 0; i < _Total; i++)
            {
                    _Count.Value++;
            }
            
            task.Wait();
            Debug.Print("{0}, Main thread count", _Count.Value);

            Debug.Print("Counters: {0}", _Count.Values.Count());
        }
    }
}
