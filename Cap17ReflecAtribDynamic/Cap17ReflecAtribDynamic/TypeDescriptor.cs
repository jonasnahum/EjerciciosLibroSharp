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
                string tieneGet = item.CanRead ? "Tiene Getter" : string.Empty;
                string tieneSet = item.CanWrite ? "Tiene Setter" : string.Empty;
                string tipo = item.PropertyType.Name;
                string name = item.Name;
                yield return string.Format("{0},{1},{2},{3},{4}","propiedad",name,tipo,tieneGet,tieneSet);
       
            }
        }
        private IEnumerable<string> GetMethodInfo() 
        {
            MethodInfo[] metodos = Type.GetMethods();
            foreach (MethodInfo item in metodos)
            {
                string regresa = item.ReturnType.Name;
                IEnumerable<string> parametrosList = from p in item.GetParameters() select string.Format("{0} {1}",p.ParameterType.Name,p.Name);
                string parametros = string.Join(",", parametrosList.ToArray());
                string name=item.Name;
                yield return string.Format("{0} {1} {2}", "metodo", name, regresa, parametros);
                
            }
        }
        private IEnumerable<string> GetAssemblyInfo()
        {
            yield return string.Format("{0}", Type.Assembly.FullName);
            yield return string.Format("{0}", Type.FullName);
        }

        /// <summary>
        /// Imprime informacion acerca del tipo actual.
        /// </summary>
        public void Print() 
        {
            Print(GetAssemblyInfo(), "Assembly Name");
            Print(GetPropertyInfo(), "Propiedades");
            Print(GetMethodInfo(), "Metodos");
        }
    }

}
