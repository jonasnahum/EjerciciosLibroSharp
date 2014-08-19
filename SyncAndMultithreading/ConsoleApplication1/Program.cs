using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Program
    {//Mutex permite que solo un programa corra al mismo tiempo, no te deja abrir otro.
        static void Main(string[] args)
        {
            bool firstApplicationInstance;
            string mutexName = Assembly.GetEntryAssembly().FullName;
            using (Mutex mutex = new Mutex(false, mutexName, out firstApplicationInstance))
            {
                if (!firstApplicationInstance)
                {
                    Console.WriteLine("this application is already running.");
                }
                else
                {
                    Console.WriteLine(" ENTER TO SHUT DOWN");
                    Console.ReadLine();
                }
            }
        }
    }
}
