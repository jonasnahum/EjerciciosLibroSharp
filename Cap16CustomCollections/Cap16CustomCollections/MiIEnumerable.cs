using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap16CustomCollections
{
    public class MiIEnumerable:IEnumerable<string>
    {


        public IEnumerator<string> GetEnumerator()//la instancia que regresa tiene este contrato.IEnumerator<string>.
        {
            MiEnumerator miEnumerator = new MiEnumerator();
            return miEnumerator;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            MiEnumerator miEnumerator = new MiEnumerator();
            return miEnumerator;
        }
    }
    public class MiEnumerator : IEnumerator<string>
    {
        private string[] palabras = new string[] { "uno", "dos", "tres", "cuatro", "cinco" };
        private int pointer = -1;
        public string Current
        {
            get {
                return palabras[pointer];
                }
        }

        public void Dispose()
        {
            palabras = null;//esto hace cuanddo pase el garbage collector.
        }

        object System.Collections.IEnumerator.Current
        {
            get {
                return Current;
                }
        }

        public bool MoveNext()
        {
            if (pointer < palabras.Length - 1)
            {
                pointer++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            pointer = -1;
        }
    }
}
