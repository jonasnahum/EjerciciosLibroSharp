using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesCap7
{
    public class Camioneta: IVehiculo
    {
        public Camioneta(string color, string avanzar)
        {
            this.Color = color;
            this.mAvanzar = avanzar;

        }
        private string mColor;
        private string mAvanzar;
        public string Color    
        {
            get
            {
                return mColor;
            }
            set
            {
                mColor = value;
            }
        }

        public string Avanzar()
        {
            return mAvanzar;
        }
    }
}
