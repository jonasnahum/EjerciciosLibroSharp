using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap17ReflecAtribDynamic;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

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
        public void NoTestMethodAttribute ()
        {
            Assert.IsTrue(false);

        }
        [TestMethod]
        public void AttributeTest()
        {//para marcar un elemento y o asociarle una funcion extra.
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
            

            string objetoSerializado = Serializar(persona);
            persona = null;
            System.Diagnostics.Debug.Print(objetoSerializado);

            Persona persona2 = DeserializarXml<Persona>(objetoSerializado);
            Assert.AreEqual("Agustin", persona2.Nombre);
        }
    }
}
