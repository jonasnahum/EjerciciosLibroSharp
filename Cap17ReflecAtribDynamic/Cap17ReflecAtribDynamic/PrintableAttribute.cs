using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cap17ReflecAtribDynamic
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]//attribute usage, indinca donde se va aplicar el atributo, property, clase, etc.
    public class PrintableAttribute:Attribute
    {
        #region EsteParteEsColapsable
        private string Prefix { get; set; }
        public PrintableAttribute(string prefix = "Propiedad")
        {
            Prefix = prefix;
        }
        [Obsolete("Por favor ya no usen este metodo porque consume mucha memoria")]
        public void Print(string value) 
        {
            System.Diagnostics.Debug.Print("{0} {1}", Prefix, value);
        }
        #endregion
    }
}
