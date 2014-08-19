using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeMultithreadingEvent
{
    public class Timer
    {
        public event TickHandler Tick;
        public EventArgs e = null;
        public delegate void TickHandler(Timer m, EventArgs e);
        public void Start()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                TickHandler copiaLocal = Tick;//se hace una copia local del evento.
                
                if (copiaLocal != null)//se pone la copia local en vez del evento Tick,para hacerlo thread safe, Tick puede ser nulo si le agregan otro handler on otro thread, borra la lista actual y crea una nueva y entre el nullo y fire puede ocurrir un race condition.
                {
                    copiaLocal(this, e);
                }
            }
        }
    }
}
