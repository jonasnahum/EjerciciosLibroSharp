using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap12Delegados;
using System.Linq.Expressions;

namespace DelegadosCap12Test
{//primero se declara el delegado, luego la variable de tipo delegado que guarda un metodo, y finalmente la variable de tipo delegado que hace la implementacion.
    [TestClass]
    public class UnitTest1
    {
        public decimal PrecioPaLosCompas(Pedido pedido)//esta es una variable de tipo delegado.
        {
            return pedido.Garrafones * 9;//aqui se ejecuta el pedido, que en este caso es Nahum.
        }
        [TestMethod]
        public void TestMethod1()
        {
            Pedido ped = new Pedido();//la clase Pedido tiene la property Int Garrafones.
            ped.Garrafones = 10;//se asigna valor.
            CalculadoraDePrecios cdp = new CalculadoraDePrecios(10M, 1.16M);//se le manda precio unitario e iva.
            decimal total = ped.CalcularTotal(cdp.CalcularPrecio);//variable de tipo delegado que guarda un metodo//CalcularTotal es un metodo que recibe como parametros otro metodo que cumple con la firma del delegado, que regrese un decimal y reciba un pedido.//CalcularPrecio es como una variable cuando no se le ponen los parentesis.
            Assert.AreEqual(116, total);


            Pedido Nahum = new Pedido();
            Nahum.Garrafones = 5;//se le asigna numero de Garrafones.
            decimal total2 = Nahum.CalcularTotal(PrecioPaLosCompas);//variable de tipo delegado que guarda un metodo.
            Assert.AreEqual(45, total2);
        }
        public void ImprimirEnConsola(string a)
        {
            System.Diagnostics.Debug.Print(a);
        }
        [TestMethod]
        public void EjemplosDelegados()
        {
            int Z = 1;
            DelegadosEjemplo ejemplo = new DelegadosEjemplo();
            ejemplo.M1 = delegate(int x, int y) //variable de tipo delegado//se pasa el metodo que va a guardar M1// delegate suple al nombre del metodo, este es un delegado anonimo, no tiene nombre, y hace su implementacion.
            {
                return (x + y) - Z;
            };
            ejemplo.M2 = delegate(string a, string b)//variable de tipo delegado que se guarda en M2//aqui es un metodo declarado inline, no se mandan valores, adem[as es un metodo anonimo.cumple con el delegado de M2.
            {
                return a + b;
            };//lleva ; porque es una sola linea toda la implementacion.
            ejemplo.M3 = ImprimirEnConsola;//variable de tipo delegado que se guarda en M3.
            Z = 0;//variales capture,..

            int resultadoSuma = ejemplo.M1(10, 12);//se pasan los valores a la variable de tipo delegado que a su vez, se va a guardar en M1.
            string resultString = ejemplo.M2("jonas", "nahum");//se pasan los valores a la variable de tipo delegado que se va a guardar en M2.
            ejemplo.M3("Hola");

            Assert.AreEqual(22, resultadoSuma);
            Assert.AreEqual("jonasnahum", resultString);
        }
        [TestMethod]
        public void EjemplosDelegadosSystemDefined()
        {//las variables de tipo delegado son la implementacion del metodo que cumple con la firma.
            int Z = 1;//esta variable puede ser llamada dentro de un metodo, pero no alrevez, mas adelante, cambia su valor, y por lo tanto, no afecta el metodo en donde se usa.
            DelegadosEjemploSystemDefined ejemplo = new DelegadosEjemploSystemDefined();//un objeto de la clase.
            ejemplo.M1 = delegate(int x, int y) //estos son los parametros.//variable de tipo delegado//se pasa el metodo que va a guardar M1// delegate suple al nombre del metodo, este es un delegado anonimo, no tiene nombre, y hace su implementacion.
            {
                return (x + y) - Z;//este es el cuerpo del metodo
            };
            ejemplo.M2 = delegate(string a, string b)//variable de tipo delegado que se guarda en M2//aqui es un metodo declarado inline, adem[as es un metodo anonimo.cumple con el delegado de M2.
            {
                return a + b;
            };//lleva ; porque es una sola linea toda la implementacion.
            ejemplo.M3 = ImprimirEnConsola;//variable de tipo delegado que se guarda en M3.sin parentesis porque es variable.
            Z = 0;//variales capture,..

            int resultadoSuma = ejemplo.M1(10, 12);//se pasan los valores a la variable de tipo delegado que a su vez, se va a guardar en M1.
            string resultString = ejemplo.M2("jonas", "nahum");//se pasan los valores a la variable de tipo delegado que se va a guardar en M2.
            ejemplo.M3("Hola");

            Assert.AreEqual(22, resultadoSuma);
            Assert.AreEqual("jonasnahum", resultString);
        }
        [TestMethod]
        public void EjemplosDelegadosLambda()
        {

            DelegadosEjemploSystemDefined ejemplo = new DelegadosEjemploSystemDefined();//se hace un objeto de la clase.
            ejemplo.M1 = (int primer, int seg) => primer + seg;//lambda entre parentesis, los parametros, la flechita equivale al return, despues esta el cuerpo del metodo.//una variable que se guarda en M1 y este a su vez es la firma.

            ejemplo.M2 = (string uno, string dos) => uno + dos;
            ejemplo.M3 = x => ImprimirEnConsola(x);//cuando es un solo parametro se pueden omitir los parentesis y el tipo de dato, en este caso, el cuerpo del metodo es un metodo que cumple con la firma.

            int resultadoSuma = ejemplo.M1(10, 12);//se pasan los valores a la variable de tipo delegado que a su vez, se va a guardar en M1.
            string resultString = ejemplo.M2("jonas", "nahum");//se pasan los valores a la variable de tipo delegado que se va a guardar en M2.
            ejemplo.M3("Hola");

            Assert.AreEqual(22, resultadoSuma);
            Assert.AreEqual("jonasnahum", resultString);
        }
        [TestMethod]
        public void EjemplosDelegadosLambda2()
        {                       //delegados firma que guarda el metodo, func y action son delegados y son firmas y a la vez son un tipo de dato que guarda el metodo, son maneras breves de expresar un delegado.
            Func<string, string> ConvertirAminusculas;//esta es la variable que va guardar un metodo.//func tienen parametros y valor de retorno.
            ConvertirAminusculas = x => x.ToLower();//ConvertirAminusculas es una variable, que va guardar este metodo en forma de Lambda.
            string espero = "hola";
            Assert.AreEqual(espero, ConvertirAminusculas("HOLA"));

            Action<string> ImprimirAconsola;//Action no regresa nada.
            ImprimirAconsola = x => System.Diagnostics.Debug.Print(x);
            ImprimirAconsola("hola jonas");

            Func<string> ObtenerHoraActual;
            ObtenerHoraActual = () => DateTime.Now.ToString();//cuando no recibe nada, ningun parametro, los parentesis si son obligatorios.
            System.Diagnostics.Debug.Print(ObtenerHoraActual());

            Func<int, int, decimal> Dividir;
            Dividir = (int arriba, int numerando) => arriba / numerando;
            decimal result = Dividir(10, 2);
            decimal resultadoEsperado = 5;
            Assert.AreEqual(resultadoEsperado, result);

            Func<double, double, double, double> PromediarTresCalificaciones;
            PromediarTresCalificaciones = (double esp, double mate, double hist) =>
            {
                double sumarcalif;
                sumarcalif = esp + mate + hist;
                return sumarcalif / 3;
            };

            Func<int, int, int> SumaDeDosEnteros;
            SumaDeDosEnteros = (a, b) => a + b;//no se le pone los tipos de parametros, los infiere del func.
            Assert.AreEqual(5, SumaDeDosEnteros(3, 2));

            //delegado, es la variable que guarda un metodo.
            EscribirNombreCompleto NombreCombleto;
            NombreCombleto = (string nombre, string apellido) => string.Format("{0} {1}", nombre, apellido);//lambda.
            string variableMethodo = NombreCombleto("jonas", "nahum");
            string LoqueEspero = "jonas nahum";
            Assert.AreEqual(LoqueEspero, variableMethodo);

            EscribirNombreCompleto NombreCombleto1;
            NombreCombleto1 = delegate(string nombre, string apellido)//delegado anonimo.
            {
                return string.Format("{0} {1}", nombre, apellido);
            };

            EscribirNombreCompleto NombreCompleto2 = NombreCompletoCualquierNombre;//un metodo.
            string resultadomet = NombreCompleto2("jonas", "jimenez");
            string miRespuesta = "jonas jimenez";
            Assert.AreEqual(resultadomet, miRespuesta);


        }
        public string NombreCompletoCualquierNombre(string nombre, string apellido)
        {
            return string.Format("{0} {1}", nombre, apellido);
        }
        delegate string EscribirNombreCompleto(string nombre, string apellido);//definicion de firma.

        [TestMethod]
        public void ExpressionTreeTest()
        {
            ExpressionTreeBuilder builder = new ExpressionTreeBuilder();
            Expression<Func<int, int, int>> expresionTree = builder.SumarEnteros();

            Func<int, int, int> metodo = expresionTree.Compile();

            int resultado=metodo(3, 3);
            int yoEspero = 6;
            Assert.AreEqual(yoEspero, resultado);

            Expression<Func<decimal, decimal, decimal>> expresionArbol = builder.DividirNumeros();//el metodo dividir numeros se va a guardar en expresion arbol, de tipo    Expression<Func<int, int, decimal>>.
            Func<decimal, decimal, decimal> metodo2 = expresionArbol.Compile();

            decimal result = metodo2(5, 2);
            decimal espero = 2.5M;
            Assert.AreEqual(espero, result);
        }
    }//funtion, action y delegate son lo mismo y lambda,metodo,y delegado anonimo son lo mismo.

}
