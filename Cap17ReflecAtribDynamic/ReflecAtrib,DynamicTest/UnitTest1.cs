using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap17ReflecAtribDynamic;
using System.Collections.Generic;

namespace ReflecAtrib_DynamicTest
{
    [TestClass]
    public class UnitTest1
    {//Reflection. conjunto de clases que permiten obtener informacion de codigo y generar codigo.
        [TestMethod]
        public void TestMethod1()
        {//type of saca los metadatos de un typo y los guarda en una variable de tipo Type.regresa un objeto tipo Type.
            Type typeAlumno = typeof(Alumno);//oopera sobre el tipo en este caso Alumno.Type guarda metadatos respecto de un tipo, en este caso , de Alumno.
            TypeDescriptor typeDescriptor = new TypeDescriptor(typeAlumno);
            
            typeDescriptor.Print();

            //Alumno alumno = new Alumno("pancho");
            //Type datosdeAlumno = alumno.GetType();//este opera sobre una instancia.

            //Type decimalT = typeof(decimal);
            //typeDescriptor.Type = decimalT;
            //typeDescriptor.Print();
        }

        [TestMethod]
        public void ActivatorTest()
        { //activator ayuda a crear instancias.
            Dictionary<string,string> propiedades=new Dictionary<string,string>();
            propiedades.Add("Nombre","Juan");
            propiedades.Add("Edad","15");
            propiedades.Add("Apellido","Escutia");

            Alumno instanciaCreada=CreadorDeInstancias.CrearInstancia<Alumno>(propiedades);
            Profesor profe = CreadorDeInstancias.CrearInstancia<Profesor>(propiedades);


            TypeDescriptor.Print(CreadorDeInstancias.ClassesEnAssembly(typeof(Alumno)), "Clases en assembly");
            
        }
    }
}
