using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cap17ReflecAtribDynamic
{
    public class DynamicXml : DynamicObject
    {
        private XElement Element { get; set; }
        public DynamicXml(XElement element)//este es el constructor.
        {
            Element = element;
        }
        public static DynamicXml Parse(string text)
        {
            return new DynamicXml(XElement.Parse(text));
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)//esto es equivalente al Get de una propiedad y binder es la propiedad.
        {
            bool success = false;
            result = null;
            XElement firstDescendant =//que busque hacia abajo.
            Element.Descendants(binder.Name).FirstOrDefault();
            if (firstDescendant != null)
            {
                if (firstDescendant.Descendants().Count() > 0)
                {
                    result = new DynamicXml(firstDescendant);
                }
                else
                {
                    result = firstDescendant.Value;
                }
                success = true;
            }
            return success;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)//binder es la propiedad y value, el valor de esa propiedad,  trysetmember es el equivalente al set sobre una propiedad dinamica.
        {
            bool success = false;
            XElement firstDescendant =
            Element.Descendants(binder.Name).FirstOrDefault();
            if (firstDescendant != null)
            {
                if (value.GetType() == typeof(XElement))
                {
                    firstDescendant.ReplaceWith(value);
                }
                else
                {
                    firstDescendant.Value = value.ToString();
                }
                success = true;
            }
            return success;

        }
    }
}