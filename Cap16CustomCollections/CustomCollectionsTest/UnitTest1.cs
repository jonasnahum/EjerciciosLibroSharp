using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap16CustomCollections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace CustomCollectionsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ListaVSDictionaryTesty()
        {//la lista usa indice.
            IList<char> lista = new List<char>();
            lista.Add('m');
            lista.Add('e');
            lista.Add('d');

            char letra = lista[1];//index numerico. busca 'e y lo asigna a letra, lista tiene index  numerico.

            //el diccionario usa key que puede ser cualquier cosa aparte del numero, busca con base en key.
            IDictionary<char, string> diccionario = new Dictionary<char, string>();//cada elemento es un string.
            diccionario.Add('m', "mexico");
            diccionario.Add('e', "elefante");
            diccionario.Add('d',"dado");

            string palabra = diccionario['e'];//se hace retrive con el [key].,no pueden existir dos key iguales.

            Assert.AreEqual('e', letra);
            Assert.AreEqual("elefante", palabra);
        }

        [TestMethod]
        public void IComparerTest()//Sort()//ordena la coleccion utilizando IComparer.
        {//se puede definir cual va a ser un ordenamiento de la colleccion segun algun criterio  descrito el un metodo CompareTo de la class Alumno:IComparable<Alumno> en este caso el metodo regresa un int que va ordenando segun la longitud del nombre.
            List<Alumno> alumnos = new List<Alumno>();
            alumnos.Add(new Alumno("Pedro"));
            alumnos.Add(new Alumno("Tono"));
            alumnos.Add(new Alumno("Ali"));
            alumnos.Add(new Alumno("Federico"));
            alumnos.Add(new Alumno("Marcelo"));

            Assert.AreEqual("Pedro", alumnos[0].Nombre);//aqui tienen el orden en que se agregaron a la colleccion.
            Assert.AreEqual("Marcelo", alumnos[4].Nombre);
            alumnos.Sort();//el metodo que hace el ordenamiento esta el Alumno que implementa IComparable. los ordena por su length.
            Assert.AreEqual("Ali", alumnos[0].Nombre);//el nombre mas peque;o debe estar en primer lugar ahora.
            Assert.AreEqual("Federico", alumnos[4].Nombre);

        }
        [TestMethod]
        public void ICollectionTest()
        {//Count. en este caso es una propiedad que cuenta los elementos en la coleccion. es de ICollection
        //IsReadOnly. propiedad de ICollection que devuelve un bool que dice si es ReadOnly.
        //Add(). es un metodo.de ICollection.
        //Clear().borra los datos con todo y su casillero.
        //Contains(). dice en un bool si la coleccion contiene un dato en particular.
        //CopyTo().copia un coleccion y lo pasa a un array, empezando a copiar en el indice del array que se le pasa como parametro.
        //Remove().con el remove, deja de existir el casillero., borrar la primera ocurrencia, la primera vez que ocurre un numero o dato en la colleccion.  
            ICollection<int> coleccion = new List<int>();//list<> implementa ICollection.//asi Lista implementa las propiedades y metodos de ICollection.
            coleccion.Add(2);
            coleccion.Add(4);
            coleccion.Add(5);
            coleccion.Add(6);

            Assert.AreEqual(4, coleccion.Count);
            Assert.AreEqual(false, coleccion.IsReadOnly);

            coleccion.Clear();
            Assert.AreEqual(0, coleccion.Count);
            coleccion.Add(2);
            coleccion.Add(4);
            coleccion.Add(5);
            coleccion.Add(6);
            Assert.AreEqual(true, coleccion.Contains(5));//contains regresa true o false.
            Assert.AreEqual(false, coleccion.Contains(10));

            int[] arrayEnteros = new int[4];//se reservan 4 espacios en el array.
            coleccion.CopyTo(arrayEnteros, 0);//copia un coleccion y lo pasa a un array de enteros, empezando en el indicee que se le pasa como parametro.
            Assert.AreEqual(2, arrayEnteros[0]);
            Assert.AreEqual(6, arrayEnteros[3]);

            coleccion.Remove(4);//con el remove, deja de existir el casillero., borrar la primera ocurrencia, la primera vez que ocurre un 4 en la colleccion.
            Assert.AreEqual(false,coleccion.Contains(4));
        }
        [TestMethod]
        public void PrimaryCollectionClasses()
        {
            List<int> listaEnteros = new List<int>();
            Dictionary<char, string> diccionario = new Dictionary<char, string>();//el key es char, y el valor es string. tiene los elementos en el orden en el que yo se los agregue.
            SortedDictionary<Alumno, int> alumnosEdad = new SortedDictionary<Alumno, int>();//ordenado en base al comparer que se encuentra en la clase Alumno., se van ordenando automaticamente cuando se van agregando los datos.
            Stack<int> platos = new Stack<int>();
            Queue<string> tunel = new Queue<string>();
            LinkedList<string> tren = new LinkedList<string>();
            
            Alumno pepe = new Alumno("pepe");
            Alumno maryJose = new Alumno("maryJose");
            Alumno juana = new Alumno("juana");

            alumnosEdad.Add(pepe, 12);//se agregan datos al diccionario sorted la edade en este caso 12, son valor para el diccionario, a las cuales se accesara por medio del alumno, que es el key.
            alumnosEdad.Add(maryJose, 15);
            alumnosEdad.Add(juana, 18);

            foreach (KeyValuePair<Alumno,int> item in alumnosEdad )//internamente el diccionario tiene una colleccion key value pair. y una propiedad key y otra propiedad value.
            {
                System.Diagnostics.Debug.Print("{0} {1}", item.Key.Nombre, item.Value.ToString());
            }

            //con linked, se puede decir a la coleccion en que lugar se va a guardar cada valor..
            var maquina = new LinkedListNode<string>("maquina");
            var vagon1 = new LinkedListNode<string>("vagon1");
            var vagon2 = new LinkedListNode<string>("vagon2");
            
            tren.AddFirst(maquina);
            tren.AddAfter(maquina,vagon1);
            tren.AddBefore(vagon1, vagon2);

            foreach (string vagon in tren)
            {
                System.Diagnostics.Debug.Print(vagon);  
            }


        }
        [TestMethod]
        public void IndexOperatorTest()
        {//casi no se usan , porque son opacados por las primary collections.
            ListaAlumnos lista = new ListaAlumnos();//la clase contiene una lista de Alumnos, pero es privada, solo se accesa a ella por medio del indexOperator.
            lista[0] = new Alumno("pepe");//set.se guarda un objeto alumno en la propiedad que corresponde al casillero[0] que es Alumno1 y se envia string pepe en nombre.
            Alumno pepe = lista[0];//get a travez del indexOperator. lo que hay en Alumno1 (hay un string "pepe")se asigna a pepe, esta variable de tipo Alumno.
            Assert.AreEqual("pepe", lista[0].Nombre);//se busca por enteros[0], su index que es como el key. lista[0] contiene un alumno por eso se le puede aplicar la propiedad Nombre.

            lista[1] = new Alumno("pedro");//se asigna la propiedad Nombre de un nuevo objeto alumno y se guarda en casillero [1], que corresponde a la propiedad Alumno2 de la clase ListaAlumnos.
            lista[2] = new Alumno("juan");
            lista[3] = new Alumno("tono");
            lista[4] = new Alumno("saul");

            Alumno saul = lista["enrique", "saul"];//que busque en lista a [enrique y saul], el primero que encuentre, lo asigne a la variable saul//lista de la clase LIstaAlumnos, en su index una sobre carga, puede recibir un int, o un array de strings.
            Assert.AreEqual("saul", saul.Nombre);//si lo encontro. enrique y saul, son comparados  contra la lista, y el primero que contega la lista es el que se guarda, en este caso, se guardo saul, porque este si se encuentra en la lista.

            lista["tono"] = new Alumno("joseAntonio");//al objeto que en su indice tiene tono, cambiarlo por joseAntonio.
            Assert.AreEqual("joseAntonio", lista["joseAntonio"].Nombre);//se busca por string index,
        }

        public IEnumerable<string> GetStrings() //hace una clase internamete que regresa todos los valores que se ingresaron aqui.
        {
            yield return "uno";//yield, palabra reservada que permite tener varios return.
            yield return "dos";//yield, implementa las interfaces necesarias para obtener un IEnumerable de strings.
            yield return "tres";
            yield return "cuatro";
            yield return "cinco";
        }
        public IEnumerable<int> GetPares(int limite) 
        {
            int inicial = 0;
            while (inicial % 2 == 0 && inicial <= limite) 
            {
                yield return inicial += 2;
            }
        }
        public IEnumerable<char> GetAbecedario()
        {
            char a = 'a';
            int inicial = (int)a;
            int final = inicial + 26;
            while (inicial < final)
            {
                yield return (char)inicial;//lista que requiere cierta logica en su construccion
                inicial++;
            }
        }
        public IEnumerable<int> EjemploSimpleYieldBreak(int iterciones)
        {            
            int i = 1;
            while (true)
            {
                yield return i;
                i++;

                if (iterciones == i) 
                {
                    yield break;//aqui se termina la produccion de la lista.
                }
            } 
            
        }
        public IEnumerable<DateTime> ObtenerFinesDeSemana(DateTime inicio, DateTime fin)//construir lista de fechas.
        {
            DateTime apuntador = inicio;
            
            while (apuntador < fin) {
                bool esFinDeSemana = apuntador.DayOfWeek == DayOfWeek.Sunday || apuntador.DayOfWeek == DayOfWeek.Saturday;
                               
                if (esFinDeSemana) 
                { 
                    yield return apuntador;
                }
                
                apuntador = apuntador.AddDays(1);
            }
            
        }



        [TestMethod]
        public void YieldTest()//construye de manera automatica e interna una clase que implementa IEnumerable.
        {//cuando se quiere hacer una lista de tipo IEnumerable que requiere cierta logica.. la produce de manera automatica.
            IEnumerable<string> miEnumerator = new MiIEnumerable();//IEnumerable es un contrato general por lo tanto se puede transformar un muchos tipos de lista.
            IEnumerable<string> yieldEnumator = GetStrings();
            Print(miEnumerator, "mi Enumerator");
            Print(yieldEnumator, " Yield Enumerator");
            IEnumerable<int> pares = GetPares(15);
            Print(pares, "Produccion de Numeros Pares Con Yield");

            List<string> otroTipo = miEnumerator.ToList();
            int i=-1;
            Dictionary<int, string> otroTipo1 = miEnumerator.ToDictionary(it => i++);
            string[] otroTipo2 = miEnumerator.ToArray();

            Print(GetAbecedario(), "Abecedario hecho con Yeld");


            Print(EjemploSimpleYieldBreak(5), "Lista de Numeros Enteros");

            DateTime inicio = new DateTime(2014, 08, 1);
            DateTime fin = new DateTime(2014, 08, 31);
            IEnumerable<DateTime> finesDeSemana = ObtenerFinesDeSemana(inicio, fin);
            Print(finesDeSemana, "Fines de Semana Agosto 1014");

            var diasFinPar = from f in finesDeSemana where f.Day % 2 == 0 select f;//IEnumarable tiene acceso a todos los standart query operators.
            
        }

        public void Print<T>(IEnumerable<T> lista, string titulo)
        {
            System.Diagnostics.Debug.Print("----------------------------{0}----------------------------", titulo);
            foreach (T item in lista)
            {
                System.Diagnostics.Debug.Print(item.ToString());
            }
        }
        

        public List<string> ObtenrePalabras(bool comoNulo) 
        {
            if (comoNulo) return null;
            return new List<string>();//si como nulo es falso, return empty.
        }
        [TestMethod]
        public void NullOrEmptyCollection()
        {//es preferible obtener una lista vacia que algo nullo, porque este marca error.
            List<string> palabras = ObtenrePalabras(false);
            List<string> palabrasNulo = ObtenrePalabras(true);

            foreach (var item in palabras)
            {
                System.Diagnostics.Debug.Print(item);
            }
        }

    }
}
