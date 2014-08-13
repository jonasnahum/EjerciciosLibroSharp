using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cap17ReflecAtribDynamic
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]//attribute usage, indinca donde se va aplicar el atributo, property, clase, etc.
    public class PrintableAttribute:Attribute//por convencion termina diciendo Attribute y hereda de Attribute.
    {
        #region EsteParteEsColapsable//una parte que se puede colapsar en el recuadro con el signo -.
        private string Prefix { get; set; }
        public PrintableAttribute(string prefix = "Propiedad")//un constructor que recibe un prefijo, y si no lo recibe, por default tiene asignado el string "Propiedad".
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
