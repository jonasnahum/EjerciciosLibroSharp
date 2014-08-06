using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap16CustomCollections;
using System.Collections.Generic;
using System.Linq;

namespace CustomCollectionsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DictionaryTest()
        {//la lista usa indice.
            IList<char> lista = new List<char>();
            lista.Add('m');
            lista.Add('e');
            lista.Add('d');

            char letra = lista[1];//index numerico.

            IDictionary<char, string> diccionario = new Dictionary<char, string>();//el diccionario usa key.
            diccionario.Add('m', "mexico");
            diccionario.Add('e', "elefante");
            diccionario.Add('d',"dado");

            string palabra = diccionario['e'];//se hace retrive con el key.,no pueden existir dos key iguales.

            Assert.AreEqual('e', letra);
            Assert.AreEqual("elefante", palabra);
        }

        [TestMethod]
        public void IComparerTest()
        {//se puede definir cual va a ser un ordenamiento segun algun criterio  descrito el un metodo IComparable.
            List<Alumno> alumnos = new List<Alumno>();
            alumnos.Add(new Alumno("Pedro"));
            alumnos.Add(new Alumno("Tono"));
            alumnos.Add(new Alumno("Ali"));
            alumnos.Add(new Alumno("Federico"));
            alumnos.Add(new Alumno("Marcelo"));

            Assert.AreEqual("Pedro", alumnos[0].Nombre);//aqui tienen el orden en que se agregaron a la colleccion.
            Assert.AreEqual("Marcelo", alumnos[4].Nombre);
            alumnos.Sort();//el metodo que hace el ordenamiento esta el Alumno que implementa IComparable. los ordena por su length.
            Assert.AreEqual("Ali", alumnos[0].Nombre);
            Assert.AreEqual("Federico", alumnos[4].Nombre);

        }
        [TestMethod]
        public void ICollectionTest()
        {
            ICollection<int> coleccion = new List<int>();
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

            int[] arrayEnteros = new int[4];
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
            SortedDictionary<Alumno, int> alumnosEdad = new SortedDictionary<Alumno, int>();//ordenado en base al comparer que se encuentra en la clase Alumno., se van ordenando cuando se van agregando
            Stack<int> platos = new Stack<int>();
            Queue<string> tunel = new Queue<string>();
            LinkedList<string> tren = new LinkedList<string>();
            
            Alumno pepe = new Alumno("pepe");
            Alumno maryJose = new Alumno("maryJose");
            Alumno juana = new Alumno("juana");

            alumnosEdad.Add(pepe, 12);//las edades en este caso 12, son valor para el diccionario, a las cuales se accesara por medio del alumno, que es el key.
            alumnosEdad.Add(maryJose, 15);
            alumnosEdad.Add(juana, 18);

            foreach (KeyValuePair<Alumno,int> item in alumnosEdad )//internamente el diccionario una colleccion key value pair. y una propiedad key y otra propiedad values.
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

    }
}
