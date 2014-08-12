using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cap17ReflecAtribDynamic
{
    public class TypeDescriptor
    {
        public TypeDescriptor(Type tipo)
        {
            Type = tipo;
        }
    
        public Type Type { get; set; }

        public static void Print<T>(IEnumerable<T> lista, string titulo)
        {
            System.Diagnostics.Debug.Print("----------------------{0}---------------------------", titulo);
            foreach (T item in lista)
            {
                System.Diagnostics.Debug.Print(item.ToString());
            }
        }

        private IEnumerable<string> GetPropertyInfo() 
        {
            PropertyInfo[] propiedades= Type.GetProperties();
            foreach (PropertyInfo item in propiedades)
            {
                string tieneGet = item.CanRead ? "Tiene Getter" : string.Empty;//si se puede leer imprimir Tiene Getter, de lo contrario, el string queda vacio. el signo ? es como un if.
                string tieneSet = item.CanWrite ? "Tiene Setter" : string.Empty;
                string tipo = item.PropertyType.Name;
                string name = item.Name;
                yield return string.Format("{0},{1},{2},{3},{4}","propiedad",name,tipo,tieneGet,tieneSet);
       
            }
        }
        private IEnumerable<string> GetMethodInfo() 
        {
            MethodInfo[] metodos = Type.GetMethods();//se sacan los metodos y se meten en un array.
            foreach (MethodInfo item in metodos)
            {
                string regresa = item.ReturnType.Name;
                IEnumerable<string> parametrosList = from p in item.GetParameters() select string.Format("{0} {1}",p.ParameterType.Name,p.Name);//consesguir los parametros de cada item , imprimirlos y guardarlos en la variable.
                string parametros = string.Join(",", parametrosList.ToArray());
                string name=item.Name;
                yield return string.Format("{0} {1} {2}", "metodo", name, regresa, parametros);//retorna el nombre ,el typo y los parametros.
                
            }
        }
        public static void GetAttributeInfo<T>(T objeto)
        {
            
            PropertyInfo[] properties = objeto.GetType().GetProperties();
     
            foreach (PropertyInfo item in properties)
            {
                PrintableAttribute[] attributes = (PrintableAttribute[])item.GetCustomAttributes(typeof(PrintableAttribute), false);
                foreach (PrintableAttribute attribute in attributes)
                {
                    string value = item.GetValue(objeto, null).ToString();
                    attribute.Print(value);
                }
            }
        }
        private IEnumerable<string> GetAssemblyInfo()
        {
            yield return string.Format("{0}", Type.Assembly.FullName);//trabaja sobre el Type Alumno.
            yield return string.Format("{0}", Type.FullName);
        }

        /// <summary>
        /// Imprime informacion acerca del tipo actual.
        /// </summary>
        public void Print() 
        {
            Print(GetAssemblyInfo(), "Assembly Name");//el metodo print se tiene que llamar desde adentro de otro metod.
            Print(GetPropertyInfo(), "Propiedades");
            Print(GetMethodInfo(), "Metodos");
        }
    }

}
