using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap12Delegados
{
    public class Sumadora
    {
        public delegate int SumarDosEnteros(int a, int b);
        public delegate string SumarStrings(string a, string b);
        public delegate void ImprimirConsola(string s);
    }
    public interface ISumadora
    {
        Cap12Delegados.Sumadora.SumarDosEnteros M1 { get; set; }//en este caso, las propiedades son de tipo delegado.
        Cap12Delegados.Sumadora.SumarStrings M2 { get; set; }//SumarStrings es un tipo de dato, que guarda metodo.
        Cap12Delegados.Sumadora.ImprimirConsola M3 { get; set; }
    }
    public class DelegadosEjemplo : ISumadora
    {
        public Cap12Delegados.Sumadora.SumarDosEnteros M1 { get; set; }
        public Cap12Delegados.Sumadora.SumarStrings M2 { get; set; }
        public Cap12Delegados.Sumadora.ImprimirConsola M3 { get; set; }
    }

}
