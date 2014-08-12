using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cap17ReflecAtribDynamic
{
    [Serializable]
    public class Persona
    {
        [NonSerialized]
        [XmlIgnore]
        public string Password;

        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Apellido { get; set; }
        
     
    }
}
