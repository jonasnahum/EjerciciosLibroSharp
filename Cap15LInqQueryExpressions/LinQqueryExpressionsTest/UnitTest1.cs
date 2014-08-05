using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap15LInqQueryExpressions;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace LinQqueryExpressionsTest
{
    [TestClass]
    public class UnitTest1
    {
        public void Print<T>(IEnumerable<T> lista, string titulo)
        {
            Debug.Print("----------------------{0}------------------------", titulo);
            foreach (var item in lista)
            {
                Debug.Print(item.ToString());
            }
        }

        [TestMethod]
        public void WhereTest()
        {
            IEnumerable<Alumno> alumnos = Alumno.ObtenerAlumnos();
            IEnumerable<Alumno>aprobados= from a in alumnos where a.Promedio>=6 select a;//a es cada alumno,in la lista. where filtro, y select, la proyeccion, lo que va regresar. la proyeccion con select es obligada.
            Print(aprobados, "Alumnos Aprobados");

            IEnumerable<int> edadesAprobados = from a in aprobados select a.Edad;// a aprobados ya se le aplico el where, ya estan filtrados,, solo se usa el select para hacer otra proyeccion.
            Print(edadesAprobados, "select Edades Aprobados");

            IEnumerable<Alumno> MayoresDeEdadyAprobados = from a in aprobados where a.Edad >= 18 select a;//el select siempre va hasta el ultimo.
            Print(MayoresDeEdadyAprobados, "Maryores de Edad y Aprobados");

            IEnumerable<Alumno> MenoresDeEdadyAprobados = from a in alumnos where a.Edad < 18 && a.Promedio >= 6 select a;//dos filtros se unen con &&, se puede usar cualquier operador.
        }
        [TestMethod]
        public void SortTest()
        {
            IEnumerable<Alumno> todosLosAlumnos = Alumno.ObtenerAlumnos();
            IEnumerable<string> nombresDescendente = from a in todosLosAlumnos orderby a.Nombre descending select a.Nombre;//no ocupa where porque no se ocupa filtrar, se ocupan todos los elementos de la lista.
            Print(nombresDescendente, "Lista de Nombres en Orden Descendente");

            IEnumerable<string> padresAscendente = from a in todosLosAlumnos orderby a.Padre.Nombre select a.Padre.Nombre;//el select se relaciona con el IEnumerable para regresar el mismo typo de dato.
            Print(padresAscendente, "Nombres de los Padres en Orden Ascendente");

            IEnumerable<decimal> promediosId1Desc = from a in todosLosAlumnos where a.IdSalon == 1 orderby a.Promedio descending select a.Promedio;
            Print(promediosId1Desc, "Promedios de alumnos que vayan al salon 1 ordenados en orden descendente");

        }
        [TestMethod]
        public void LetTest()
        {//hace una variable para reutilizarla en otra parte del query. y consumir menos recursos.
            IEnumerable<DirectoryInfo> directoriosEnC = from path in Directory.GetDirectories("c:")//aqui se regresa un array con los paths de los directorios.
                                                        orderby new DirectoryInfo(path).CreationTime//se crea un directory info, para ordenarlo por sy creation time que cooresponde a ese path.
                                                        select new DirectoryInfo(path);//Se regresa una proyeccion con el directory info correspondiente a ese path, pero se vuelve a crear un objeto, para representar al mismo directorio.

            IEnumerable<DirectoryInfo> masEficiente = from path in Directory.GetDirectories("c:\\")
                                                        let d = new DirectoryInfo(path)//se guarda en una variable y luego se reutiliza.
                                                        orderby d.CreationTime
                                                        select d;
            Print(masEficiente, "Directorios ordenados por fecha de creacion");
            
        }
        [TestMethod]
        public void GroupBy()
            //hace subgrupos de un grupo segun alguna propiedad en comun// no lleva select, la proyeccion la hace automaticamente.
        {
            IEnumerable<IGrouping<int, Alumno>> porSalon = from a in Alumno.ObtenerAlumnos() group a by a.IdSalon;
            foreach (var salon in porSalon)
            {
                Print(salon, "salon");
            }
            IEnumerable<IGrouping<Sexo, Alumno>> porSexo = from a in Alumno.ObtenerAlumnos() group a by a.Sexo;
            foreach (var porGenero in porSexo)
            {
                Print(porGenero, "por Genero");
            }

        }
        [TestMethod]
        public void IntoTest()
        {//es una variable como el let, pero para una lista.casi no se apl8ica.
            IEnumerable<Alumno> alumnos = Alumno.ObtenerAlumnos();
            var padreDeFamiliaSegunElSexoDelHijo = from a in alumnos
                                               group a by a.Sexo into porsexo //into porsexo es una variable temporal  igroupinsexoalumno.
                                               select new { Key = porsexo.Key, Alumnos = porsexo };


            IEnumerable<IGrouping<int, Alumno>> porSalon = from a in Alumno.ObtenerAlumnos() group a by a.IdSalon;
            var otro = from subgrupo in porSalon select new { Key = subgrupo.Key, Alumnos = subgrupo };            
        }
        [TestMethod]
        public void DistinctTest()
        {//saca un IEnumerable quitando los elementos que son repetidos, solamente pone cada valor una sola vez, si ya esta en la lista, ya no lo agrega.
            IEnumerable<Alumno> alumnos = Alumno.ObtenerAlumnos();
            IEnumerable<Sexo> generos = (from a in alumnos select a.Sexo).Distinct();
            Print(generos,"generos unicos");
           
        }


        [TestMethod]
        public void JoinTest()
        {
            IEnumerable<Alumno> alumnos = Alumno.ObtenerAlumnos();
            IEnumerable<Salon> salones = Salon.ObtenerSalones();

            var innerJoinPartial = from a in alumnos join s in salones on a.IdSalon equals s.Id select new { Alumno = a, Salon = s };
            var innerJoin = from it in innerJoinPartial select string.Format("{0} {1}", it.Alumno.Nombre, it.Salon.Grado);
            Print(innerJoin, "inner join");

            var leftJointPartial =
                (from a in alumnos
                 join
                     s in salones
                     on a.IdSalon equals s.Id into salonesAlumno//estan ya joinded.
                 select new { Alumno = a, Salones = salonesAlumno });

            var leftJoint = from it in leftJointPartial where it.Salones.Count() == 0 select it.Alumno;

            Print(leftJoint, "Left join");


            var rightJoinPartial =
                (from s in salones
                 join
                     a in alumnos
                     on s.Id equals a.IdSalon into alumnosSalon//ya estan joined.
                 select new { Salon = s, Alumnos = alumnosSalon });

            var rightJoin = from it in rightJoinPartial where it.Alumnos.Count() == 0 select it.Salon;

            Print(rightJoin, "Right join");

        }

    }
}
