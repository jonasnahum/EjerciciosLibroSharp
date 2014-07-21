using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericsCap11;

namespace GenericsCap11Test
{
    [TestClass]
    public class UnitTest1
    {//constraint son como un parametro, pero en vez de pasarle un valor le pasas un tipo.
        [TestMethod]
        public void GenericsTest()
        {
            ArrayGenerico<int> arrayEnteros = new ArrayGenerico<int>(1);//le mandamos un T, en este caso es un int a toda la clase, un tipo, la clase lo recibe con la palabra <T>.Entre parentesis se manda como parametro un numero al constructor, que sera la variable longitud.
            arrayEnteros.AgregarElemento(5, 0);//arrayEnteros es un objeto. 5 es un elemento del array y 0 es la posicion indice donde se va guardar.

            ArrayGenerico<string> arrayStrings = new ArrayGenerico<string>(2);//se crea un objeto, se manda tipo a la clase y parametro al constructor.la longitud es de 2, osea que se almacena espacio para [0] y [1].
            arrayStrings.AgregarElemento("jonas", 1);//se manda llamar metodo del objeto., se manda el tipo string y el indice del array.

            Assert.AreEqual(5, arrayEnteros.Lista[0]);
            Assert.AreEqual("jonas", arrayStrings.Lista[1]);//expected es jonas, osea lo que espero y actual, el resultado del metodo.

            int[] input = new int[] { 5, 4, 8 };//se crea un array de enteros llamado input.
            int[] output = new int[] { 8, 4, 5 };//un array de enteros.
            int[] outputPrueba = FuncionesArray.InvertirOrden(input);//el metodo en la clase solo recibe un array y lo regresa del mismo tipo pero invertido.

            AssertValoresIguales(output, outputPrueba);//espected es output, actual es el resultado del metodo.

            char[] textoEnviar= "Jonas".ToCharArray();//se convierte el string a char y se envia a un array de char.
            char[] loQueEspero = new char[] { 's', 'a', 'n', 'o', 'J' };
            char[] result = FuncionesArray.InvertirOrden(textoEnviar);
            AssertValoresIguales(loQueEspero, result);

        }
        public void AssertValoresIguales<T>(T[] expected, T[] actual)//un metodo para hacer el assert en los array. sera de tipo <T>, y recibira como carametro dos arrays.
        {
            for (int i = 0; i < expected.Length; i++)//que itere tanto como la longitud del array que se expera.
            {
                Assert.AreEqual(expected[i], actual[i]);// lo que espero, en cada uno de sus indices comparado con el resultado del metodo en cada uno de sus indices.
            }
        }
        [TestMethod]
        public void ConstraintsTest()
        {
            FabricaDeCarros fabrica = new FabricaDeCarros();
            Tsuru tsuru = fabrica.ConstruirCarro<Tsuru>("rojo", 4);//tsuru es solo una variable de tipo Tsuru, Construir Carro es un metodo que recibe cualquier tipo que herede de AutoBase, en este caso, enviamos al metodo construir carro un tipo tsuru, y dicho metodo crea una instacia de tipo tsuru y la guarda en la variable.
            Camioneta camioneta = fabrica.ConstruirCarro<Camioneta>("verde", 2);//regresa a la varible una instancia de tipo Camioneta.
            Assert.IsTrue(tsuru is Tsuru);
            Assert.IsTrue(camioneta is Camioneta);
            //Camion camion = fabrica.ConstruirCarro<Camion>("Ga;eama", 4); no es posible, porque no cumple con el Constraint de new(), tener un constructor vacio.
            // Moto moto = fabrica.ConstruirCarro<Moto>("negra", 0); marca error, porque Moto no hereda de Autobase como indica el Constraint.
        }

    }
}
