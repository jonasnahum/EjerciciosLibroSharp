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
        public int IdSalon { get; set; }
        public Alumno(string nombre, int edad,decimal promedio, string nombrePadre, int idSalon)
        {
            Nombre = nombre;
            Edad = edad;
            Promedio = promedio;
            Padre = new PadreDeFamilia(nombrePadre);
            IdSalon = idSalon;
        }
        public static IEnumerable<Alumno> ObtenerAlumnos()//de typo IEnumerable generico o de <T>
        {
            IList<Alumno> alumnos = new List<Alumno>();//,,IEnumerable, List, e IList son compatibles.List<T> implementa IEnumerable<T>
            alumnos.Add(new Alumno("jonas", 28, 8.4M,"jose",1));
            alumnos.Add(new Alumno("jonas", 20, 9M,"maria",1));
            alumnos.Add(new Alumno("juan", 26, 7.4M,"juana",1));
            alumnos.Add(new Alumno("mary", 18, 6.4M,"Marycruz",1));
            alumnos.Add(new Alumno("lupe", 16, 8M,"guadalupe",1));
            alumnos.Add(new Alumno("chole", 30, 5M,"soledad",2));
            alumnos.Add(new Alumno("amon", 22, 7M,"nenita",2));
            alumnos.Add(new Alumno("edwin", 15, 7.64M,"ceci",2));
            alumnos.Add(new Alumno("sandra", 16, 8.6M,"sandra",2));
            alumnos.Add(new Alumno("vero", 32, 6.5M,"maria",2));
            alumnos.Add(new Alumno("julieta", 28, 8.4M,"vero",3));
            alumnos.Add(new Alumno("diego", 14, 8.4M,"Nury",3));
            alumnos.Add(new Alumno("paul", 15, 6.5M,"estela",3));
            alumnos.Add(new Alumno("paty", 8, 7.8M,"yolanda",3));
            alumnos.Add(new Alumno("hugo", 23, 3.0M,"lucrecia",3));

            alumnos.Add(new Alumno("solovino", 5, 7.8M, "Toby", 13));
            alumnos.Add(new Alumno("pantera", 23, 3.0M, "wera", 13));
            return alumnos;//regresa una lista de objetos de tipo IList<Alumno>
        }
        public override string ToString()//el base por default es el fully qualifyed name, este override regresa los datos del alumno.
        {
            return string.Format("{0} {1} {2}", Nombre, Edad, Promedio);
        }
    }
    public class Salon
	{
        public int Grado { get; set; }
        public string Grupo { get; set; }
        public int Id { get; set; }
        public Salon(int grado, string grupo, int id)
        {
            Grado = grado;
            Grupo = grupo;
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0}° {1}", Grado, Grupo);
        }
        public static IEnumerable<Salon> ObtenerSalones() //IEnumerable, la interfase que implementan todas las listas, de tipo IEnumerable<Salon>.
        {
            Salon salon1 = new Salon(1, "A", 1);//crea objetos.
            Salon salon2 = new Salon(2, "B", 2);
            Salon salon3 = new Salon(3, "C", 3);
            Salon salon4 = new Salon(4, "C", 4);

            return new Salon[] { salon1, salon2, salon3,salon4   };//regresa un array de objetos, de tipo IEnumerable<Salon>.
        }

	}
}

