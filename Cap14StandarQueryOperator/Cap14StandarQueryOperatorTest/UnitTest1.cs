using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap14StandarQueryOperator;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Diagnostics;

namespace Cap14StandarQueryOperatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VarTest()
        {
            var variable = new string[] { "hola" };//infiere el tipo del lado derecho de =.
            var otraVar = "hola";
            var x = 1;

            var persona = new { Nombre = "jonas", Edad = 23, Apellido = "jimenez" };//typo Anonimo, es una clase, que guarda propiedades en forma breve. pero no tiene nombre. no se pueden agregar propiedades, ni hacer m[as objetos.
            Assert.AreEqual("jonas", persona.Nombre);
            Assert.AreEqual(23, persona.Edad);
            Assert.AreEqual("jimenez", persona.Apellido);

            Assert.AreEqual("hola", variable[0]);
            Assert.AreEqual("hola", otraVar);
            Assert.AreEqual(1, x);

            TypeTest(variable);
            TypeTest(otraVar);
            TypeTest(x);
            TypeTest(persona);
        }

        public void TypeTest<T>(T variable) //recibe una variable de cualquier typo y las compara.
        {
            Type tipoVar = variable.GetType();
            Type tipoTipo = typeof(T);
            Assert.AreEqual(tipoTipo, tipoVar);
            System.Diagnostics.Debug.Print(tipoVar.ToString());
        }
        [TestMethod]
        public void CollectionInitializerTest()
        {
            List<string> lista = new List<string>() { "hola", "mundo" };//List es una clase generica de string que tiene using System.collections.generics., inicializada con dos valores. 
            lista.Add("mexico");//aqui, lista tiene 3 elementos.
            lista.Remove("hola");//ahora tiene 2 elementos.

            List<int> enteroslista = new List<int>() {2,3,4,5};//enteroslista es un objeto de tipo List<int> se instancia con new.
            Print(lista);
            Print(enteroslista);

            int[] array = new int[] { 1, 2, 3 };
            Print(array);//el array tambien implementa IEnumerable<T>, como todas las listas.

            IEnumerator enumerador = array.GetEnumerator();//este parrafo nomas para demostrar que el array tambien tiene algunas propiedades de la lista.
            while (enumerador.MoveNext())//GetEnumerator esta dentro de IEnumerable y regresa un IEnumerator, el cual contiene current, move next y reset.
            {
                System.Diagnostics.Debug.Print(enumerador.Current.ToString()); 
            }
            enumerador.Reset();//se fue iterando y quedo al final, debemos regresarlo al principio con reset.


            while (enumerador.MoveNext())//se puede repetir el proceso porque se le ha dado reset, volver al principio.
            {
                System.Diagnostics.Debug.Print(enumerador.Current.ToString());
            }
        }

        public static void Print<T>(IEnumerable<T> items)//recibe como parametro un Ienumerable generico, que es la interface que implementan todas las listas y que a su vez contiene los metodos reset, move next,y current.
        {
            foreach (T item in items)// primero el tipo, luego singular, y luego plural.
            {
                System.Diagnostics.Debug.Print(item.ToString());//mientras esta iterando no se puede modificar, ReadOnly, y sola va hacia adelante  el move next.
            }
            // Reset() en IEnumerable vuelve al principio
            //foreach (T item in items)
            //{
            //    System.Diagnostics.Debug.Print(item.ToString());
            //}

        }
        [TestMethod]
        public void StackTest()
        {
            Stack<string> pilaPlatos = new Stack<string>(); //LIFO last in first out
            Queue<string> tunel = new Queue<string>(); //FIFO first in first out

            pilaPlatos.Push("plato1");
            pilaPlatos.Push("plato2");
            pilaPlatos.Push("plato3");
            pilaPlatos.Push("plato4");
            pilaPlatos.Push("plato5");

            pilaPlatos.Pop();
            Print(pilaPlatos);//saco el ultimo , el 5, entonces en la lista quedan del 1 al 4. pop, saca.lifo. tambien implementan IEnumerable.

            tunel.Enqueue("carro blanco");
            tunel.Enqueue("carro verde");
            tunel.Enqueue("carro azul");
            tunel.Enqueue("carro taxi");
            tunel.Enqueue("carro camioneta");
            tunel.Dequeue();
            Print(tunel);// sale del tunel el carro blanco, entonces, estan todos los demas fifo.Tambien implementa IEnumerable.

        }
        [TestMethod]
        public void WhereTest()//ocupa el using System.Linq
        {//where se usa para filtrar datos.
            IEnumerable<Alumno> lista = Alumno.ObtenerAlumnos();//se asignan todos los alumnos a la variable lista.
            IEnumerable<Alumno> alumnosConInicialA;//se declara otra variable.
            Func<Alumno, bool> filtro = a => a.Nombre.ToLower().StartsWith("a");//se declara un delegado y se le asigna un metodo.//para ser mas practico, esto deberia ir como parametro en el where.
            alumnosConInicialA = lista.Where(filtro);
            System.Diagnostics.Debug.Print("---------------Empiezan con 'a'---------------");            
            Print(alumnosConInicialA);//Print es un metodo que recibe un IEnumerable<T>

            System.Diagnostics.Debug.Print("---------------Reprobados---------------");
            IEnumerable<Alumno> alumnosReprobados;
            alumnosReprobados = lista.Where(a => a.Promedio < 6);//where recibe como parametro un delegado. que en este caso es de tipo alumno y regresa un bool porque va diciendo si es true o false que el promedio es>6, a su vez tambien regresa de ahi un IEnumerable.
            Print(alumnosReprobados);

            System.Diagnostics.Debug.Print("---------------Adultos, Aprobados, Empiece o termine con vocal---------------");
            string[] vocales=new string[] {"a","e","i","o","u","A","E","I","O","U"};
            Print(lista.Where(a => a.Edad > 18 && a.Promedio >= 6 && (vocales.Contains(a.Nombre.Substring(0, 1)) || vocales.Contains(a.Nombre.Substring(a.Nombre.Length-1,1)))));

            System.Diagnostics.Debug.Print("---------------Menores Aprobados---------------");
            Print(lista.Where(a => a.Edad < 18 && a.Promedio >= 6));//where recibe un metodo, un lambda, a su vez el lambda recibe un Alumno y regresa un bool.el where regresa un IEnumerable con los elementos filtrados y lo pasa a lista, y lista lo pasa a print.

        }
        [TestMethod]
        public void SelectTest()//ocupa el using System.Linq
        {// sirve para hacer proyecciones, la proyeccion es convertir un IEnumerable de un tipo en otro IEnumerable de otro tipo diferente.
            IEnumerable<Alumno> lista = Alumno.ObtenerAlumnos();//se declara variable de tipo IEnumerable<Alumno>
            IEnumerable<int> longitudNombres;//es un tipo IEnumerable diferente.
            longitudNombres = lista.Select(a => a.Nombre.Length);//de lado izquierdo es IEnumerable<Int>, y del lado derecho trabaja con IEnumarable<Alumno>, hace una proyeccion.
            Print(longitudNombres);//Print() es un metodo que recibe un IEnumerable<T>.

            IEnumerable<PadreDeFamilia> papasReprobados;//se declara una variable y su tipo.
            papasReprobados = lista.Where(a => a.Promedio < 6).Select(a=>a.Padre);//de una coleccion de alumnos reprobados se convierte a una coleccion de padres de familia, a la conversion se le llama proyeccion.primero ejecuta lista hasta el punto, luego dentro de los parentesis hasta el punto,  luego el select .
            System.Diagnostics.Debug.Print("---------------Papas de reprobados---------------");
            Print(papasReprobados);

            IEnumerable<char> inicialNombreAlumno;
            inicialNombreAlumno = lista.Select(a => a.Nombre[0]);//el string se comporta como un char
            System.Diagnostics.Debug.Print("---------------Iniciales de Alumnos---------------");
            Print(inicialNombreAlumno);

            var nombreAlumnoNombrePadre = lista.Select(a => new { NombrePadre = a.Padre.Nombre, NombreAlumno = a.Nombre });//proyeccion a un tipo anonimo, convierte una lista de tipo Alumno a un tipo anonimo.
            System.Diagnostics.Debug.Print("---------------Proyeccion a un Tipo Anonimo---------------");
            Print(nombreAlumnoNombrePadre);

        }

        [TestMethod]
        public void Paralel()
        {//pone a trabajar todos los procesadores para hacer la misma operacion y divide el trabajo y lo hace mas rapido.
            List<Alumno> lista = Alumno.ObtenerAlumnos().ToList();//ObtenerAlumnos es de tipo IEnumerable, lo convierte a lista y lo guarda en variable lista.
            Action<Alumno> imprimirAlumno = a =>
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        throw new NotImplementedException();
                    }
                    catch
                    {
                    }
                }
            };//un lambda que tira errores, para gastar memoria y luego contar el tiempo que tarda en tirar errores.

            DateTime inicio = DateTime.Now;//se registra el tiempo de inicio.
            lista.ForEach(imprimirAlumno);//a cada elemento en List<Alumno>lista, se le aplica la variable delegado immpmrimirAlumno que recibe un Alumno, porque cada elemto es de tipo Alumno, y todos juntos son list<Alumno>.
            TimeSpan tiempo = inicio - DateTime.Now;//la variable tiempo, saca la diferencia de tiempos, el span de lo que duro la operacion throw.
            Debug.Print("{0}: {1}", "sin parallel", tiempo.Milliseconds);

            inicio = DateTime.Now;
            lista.AsParallel().ForAll(imprimirAlumno);//a la lista se le aplica AsParallel, y  luego su metodo forAll imprimirAlumno.
            tiempo = inicio - DateTime.Now;//se saca la diferencia.
            Debug.Print("{0}: {1}", "con parallel", tiempo.Milliseconds);
        }
        [TestMethod]
        public void CountTest()
        {//cuenta los elementos de una lista segun el metodo lambda, regresa un numero, un valor. tiene una sobre carga que recibe un lambda y otra que no recibe , que solo cuenta.
            int cantidadDeAlumnosAprobados = Alumno.ObtenerAlumnos().Count(a => a.Promedio >= 6);
            Debug.Print("De {0} alumnos, {1} aprobaron", Alumno.ObtenerAlumnos().Count(), cantidadDeAlumnosAprobados);
        }
        [TestMethod]
        public void AnyTest()
        {// recorre la lista y regresa un true, si existe la condicion que se paso como lambda, si no lleva parametro el Any, regresa true si la lista contiene por lo menos 1 elemento.
            if (Alumno.ObtenerAlumnos().Any())//los if llevan entre parentesis un bool.//si obtener alumnos contiene por lo menos 1 elemento.
            {
                Debug.Print("esta lista contiene por lo mentos 1 elemento");
            }
            if (Alumno.ObtenerAlumnos().Any(a=>a.Promedio<6))//si por lo menos hay un elemento reprobado.
            {
                Debug.Print("por lo menos hay un alumno reprobado");
            }
        }
        [TestMethod]
        public void OtrosMetodosQueOperanSobreIEnumerable() 
        {//Avarage().saca el promedio de una secuencia, segun su typo.
            var alumnos = Alumno.ObtenerAlumnos();
            decimal promedioGeneralDelGpo= alumnos.Average(a => a.Promedio);
            Debug.Print(promedioGeneralDelGpo.ToString());
        }

        public bool EsAprobado(Alumno alumno)//este metodo equivale a un lambda.
        {
            bool aprobo = alumno.Promedio >= 6;
            return aprobo;
        }

        [TestMethod]
        public void DeferredExecutionTest()
        {// los query operators se ejecutan hasta el punto en que ejecuta una operacion o se solicita algun dato de la colleccion sobre la que actuan.
            IEnumerable<Alumno> todosLosAlumnos = Alumno.ObtenerAlumnos();
            IEnumerable<Alumno> aprobados = todosLosAlumnos.Where(EsAprobado);//aqui el where sabe lo que va y como lo va hacer, pero no se ejecuta.

            int cantidadDeAlumnos = 0;
            cantidadDeAlumnos = aprobados.Count();//en este momento se ejecuta el where y su parametro.
        }
        [TestMethod]
        public void IEnumerableConvertionTest()
        {//Un IEnumerable, se convierte facilmente a un array o a un List.
            IEnumerable<Alumno> alumnos = Alumno.ObtenerAlumnos();//este es un IEnumerable.
            Alumno[] alumnosArray = alumnos.ToArray();
            List<Alumno> alumnosList = alumnos.ToList();
        }
        [TestMethod]
        public void OrdenamientoIEnumerable()
        {//OrderBy.. ordena de a-z o de 1--2,
         //ThenBy. es utilizado para agregar un segundo criterio de ordenamiento.
            IEnumerable<Alumno> alumnos = Alumno.ObtenerAlumnos();
            Debug.Print("--------------------Alumnos en Orden Alfabetico Z-A-----------------------------------");
            IEnumerable<Alumno> ordenados = alumnos.OrderByDescending(a => a.Nombre);
            Print(ordenados);

            Debug.Print("-------------------Ordenado por Promedio de Menor a Mayor-----------------------------------");
            ordenados = alumnos.OrderBy(a => a.Promedio);
            Print(ordenados);

            Debug.Print("--------------------Ordenado Por Orden Alfabetico y Luego Por Edad-----------------------------------");
            ordenados = alumnos.OrderBy(a => a.Nombre).ThenBy(a=>a.Edad);//el primer criterio es ordenado, y Then By es un criterio secundario de ordenamiento, si hay dos nombres iguales, pone primero el de menor edad.
            Print(ordenados);
        }
        [TestMethod]
        public void JoinOperators()
        {//one to many.//un salon a varios alumnos.
            IEnumerable<Alumno> todosLosAlumnos = Alumno.ObtenerAlumnos();
            IEnumerable<Salon> todosLosSalones = Salon.ObtenerSalones();
            //var grupo = todosLosAlumnos.Join(todosLosSalones,//todos los alumnos hacen join con todos los salones. 
            //    a => a.IdSalon,//foreing key, porque el id esta es el numero de salon y el salon es una caracteristica externa, que esta en salon no aqui en alumno.
            //    s => s.Id, //primary key, porque el id identifica a cada uno de los elementos de salon y con base en este, se agrupan los alumnos.
            //    (a, s) => new
            //    { //aqui ha mandado todos los elementos que se pudieron juntar con el join, para hacer una proyeccion.,se hace una clase anonyma.
            //        Alumno = a,
            //        Salon = s,
            //    });//la variable grupo, al final tiene un IEnumerable de esta clase anonyma.
            //Debug.Print("-------------------Inner Join-----------------------------------");
            //Print(grupo.Select(anonymo=>string.Format("{0}-{1}",anonymo.Alumno.ToString(),anonymo.Salon.ToString())));
            var grupo = todosLosAlumnos.Join(todosLosSalones,//first secuence(inner)                inner, to join to the firstsecuence.
                a => a.IdSalon,//OuterKeySelector. a funcion to extract the join key from each element of the first secuence.
                s=>s.Id,//innerKeySelector a funtion to extract the join key from each element of the second secuence.
                (a, s) => new//result selector. a function to create a result element from two matching elements.
                    {
                        Alumno = a,
                        Salon = s,
                    });
            Debug.Print("-------------------Inner Join-----------------------------------");
            Print(grupo.Select(anonymo => string.Format("{0}-{1}", anonymo.Alumno.ToString(), anonymo.Salon.ToString())));

            Debug.Print("-------------------Right Join-----------------------------------");            
            var rightResult = todosLosSalones.GroupJoin(todosLosAlumnos,
            s => s.Id,
            a => a.IdSalon,
            (s, alumnos) => new//hay objetos combinados cada uno tiene salon y alumno. aqui ya esta la proyeccion en la cual cada salon ya tiene sus alumnos.estan agrupados en base a su propiedad comun.
            {
                Alumnos = alumnos,
                Salon = s//right result, la variable, tiene un IEnumerable de objetos de esta clase anonyma, cada objeto, tendra estas dos propiedades.
            });

            Print(rightResult.Where(it => it.Alumnos.Count() == 0).Select(it=>it.Salon));//buscar en cada objeto de la clase anonyma en donde sus alumnos=0, de esa lista hacer una proyeccion y sacar los salones.

            Debug.Print("-------------------Left Join-----------------------------------");
            var leftJoint = todosLosAlumnos.GroupJoin(todosLosSalones,
            a => a.IdSalon,
            s => s.Id,
            (a, salones) => new
            {
                Salones = salones,
                Alumno = a
            });
            Print(leftJoint.Where(classAnonym => classAnonym.Salones.Count() == 0).Select(classAnonym => classAnonym.Alumno));//solovino y pantera si tienen salon, pero sus Key selector no encuentra relacion con un salon.
        }
    }
}
