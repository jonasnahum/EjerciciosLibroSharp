using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap17ReflecAtribDynamic;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace ReflecAtrib_DynamicTest
{
    [TestClass]
    public class UnitTest1
    {//Reflection. conjunto de clases que permiten obtener informacion de codigo y generar codigo.
        [TestMethod]
        public void TypeTest()
        {//typeof saca los metadatos de un typo y los guarda en una variable de tipo Type.regresa un objeto tipo Type.
            Type typeAlumno = typeof(Alumno);//opera sobre el tipo en este caso Alumno.Type guarda metadatos respecto de un tipo, en este caso , de Alumno.
            TypeDescriptor typeDescriptor = new TypeDescriptor(typeAlumno);//la instancia recibe en su constructor un objeto de tipo Type.
            
            typeDescriptor.Print();

            //Alumno alumno = new Alumno("pancho");
            //Type datosdeAlumno = alumno.GetType();//hace lo mismo que typeof pero este opera sobre una instancia.

            //Type decimalT = typeof(decimal);
            //typeDescriptor.Type = decimalT;//se guarda en la property de typeDescriptor.
            //typeDescriptor.Print();
        }

        [TestMethod]
        public void ActivatorTest()
        { //activator ayuda a crear instancias.
            Dictionary<string,string> propiedades=new Dictionary<string,string>();//<T key, Tvalue>.
            propiedades.Add("Nombre","Juan");
            propiedades.Add("Edad","15");
            propiedades.Add("Apellido","Escutia");

            Alumno instanciaCreada=CreadorDeInstancias.CrearInstancia<Alumno>(propiedades);//er al metodo generico CrearInstancias de tipo T y pasarle Alumno como tipo y propiedades, el diccionario que tiene como key un string.
            Profesor profe = CreadorDeInstancias.CrearInstancia<Profesor>(propiedades);


            TypeDescriptor.Print(CreadorDeInstancias.ClassesEnAssembly(typeof(Alumno)), "Clases en assembly");
            
        }
        //[TestMethod]
        public void NoTestMethodAttribute ()//gracias al attribute [TestMethod], es que visual studio evalua los asserts .
        {
            Assert.IsTrue(false);

        }
        [TestMethod]
        public void AttributeTest()
        {//para marcar un elemento y/o asociarle una funcion extra.
            Profesor alumno = new Profesor();
            alumno.Nombre = "juan";
            alumno.Edad = 17;
            alumno.Apellido = "Escutia";
            TypeDescriptor.GetAttributeInfo(alumno);
        }
        public string Serializar<T>(T miObjeto) 
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);

            serializer.Serialize(writer, miObjeto);
            var xml = sww.ToString(); // Your xml

            return xml;
        }

        public T DeserializarXml<T>(string xml) 
        {
            xml = xml.Replace("utf-16", "utf-8");
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(xml);
            writer.Flush();
            stream.Position = 0;
            

            T miObjeto = (T)serializer.Deserialize(stream);

            return miObjeto;
        }

        [TestMethod]
        public void SerializationTest()
        {//serializa un objeto, la convierte tal vez a texto xml, guarda su estado, y luego se puede volver hacer el objeto y con base en el texto, recuperar los datos.
            Persona persona = new Persona();
            persona.Nombre = "Agustin";
            persona.Edad = int.Parse("45");
            persona.Apellido = "Melgar";
            persona.Password = "hola";
            

            string objetoSerializado = Serializar(persona);//se manda un oobjeto persona de tipo Persona al metodo Serializar.
            persona = null;//se le borran los datos que de sus propiedades: agustin 45, melgar, etc.
            System.Diagnostics.Debug.Print(objetoSerializado);//imprime los datos que se guardaron en xml.

            Persona persona2 = DeserializarXml<Persona>(objetoSerializado);//manda un tipo persona y una variable de tipo string, y la guarda en persona2.
            Assert.AreEqual("Agustin", persona2.Nombre);
        }

        [TestMethod]
        public void OtroDynamicTest()
        {
            Persona persona = new Persona();
            persona.Nombre = "Agustin";
            persona.Edad = int.Parse("45");
            persona.Apellido = "Melgar";
            persona.Password = "hola";

            string objetoSerializado = Serializar(persona);//objetoSerialializado contiene objeto persona ya serializado.

            XElement e = XElement.Parse(objetoSerializado);
            Assert.AreEqual("Agustin",e.Descendants("Nombre").FirstOrDefault().Value);//descendants regresa un ienumerable de nodos nombre, como se pueden repetir o no haber ninguno, que regrese el valor.


            dynamic person = DynamicXml.Parse(objetoSerializado);
            Assert.AreEqual("Agustin", person.Nombre);//es mas legible poner person.Nombre. y es mejor, si no te toca hacer la clase DynamicXml.
        }

       [TestMethod]
        public void DynamicTest()
        {//permite poner metodos y propiedades y tipos aunque no existan, a la hora del runtime es cuando verifica que existan.
            dynamic data = "hola";            //el tipo original hola, lo compila hasta run time.
            //System.Diagnostics.Debug.Print(data.ToString());
            data = (double)data.Length;//cualquier tipo se puede convertir a dynamic.length es int y luego se convierte a double y data guarda double.se puede cambiar de typo, data era string y ahora es double.
            data = data * 3.5 + 28.6;//el resultado de cualquier operacion es de typo dinamic.
            if (data == 42.6)
            {
                Assert.AreEqual(42.6, data);
            }
            else 
            {
                data.EsteMetodoNoExiste();//este metodo no existe y asi corre el programa.lo compila hasta que aqui llega el thred.
            }

            Assert.IsNull(default(dynamic));//su valor por default es null, lo que comprueba que es un valor por referencia. de lo contrario seria 0.

            int usoDeExtensionMethod = "Hola mundo".ObtenerLongitud();//extension method que no funciona en dynamic, hola mundo pasa como parametro al metodo que existe en otra clase. a hola mundo se le aplica un metodo extension. 
            
        }

    }

    /// <summary>
    /// Extension Method example.
    /// </summary>
    public static class StringExtensions//su clase debe ser estatica.
    {
        public static int ObtenerLongitud(this string myString)//el metodo tambien debe ser estatico, se utiliza this
        {
            return myString.Length;
        }
    }

}
