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
        public Escuela() 
        {
            Grupos = new Grupo[3];//se instancia el array.
            Grupos[0]= new Grupo(1);
            Grupos[1] = new Grupo(2);
            Grupos[2] = new Grupo(3);
        }
        public Grupo[] Grupos { get; set; }//reservar el espacio para un array tipo Grupo, llamado Grupos.
        public Alumno[] AvanzarGrado(Alumno[] nuevoIngreso)
        {
            Alumno[] egresadosPrimero = Grupos[0].Egresar();
            Alumno[] egresadosSegundo = Grupos[1].Egresar();
            Alumno[] egresadosTercero = Grupos[2].Egresar();

            Grupos[0].InscribirAlunos(nuevoIngreso);
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
            Alumnos = new Alumno[5];
            
        }
        private int Grado { get; set; }
        public Alumno[] Alumnos { get; set; }//coleccion de alumno, de clase alumno, llamada Alumnos.
       
        public void InscribirAlunos(Alumno[] alumnos)
        {
            foreach (Alumno alumno in alumnos)
            {
                if (alumno==null)
                {
                    continue;//pasar al sig elemento, o sig iteracion del for.
                }
                for (int i = 0; i < Alumnos.Length; i++)
                {
                    if (alumno.Promedio < 6.0 && !alumno.EstaRepitiendo)
                    {
                        throw new InscribirAlumnoReprobadoException();
                    }
                    if (Alumnos[i] == null)
                    {
                        Alumnos[i] = alumno;
                    }
                }
                throw new CupoLlenoException();
            }
        }

        public Alumno[] Egresar() 
        {
            Alumno[] egresados = new Alumno[Alumnos.Length];
            int egresadosContador=0;
            for (int i = 0; i < Alumnos.Length;i++ )
            {
                if (Alumnos[i]==null)
                {
                    continue;                   
                }
                if (Alumnos[i].Promedio < 6)
                {
                    // se queda en el grupo;
                }
                else
                {
                    egresados[egresadosContador] = Alumnos[i];
                    Alumnos[i] = null;
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
    public class InscribirAlumnoReprobadoException : Exception
    {
        public InscribirAlumnoReprobadoException(string mensaje):base(mensaje)
        {
            
        }
        public InscribirAlumnoReprobadoException() : this("se intent[o inscribir a un alumno reprobado en un grupo") { }
    }

    public class CupoLlenoException : Exception 
    {
        public CupoLlenoException():base("el grupo esta lleno") { }
    }


}
