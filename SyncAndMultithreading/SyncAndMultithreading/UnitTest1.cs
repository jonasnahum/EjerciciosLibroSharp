using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap19;
using ResetEvents;  

namespace SyncAndMultithreading
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {//mutex, volatile.
            LockWithMonitor examples = new LockWithMonitor();
            //examples.RaceCondition();
            LockWithLock lwl = new LockWithLock();
            //lwl.RaceCondition();
            LockWithInterlocked masEficiente= new LockWithInterlocked();
            //masEficiente.RaceCondition();//mas eficiente que el lock, segun.
            ResetEvents.ResetEvents.Main();//puede controlar los hilos a gusto, que se detengan y esperen al principal o vice versa.

        }
    }
}
