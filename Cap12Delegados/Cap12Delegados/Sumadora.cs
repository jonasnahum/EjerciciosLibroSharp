using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap12Delegados
{
    public class Sumadora
    {
        public delegate int SumarDosEnteros(int a, int b);//se declaran los delegados, su tipo de dato de retorno y parametros.
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
        public Cap12Delegados.Sumadora.SumarDosEnteros M1 { get; set; }//tipo de dato que guarda un metodo.
        public Cap12Delegados.Sumadora.SumarStrings M2 { get; set; }//variable de tipo delegado que guarda un metodo.
        public Cap12Delegados.Sumadora.ImprimirConsola M3 { get; set; }
    }

    public class DelegadosEjemploSystemDefined
    {
        public Func<int, int, int> M1 { get; set; }// los dos primeros son los parametros en el orden en el que aparecen. el ultimo int es el valor de retorno// esta linea es igual apublic Cap12Delegados.Sumadora.SumarDosEnteros M1 { get; set; }.
        public Func<string, string, string> M2 { get; set; }//cumple con contrato de ISumadora.
        public Action<string> M3 { get; set; }//el primero es el tipo que recibe.

    }

    public class DelegadosEjemploLambda
    {//firmas, tipos de dato que guardan un metodo:
        public Func<int, int, int> M1 { get; set; }//el primer int es el valor de retorno, los otros son los parametros en el orden en el que aparecen.// esta linea es igual apublic Cap12Delegados.Sumadora.SumarDosEnteros M1 { get; set; }.
        public Func<string, string, string> M2 { get; set; }//cumple con contrato de ISumadora.
        public Action<string> M3 { get; set; }//el primero es el tipo que recibe.

    }
}
