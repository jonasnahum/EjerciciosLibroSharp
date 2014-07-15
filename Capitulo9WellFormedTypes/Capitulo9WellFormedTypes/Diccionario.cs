using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitulo9WellFormedTypes
{
    public class Diccionario:IDisposable
    {
        public Palabra[] Palabras =new Palabra[5000000];//un array de tipo Palabra aqui en Class Diccionario.
        public Diccionario()//un metodo
        {
            for (int i = 0; i < Palabras.Length; i++)//iterar por las 5000000.
            {
                Palabras[i] = new Palabra() //hacer un objeto por cada dato en el array.
                {
                    Valor=i.ToString()//cada dato en el array se convierte a string y se guarda en variable Valor que esta declarada en Clase Palabra.
                };
            }
        }
        public void Close() 
        {
            Palabras = null;

        }

       ~Diccionario()//undeterministic.cuando pase el garbage collelctor
        {
            Dispose();
        }

       public void Dispose()//deterministic
       {
           Close();
           System.GC.SuppressFinalize(this);// esto quita el finalizer, para que ya no se vuelva a ejecutar, puesto que aqu[i ya se ejecuto.
       }
    }
    public class Estudiante : Persona
    {//lazy loading.
        private WeakReference Data;//se declara Data como WeakReference.no se le asaigna valor. se le indica que va a ocupar mucho espacio, para que si el garbage collector, no la ha borrado y se quiere recuperar en el futuro, se marca y si se puede recuperar
        Palabra Palabra { get; set; }// stronge reference, se marca por el garbage colector y ya no se puede recuperar.
        public Diccionario GetData()//metodo que regresa un dato de tipo Diccionario.lAZY LOADING.
        {
            Diccionario diccionario = (Diccionario)Data.Target;//la segunda vez si tiene algo, lo convierte a tipo Diccionario y lo guarda en variable de tipo Diccionario.
            if (diccionario != null)
            {
                return diccionario;//a partir de la segunda vez, no es nulo, solo regresa el que se habia guardado en Data.Target.
            }
            else 
            {
                diccionario = new Diccionario();//esta es la primera vez, se crea un Diccionario y se guarda en Data.Targer, para posteriores llamadas a este metodo.
                Data.Target = diccionario;
            }
            return diccionario;// en la primera vez, guardar Data.Target en variable diccionario y regresarla.
        }
        public Estudiante()
        {
            Palabra = GetData().Palabras[0];
        }
        public void Leer()
        {
            string palabraleida = Palabra.Valor;
        }
    }

    public class Palabra
    {
        /// <summary>
        /// 
        /// </summary>
        public string Valor { get; set; }
        public override int GetHashCode()//entero que identifica a la palabra, para id mas rapido.
        {
            int hashCode = Valor.GetHashCode();
            hashCode ^= base.GetHashCode();
            return hashCode;
        }
        public override bool Equals(object obj)
        {
            string valor= ((Palabra)obj).Valor;
            if (valor == Valor + ".")
                return true;
            else 
                return false;

        }        public static bool operator == (Palabra p1,Palabra p2)        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Palabra p1, Palabra p2)
        {
            return !p1.Equals(p2);
        }
    }
}
