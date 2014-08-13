using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cap17ReflecAtribDynamic
{
    public class Alumno
    {
        public Alumno() { }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Apellido { get; set; }
        
        public void DecirNombre() 
        {
            System.Diagnostics.Debug.Print(Nombre);
        }
        public Alumno(string nombre)
        {
            Nombre=nombre;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Nombre, Apellido, Edad);
        }
    }
    public class Profesor
    {
        public Profesor() { }
        [PrintableAttribute("Nombre: ")]//un atributo , en la clase PrintableAttribute, se recibe el parametro "Nomnbre:" y se guarda como prefijo en una propiedad.
        public string Nombre { get; set; }
        [Printable("Edad: ")]//Printable significa PrintableAttribute, infiere el nombre completo por la convencion de terminar en Attribute.
        public int Edad { get; set; }
        [Printable()]
        public string Apellido { get; set; }


        public void DecirNombre()
        {
            System.Diagnostics.Debug.Print(Nombre);
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Nombre, Apellido, Edad);
        }
    }
    public class CreadorDeInstancias
    {
        public static IEnumerable<string> ClassesEnAssembly(Type t) 
        {
            Assembly a = Assembly.GetAssembly(t);
            foreach (Type item in a.GetTypes())
            {
                yield return item.Name;
            }
        }

        public static T CrearInstancia<T>(Dictionary<string, string> propiedades)//metodo recibe un type diccionario generico.
        {//las instancias tienen informacion del tipo, los metodos y las propiedades....

            Type metadatos = typeof(T);//Type tiene los metadatos de toda la clase.
                                                                // new T();
            T instance =  (T)Activator.CreateInstance(metadatos);//crear una instancia de metadatos.el activador es para crear una instancia, como si llamara el new o el constructor.

            foreach (KeyValuePair<string, string> p in propiedades) 
            {
                int valorEntero = 0;//aqui se guarda el entero si se puede hacer la conversion de abajo.
                bool esEntero = int.TryParse(p.Value, out valorEntero);//es Entero marca true si se puede hacer la conversion.
                
                PropertyInfo propertyInfo = metadatos.GetProperty(p.Key);//Property Info tiene los metadatos de una propiedad. es un metadato pero de una propiedad, que este caso es key.

                if (esEntero)
                {
                    propertyInfo.SetValue(instance, valorEntero);// equivale a asignar una valor. instancia.Property = value;
                }
                else
                {
                    propertyInfo.SetValue(instance, p.Value);
                }
            }

            MethodInfo metadatoMetodo = metadatos.GetMethod("ToString");//method info tiene los metadatos de un metodo dado.
            object resultadoLammarMetodo = metadatoMetodo.Invoke(instance, null); // instancia.ToString();

            System.Diagnostics.Debug.Print(resultadoLammarMetodo.ToString());
            
            return instance;
        }
    }
}
