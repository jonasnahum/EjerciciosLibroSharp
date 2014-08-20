using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cap19
{
    public class TimerExample
    {
        private static int _Count = 0;
        private static readonly AutoResetEvent _ResetEvent =//para hacer wait y set.
            new AutoResetEvent(false);
        private static int _AlarmThreadId;
        public static void Main()
        {
            using (Timer timer =  new Timer(Alarm, null, 0, 1000))//manda un thread al metodo Alarm cada segundo.
            {
                _ResetEvent.WaitOne();//esperar hasta que Alarm mande un set. este es el thread principal
            }
            if (_AlarmThreadId==Thread.CurrentThread.ManagedThreadId)//lo que no va a ocurrir porque son diferentes thread este el principal y el que esta en le metodo Alarm.
            {
                throw new ApplicationException("Thread Ids are the same");
            }
            if (_Count<9)
            {
                throw new ApplicationException("_Cound <9");
            }
            Debug.Print("Alarm Thread Id) {0} !={1} ( Main Thread Id)", _AlarmThreadId, Thread.CurrentThread.ManagedThreadId);
            Debug.Print("Final Count={0}", _Count);
        }
        static void Alarm(object state)
        {
            _Count++;//se va incrementando la variable cada segundo.
            Debug.Print("{0}:- {1}", DateTime.Now.ToString("T"), _Count);
            if (_Count>=9)
            {
                _AlarmThreadId = Thread.CurrentThread.ManagedThreadId;
                _ResetEvent.Set();//envia el set al thread principal, y termina el aquel wait.
            }
        }
    }
}
