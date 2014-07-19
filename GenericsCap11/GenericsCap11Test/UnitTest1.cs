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
            ArrayGenerico<int> arrayEnteros = new ArrayGenerico<int>(1);//le mandamos un T a toda la clase, un tipo.
            arrayEnteros.AgregarElemento(5, 0);// 5 es un elemento del array y 0 es la posicion indice donde se va guardar.

            ArrayGenerico<string> arrayStrings = new ArrayGenerico<string>(2);
            arrayStrings.AgregarElemento("jonas", 1);

            Assert.AreEqual(5, arrayEnteros.Lista[0]);
            Assert.AreEqual("jonas", arrayStrings.Lista[1]);

            int[] input = new int[] { 5, 4, 8 };
            int[] output = new int[] { 8, 4, 5 };
            int[] outputPrueba = FuncionesArray.InvertirOrden(input);//el metodo en la clase solo recibe un array y lo regresa del mismo tipo pero invertido.

            AssertValoresIguales(output, outputPrueba);

            char[] textoEnviar= "Jonas".ToCharArray();
            char[] loQueEspero = new char[] { 's', 'a', 'n', 'o', 'J' };
            char[] result = FuncionesArray.InvertirOrden(textoEnviar);
            AssertValoresIguales(loQueEspero, result);

        }
        public void AssertValoresIguales<T>(T[] expected, T[] actual)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void ConstraintsTest()
        {
            FabricaDeCarros fabrica = new FabricaDeCarros();
            Tsuru tsuru = fabrica.ConstruirCarro<Tsuru>("rojo", 4);//tsuru es solo una variable de tipo Tsuru, Construir Carro ers un metodo que recibe cualquier tipo que herede de AutoBase, en este caso, enviamos al metodo construir carro un tipo tsuru, y dicho metodo crea una instacia de tipo tsuru.
            Camioneta camioneta = fabrica.ConstruirCarro<Camioneta>("verde", 2);//regresa a la varible una instancia de tipo Camioneta.
            Assert.IsTrue(tsuru is Tsuru);
            Assert.IsTrue(camioneta is Camioneta);
            //Camion camion = fabrica.ConstruirCarro<Camion>("Ga;eama", 4); no es posible, porque no cumple con el Constraint de new(), tener un constructor vacio.
            // Moto moto = fabrica.ConstruirCarro<Moto>("negra", 0); marca error, porque Moto no hereda de Autobase como indica el Constraint.
        }

    }
}
