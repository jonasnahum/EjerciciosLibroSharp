using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsCap11
{
    public class ArrayGenerico<T>//esta T es el mismo tipo para toda la clase.
    {
        public T[] Lista { get; set; } 
        public ArrayGenerico(int longitud)
        {
            Lista = new T[longitud];
        }

        public void AgregarElemento(T elemento, int indice)
        {
            Lista[indice] = elemento;
        }

        public void AgregarElemento<U>(U[] array, U elemento, int indice)//U entre<>, es otro tipo diferente indeterminado, en los parametros recibe un array de tipo U, y un elemento tipo U, y un Indice.
        {
            array[indice] = elemento;
        } 
        
    }

    public class FuncionesArray
    {
        public static Z[] InvertirOrden<Z>(Z[] arrayInput)// el primer Z[] es el valor de retorno, el <Z> es realmente donde se pasa el tipo que es copiado a retorno e intput, y el ultimo Z se refiere a lo que se va a recibir como parametro.
        {
            Z[] arrayOutput = new Z[arrayInput.Length];
            for (int i = 0; i < arrayInput.Length; i++)
            {
                arrayOutput[(arrayInput.Length - 1) - i] = arrayInput[i];//invierte las posiciones.            
            }
            return arrayOutput;
        }
    }
}
