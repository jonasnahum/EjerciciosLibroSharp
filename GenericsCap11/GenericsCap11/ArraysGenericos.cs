using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsCap11
{
    public class ArrayGenerico<T>//esta T es el mismo tipo para toda la clase.
    {
        public T[] Lista { get; set; } //una property que guarda un array de tipo T y su longitud se especifica cuando se instancia esta clase.
        public ArrayGenerico(int longitud)//un constructor que recibe un parametro que ser[a laa longitud.
        {
            Lista = new T[longitud];
        }

        public void AgregarElemento(T elemento, int indice)
        {
            Lista[indice] = elemento;//guardar en lista, en el indice tal, el elmento tal,
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
            Z[] arrayOutput = new Z[arrayInput.Length];//se crea un array de salida de la misma longitud que el de entrada.
            for (int i = 0; i < arrayInput.Length; i++)//iterar tantas veces como la longitud imput, en arrayOutput:
            {
                arrayOutput[(arrayInput.Length - 1) - i] = arrayInput[i];//invierte las posiciones.            
                //(arrayInput.Length - 1): en este caso, el lenght es 3, tres palasbras -1, =2, por que el primer indice es 0, luego 1, y 2. se va a trabajar con indices, se van a intercambiar los indices.
                //[(arrayInput.Length - 1) - i]: en la primera iteracion, i vale 0, entonces tenemos que 2-0=2, en la segunda iteracion, (2)-1=indice 1, (2)-2=0,.. en resumen, lo que esta dentro de[] es el indice, el cual sigue esta secuencia:2,1,0.
                //viene siendo como si:arrayOutput[2,1,0] = arrayInput[0,1,2], se invierte el orden de los array.
            }
            return arrayOutput;
        }
    }
}
