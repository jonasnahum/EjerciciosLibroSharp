using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cap19
{
//    One alternative solution to synchronization is isolation and one method
//for implementing isolation is thread local storage. With thread local storage,
//each thread has its own dedicated instance of a variable. As a result, there is
//no need for synchronization, as there is no point in synchronizing data that
//occurs within only a single thread’s context. Two examples of thread local
//storage implementations are ThreadLocal<T> and ThreadStaticAttribute.
    public class AislamientoThreadStatic
    {
        int _Total = 100;
        [ThreadStatic]
        static long _Count = 0;

        public void RaceCondition()
        {

            Task task = Task.Factory.StartNew(() =>
            {
               
                for (int i = 0; i < _Total; i++)
                {
                    _Count--;
                }

                Debug.Print("{0}, Task count", _Count);

            });

            for (int i = 0; i < _Total; i++)
            {
                _Count++;
            }

            task.Wait();
            Debug.Print("{0}, Main thread count", _Count);


        }
    }
}