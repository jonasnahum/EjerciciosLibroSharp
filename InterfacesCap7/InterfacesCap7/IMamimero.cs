using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesCap7
{
    public interface IMamifero
    {
        void TomarLeche();

    }
    public interface INombre
    {
        string Nombre {get; set; }

    }
    public interface IHumano:IMamifero
    {
        void Dormir();

    }
    public interface IPerro:IMamifero//interface que hereda de otra interface.
    {
        void Ladrar();
    }
    public class Persona : IHumano, INombre //implementando multiples interfaces.
    {
        public bool yaDesayuno { get; set; }
        public void TomarLeche()
        {
            yaDesayuno = true;
        }
        public bool yaDurmio { get; set; }
        public void Dormir()
        {
            yaDurmio = true;
        }

        public string Nombre
        {
            get;
            set;
        }
    }
    public class PastorAleman : IPerro, IMamifero, INombre //herencia de multiples interfaces.
    {
        public bool yaLadroHoy { get; set; }
        public void Ladrar()
        {
            yaLadroHoy = true;
        }
        public bool yaTomoLeche { get; set; }
        public void TomarLeche()
        {
            yaTomoLeche = true;
        }

        public string Nombre
        {
            get;
            set;
        }
    }
}
