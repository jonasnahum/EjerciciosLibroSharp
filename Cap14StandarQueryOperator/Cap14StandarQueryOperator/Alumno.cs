using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap14StandarQueryOperator
{
    public class PadreDeFamilia
    {
        public override string ToString()
        {
            return Nombre;
        }
        public string Nombre { get; set; }
        public PadreDeFamilia(string nombre)
        {
            Nombre=nombre;
        }
    }

    public class Alumno
    {
        public PadreDeFamilia Padre { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public decimal Promedio { get; set; }
        public Alumno(string nombre, int edad,decimal promedio, string nombrePadre)
        {
            Nombre = nombre;
            Edad = edad;
            Promedio = promedio;
            Padre = new PadreDeFamilia(nombrePadre);
        }
        public static IEnumerable<Alumno> ObtenerAlumnos()//de typo IEnumerable generico o de <T>
        {
            IList<Alumno> alumnos = new List<Alumno>();//,,IEnumerable, List, e IList son compatibles.List<T> implementa IEnumerable<T>
            alumnos.Add(new Alumno("jonas", 28, 8.4M,"jose"));
            alumnos.Add(new Alumno("pedro", 20, 9M,"maria"));
            alumnos.Add(new Alumno("juan", 26, 7.4M,"juana"));
            alumnos.Add(new Alumno("mary", 18, 6.4M,"Marycruz"));
            alumnos.Add(new Alumno("lupe", 16, 8M,"guadalupe"));
            alumnos.Add(new Alumno("chole", 30, 5M,"soledad"));
            alumnos.Add(new Alumno("amon", 22, 7M,"nenita"));
            alumnos.Add(new Alumno("edwin", 15, 7.64M,"ceci"));
            alumnos.Add(new Alumno("sandra", 16, 8.6M,"sandra"));
            alumnos.Add(new Alumno("vero", 32, 6.5M,"maria"));
            alumnos.Add(new Alumno("julieta", 28, 8.4M,"vero"));
            alumnos.Add(new Alumno("diego", 14, 8.4M,"Nury"));
            alumnos.Add(new Alumno("paul", 15, 6.5M,"estela"));
            alumnos.Add(new Alumno("paty", 8, 7.8M,"yolanda"));
            alumnos.Add(new Alumno("hugo", 23, 3.0M,"lucrecia"));

            return alumnos;
        }
        public override string ToString()//el base por default es el fully qualifyed name, este override regresa los datos del alumno.
        {
            return string.Format("{0} {1} {2}", Nombre, Edad, Promedio);
        }
    }
}
