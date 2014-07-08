using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesCap7
{
    public class Tsuru: IVehiculo
    {
        public Tsuru(string color, string avanzar)
        {
            this.mColor = color;
            this.mAvanzar = avanzar;
        }
        string mColor;
        string IVehiculo.Color
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
        string mAvanzar;
        string IVehiculo.Avanzar()
        {
            return mAvanzar;
        }
        // estos metodos  y e prpropiedad de la interfase, son explicitos, porque te indican de que interface provienen
        
    }
}
