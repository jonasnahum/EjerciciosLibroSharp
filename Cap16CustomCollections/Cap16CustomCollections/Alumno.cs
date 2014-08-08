using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap16CustomCollections
{
    public class Alumno:IComparable<Alumno>
    {
        public string Nombre { get; set; }
        public int CompareTo(Alumno other)//un alumno lo va a comparar con otro alumno. regresa un int, Value Meaning Less than zero
        {                                            //     This object is less than the other parameter.Zero This object is equal to
                                                     //     other. Greater than zero This object is greater than other.        
            int LongOther = other.Nombre.Length;
            int LongActual = this.Nombre.Length;
            if (LongActual < LongOther)
            {
                return 0 - 1;
            }
            if (LongActual == LongOther)
            {
                return 0;
            }
            if (LongActual > LongOther)
            {
                return 1;
            }
            return 0;//valor que se regresa si no entra ninguna de las condiciones. nunca se va llamar.
        }
        public Alumno(string nombre) 
        {
            Nombre = nombre;
        }
    }

    public class ListaAlumnos
    {//esta clase tiene dos IndexOperators,
        private Alumno Alumno1 { get; set; }//por aqui no se puede acceder a esta propiedad, solo con el IndexOperator.
        private Alumno Alumno2 { get; set; }
        private Alumno Alumno3 { get; set; }
        private Alumno Alumno4 { get; set; }
        private Alumno Alumno5 { get; set; }

        private List<Alumno> alumnos = new List<Alumno>();
        public void Load()
        {
            alumnos.Clear();
            alumnos.Add(Alumno1);
            alumnos.Add(Alumno2);
            alumnos.Add(Alumno3);
            alumnos.Add(Alumno4);
            alumnos.Add(Alumno5);
        }        

        public Alumno this[params string[] nombres]//params es para recibir un string y que no se tenga que poner new a la hora de enviar parametros en el index.
        {
            get
            {
                Load();
                return alumnos.FirstOrDefault(a => a!=null&&nombres.Contains(a.Nombre));//checa si nombres que se pasaron como parametro, si ese array contiene algun nombre los que tiene los Alumnos en ListaAlumnos, y lo regresa.
            }
            set
            {
                Load();   
                Alumno alumno = alumnos.FirstOrDefault(a => a != null && nombres.Contains(a.Nombre));//a es cada alumno de la coleccion llamada nombres que se recibe como parametro, checar si contiene algun nombre de los que este en ListaAlumnos, regresar el primero o el default y gruardarlo en la variable alumno de tipo Alumno.
                if (alumno != null)
                {
                    alumno.Nombre = value.Nombre;
                }
            }
        }

        public Alumno this[int index]//Index Operator.Propiedad especial que regresa un Alumno, el cual se ubicara por su indice de tipo int.
        {
            get
            {
                switch (index)
                {
                    case 0: return Alumno1;//si llaman el casillero 0, regresa lo que hay en la propiedad Alumno1 de tipo Alumno.
                    case 1: return Alumno2;
                    case 2: return Alumno3;
                    case 3: return Alumno4;
                    case 4: return Alumno5;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set 
            {
                switch (index)
                {
                    case 0: Alumno1 = value; break;//si agregan en casillero[0], lo asigna a propiedad Alumno1.
                    case 1: Alumno2 = value; break;
                    case 2: Alumno3 = value; break;
                    case 3: Alumno4 = value; break;
                    case 4: Alumno5 = value; break; 
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
