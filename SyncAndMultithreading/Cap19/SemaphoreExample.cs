using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cap19
{
    public class SemaphoreExample
    {
        public void Test() 
        {
            SemaphoreSlim ss = new SemaphoreSlim(2); // se le indica que el thread se detendra 2 veces.
            Debug.Print("Constructed a SemaphoreSlim with an initial count of 2");

            Debug.Print("First non-blocking Wait: {0} (should be true)", ss.Wait(0));//la primera vez que espera por 0 milisegundos.
            Debug.Print("Second non-blocking Wait: {0} (should be true)", ss.Wait(0));//la segunda vez.
            Debug.Print("Third non-blocking Wait: {0} (should be false)", ss.Wait(0));//aqui ya no se espera, aunque se haga wait, porque solo son dos veces.

            // Do a Release to free up a spot
            ss.Release();//con Release se puede esperar una vez mas a parte de las que ya tenia programadas.regresa un thread otra vez al pool., tenia 2 segun el constructor.

            Debug.Print("Non-blocking Wait after Release: {0} (should be true)", ss.Wait(0));

            // Launch an asynchronous Task that releases the semaphore after 100 ms..asynchronous significa en otro hilo.
            Task t1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                Debug.Print("Task about to release SemaphoreSlim");
                ss.Release();
            });

            // You can also wait on the SemaphoreSlim via the underlying Semaphore WaitHandle. 
            // HOWEVER, unlike SemaphoreSlim.Wait(), it WILL NOT decrement the count. 
            // In the printout below, you will see that CurrentCount is still 1
            ss.AvailableWaitHandle.WaitOne();
            Debug.Print("ss.AvailableWaitHandle.WaitOne() returned, ss.CurrentCount = {0}", ss.CurrentCount);

            // Now a real Wait(), which should return immediately and decrement the count.
            ss.Wait();
            Debug.Print("ss.CurrentCount after ss.Wait() = {0}", ss.CurrentCount);

            // Clean up
            t1.Wait();
            ss.Dispose();
        }
    }
}
