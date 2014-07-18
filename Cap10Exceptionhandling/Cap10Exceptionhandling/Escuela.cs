using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cap10Exceptionhandling
{
    public class Escuela
    {
        public Escuela() //en el objeto escuela, inicializa grupos en el array.
        {
            Grupos = new Grupo[3];//se inicializa el array.//grupo es un tipo de dato, una clase, que recibe el grado como parametro.
            Grupos[0]= new Grupo(1);//objetos Grupo y se asignan a cada casillero.
            Grupos[1] = new Grupo(2);
            Grupos[2] = new Grupo(3);
        }
        public Grupo[] Grupos { get; set; }//reservar el espacio para un array tipo Grupo, llamado Grupos.
        public Alumno[] AvanzarGrado(Alumno[] nuevoIngreso)//metodo que regresa un array Alumno 'egresados tercero' y que recibe un array Alumno,'generacion', del test.
        {
            Grupo grupo = Grupos[0];
            Alumno[] egresados = grupo.Egresar();

            Alumno[] egresadosPrimero = Grupos[0].Egresar();//array de tipo Alumno, llamado egresadosPrimero, que es igual al array de tipo Grupo, llamado grupos
            Alumno[] egresadosSegundo = Grupos[1].Egresar();//el array de Alumnos[] de tipo Alumno que esta en .Egresar, se guarda en otro array Alumno[] que se llama egresados segundo.
            Alumno[] egresadosTercero = Grupos[2].Egresar();

            Grupos[0].InscribirAlunos(nuevoIngreso);//Grupos es el nombre del array , Grupo es la clase, en la casilla 0 es para primer ano, InscribirAlunos es un metodo de esa clase y esta linea de codigo envia como parametro un array de alumnos llamado nuevo ingreso. que a su vez, viene de Test.
            Grupos[1].InscribirAlunos(egresadosPrimero);
            Grupos[2].InscribirAlunos(egresadosSegundo);

            return egresadosTercero;
        }


    }
    public class Grupo
    {
        public Grupo(int grado) 
        {
            Grado = grado;
            Alumnos = new Alumno[5];// que el array alumnos, haga 5 espacios.
            
        }
        private int Grado { get; set; }
        public Alumno[] Alumnos { get; set; }//property coleccion de Alumno, de clase Alumno, llamada Alumnos.
       
        public void InscribirAlunos(Alumno[] alumnos)//metodo que recibe un array de Alumno[] AvamzarGrado() que a su vez viene este array de Test..
        {
            foreach (Alumno alumno in alumnos)//FOREACH, TYPO, VARIABLE EN SINGULAR IN VARIABLE EN PLURAL.
            {
                if (alumno==null)
                {
                    continue;//pasar al sig elemento, o sig iteracion del for.
                }
                for (int i = 0; i < Alumnos.Length; i++)//utiliza el array Alumnos de tipo Alumno [5].
                {
                    if (alumno.Promedio < 6.0 && !alumno.EstaRepitiendo)//Promedio es una property y EstaRepitiendo es un bool.
                    {
                        throw new InscribirAlumnoReprobadoException();//manda mensaje de que se intento inscribir alumno reprobado.
                    }
                    if (Alumnos[i] == null)//se el array llamado Alumnos esta vacio,
                    {
                        Alumnos[i] = alumno;//ponerle un alumno a cada espacio vacio.
                    }
                }
                throw new CupoLlenoException();//mensaje: El grupo esta lleno.
            }
        }

        public Alumno[] Egresar() // un metodo que regresa un array de tipo Alumno.
        {
            Alumno[] egresados = new Alumno[Alumnos.Length];// un array tipo Alumno[] , se crea una nueva instancia llamada egresados de tipo Array Alumno, y su Lenght se iguala con la propiedad de Alumnos //un array de tipo Alumno que se llama egresados y que es del tamano de Alumnos.Length.
            int egresadosContador=0;
            for (int i = 0; i < Alumnos.Length;i++ )
            {
                if (Alumnos[i]==null)
                {
                    continue;//si es nullo que continue.                   
                }
                if (Alumnos[i].Promedio < 6)//si algun alumno tiene un promedio menor a 6.
                {
                    // se queda en el grupo;
                }
                else//si es mayor o igual a 6, o si Alumnos[i] tiene algo.
                {
                    egresados[egresadosContador] = Alumnos[i];//el metodo []egresados, en su variable local egresadosContador, es igual al array de Alumnos.
                    Alumnos[i] = null;//borrar Alumnos__
                    egresadosContador++;
                }
            }
            return egresados;
        }

    }
    public class Alumno
    {
        public Alumno(string nombre, double promedio)
        {
            Nombre = nombre;
            Promedio = promedio;
        }
        public bool EstaRepitiendo { get; set; }
        public string Nombre { get; set; }
        public double Promedio { get; set; }
    }
    public class InscribirAlumnoReprobadoException : Exception//todas deben heredar de Exception y tener la palabra Exception al final de nombre.
    {
        public InscribirAlumnoReprobadoException(string mensaje):base(mensaje)//se le manda el mensaje a la base.
        {
            
        }
        public InscribirAlumnoReprobadoException() : this("se intent[o inscribir a un alumno reprobado en un grupo") { }
    }

    public class CupoLlenoException : Exception 
    {
        public CupoLlenoException():base("el grupo esta lleno") { }//se le manda el mensaje a base Exception.
    }


}
