using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap13Events
{
    public class PropertyArgs : System.EventArgs
    {
        public int Value { get; set; }
        public PropertyArgs(int value)
        {
            Value = value;
        }
    }
    public class EmisorDeEventos
    {
        private int property;
        public int Property
        {
            get
            {
                if (OnPropertyGet!=null)
                {
                    OnPropertyGet(this, new PropertyArgs(property));
                }
                return property;
            }
            set
            {
                property = value;
                if (OnPropertySet!=null)
                {
                    OnPropertySet(this, new PropertyArgs(property));    
                }               
            }
        }
        public EmisorDeEventos() 
        {
            if (OnCreatedClass!=null)
            {
                OnCreatedClass(this, new System.EventArgs());
            }
        }
        public void Metodo()
        {
            if (OnMethodCalled!=null)
            {
                OnMethodCalled(this, new System.EventArgs());   
            }
 
        }

        //diferentes formas de declarar un evento.
        public event EventHandler OnCreatedClass;// primero nombre del delegado y luego, nombre del evento.

        public event MethodHandler OnMethodCalled;//nombre del evento
        public delegate void MethodHandler(object setnder, System.EventArgs args);//nombre del delegado.

        public event EventHandler<PropertyArgs> OnPropertyGet;

        public event PropertyHandler OnPropertySet;
        public delegate void PropertyHandler(object sender, PropertyArgs args);
 



    }
}
