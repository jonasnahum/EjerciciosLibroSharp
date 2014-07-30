using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap14StandarQueryOperator;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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
        {//se usa para filtrar datos.
            IEnumerable<Alumno> lista = Alumno.ObtenerAlumnos();//se asignan todos los alumnos a la variable lista.
            IEnumerable<Alumno> alumnosConInicialA;//se declara otra variable.
            Func<Alumno, bool> filtro = a => a.Nombre.ToLower().StartsWith("a");//se declara un delegado y se le asigna un metodo.
            alumnosConInicialA = lista.Where(filtro);

            System.Diagnostics.Debug.Print("---------------Empiezan con 'a'---------------");            
            Print(alumnosConInicialA);

            System.Diagnostics.Debug.Print("---------------Reprobados---------------");
            IEnumerable<Alumno> alumnosReprobados;
            alumnosReprobados = lista.Where(a => a.Promedio < 6);//where recibe como parametro un delegado. que en este caso es de tipo alumno y regresa un bool.
            Print(alumnosReprobados);

            System.Diagnostics.Debug.Print("---------------Adultos, Aprobados, Empiece o termine con vocal---------------");
            string[] vocales=new string[] {"a","e","i","o","u","A","E","I","O","U"};
            Print(lista.Where(a => a.Edad > 18 && a.Promedio >= 6 && (vocales.Contains(a.Nombre.Substring(0, 1)) || vocales.Contains(a.Nombre.Substring(a.Nombre.Length-1,1)))));

            System.Diagnostics.Debug.Print("---------------Menores Aprobados---------------");
            Print(lista.Where(a => a.Edad < 18 && a.Promedio >= 6));//where recibe un metodo, un lambda, a su vez el lambda recibe un Alumno y regresa un bool.el where regresa un IEnumerable con los elementos filtrados.

        }
        [TestMethod]
        public void SelectTest()//ocupa el using System.Linq
        {// sirve para hacer proyecciones, la proyeccion es convertir un IEnumerable de un tipo en otro IEnumerable de otro tipo diferente.
            IEnumerable<Alumno> lista = Alumno.ObtenerAlumnos();
            IEnumerable<int> longitudNombres;
            longitudNombres = lista.Select(a => a.Nombre.Length);
            Print(longitudNombres);

            IEnumerable<PadreDeFamilia> papasReprobados;
            papasReprobados = lista.Where(a => a.Promedio < 6).Select(a=>a.Padre);//de una coleccion de alumnos reprobados se convierte a una coleccion de padres de familia, a la conversion se le llama proyeccion.
            System.Diagnostics.Debug.Print("---------------Papas de reprobados---------------");
            Print(papasReprobados);

            IEnumerable<char> inicialNombreAlumno;
            inicialNombreAlumno = lista.Select(a => a.Nombre[0]);//el Select esta convirtiendo.
            System.Diagnostics.Debug.Print("---------------Iniciales de Alumnos---------------");
            Print(inicialNombreAlumno);

            var nombreAlumnoNombrePadre = lista.Select(a => new { NombrePadre = a.Padre.Nombre, NombreAlumno = a.Nombre });//proyeccion a un tipo anonimo, convierte una lista de tipo Alumno a un tipo anonimo.
            System.Diagnostics.Debug.Print("---------------Proyeccion a un Tipo Anonimo---------------");
            Print(nombreAlumnoNombrePadre);
        }
    }
}
