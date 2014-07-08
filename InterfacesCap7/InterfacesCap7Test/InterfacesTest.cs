using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterfacesCap7;

namespace InterfacesCap7Test
{
    [TestClass]
    public class InterfacesTest
    {
        [TestMethod]
        public void ImplicitMemberImplementationTest()
        {
            Camioneta aguaDelParque = new Camioneta("blanco", "avanzar");
            Assert.IsTrue(aguaDelParque is IVehiculo);
            Assert.AreEqual("blanco", aguaDelParque.Color);
            Assert.AreEqual("avanzar", aguaDelParque.Avanzar());
            IVehiculo vehiculo = aguaDelParque;
            Assert.IsNotNull(vehiculo);
        }
        [TestMethod]
        public void ExplicitMemberImplementationTest()
        {
            Tsuru taxi = new Tsuru("blanco", "avanzar");
            Assert.IsTrue(taxi is IVehiculo);
            Assert.AreEqual("blanco", ((IVehiculo)taxi).Color);//es lo mismo que el metodo anterior, pero como esto es explicito, se ti4ene que convertir antes de ser usado.
            Assert.AreEqual("avanzar", ((IVehiculo)taxi).Avanzar());
            IVehiculo vehiculo = taxi;
            Assert.IsNotNull(taxi);


        }
        [TestMethod]
        public void PolymorphismTest()
        {
            IPersona[] personas = new IPersona[2];
            personas[0] = new Nino();
            personas[1] = new Adulto();
            foreach (IPersona i in personas)
            {
                i.Trabajar();
                i.Comer();
                Assert.IsTrue(i is Nino || i is Adulto);

                Nino jonas = i as Nino;
                if (jonas != null)
                {
                    Assert.IsNotNull(jonas);
                    jonas.IrAlaEscuela();

                }
                else 
                { 
                    Assert.IsNull(jonas);
                }
                    
            }
        }
        [TestMethod]//convertir entre la clase que implementa y sus intrfaces.
        public void ConvertingClassToInterfaceTest()
        {
            PastorAleman tobi = new PastorAleman();
            Persona persona = new Persona();

            IPerro mascota = tobi as IPerro;// a tobi conviertelo en un Iperro, porque pastor aleman hereda de Iperro. esto es lo mismo que abajo.se hace cast, explicito.
            IPerro mascota1 = (IPerro)tobi;//convertir tobi a un tipo I perro.conversion cast.explicito
            IPerro mascta2 = tobi;//se puede hacer esto, porque tobi, hereda Iperro.//conversion implicita
            IHumano humano = persona;//conversion implicita
            INombre nombrado = persona;//conversion impllicita.porque el objeto persona de la clase Persona, hereda directamente de la interface.

            Assert.IsNotNull(mascota);
            Assert.IsNotNull(mascota1);
            Assert.IsNotNull(mascta2);
            Assert.IsNotNull(humano);
            Assert.IsNotNull(nombrado);

            IMamifero animal=tobi;//pastor aleman, hereda de imamifero.
            IMamifero DonFer = persona;
            Assert.IsNotNull(animal);
            Assert.IsNotNull(DonFer);

            Persona individuo = DonFer as Persona;//convertir de interface a oobjeto SIEMPRE NECESITA UN CAST EXPPLICITO, AL CONTRARIO NO.
            Persona sujeto = (Persona)DonFer;//esto es lo mismo que arriba, para convertir donfer que hasta hora es IMamifero a classe Persona, se debe estar seguro que DonFer es de hecho una persona, que implementa una intrfase, Imamifero e Ipersona.
            Assert.IsNotNull(individuo);
            Assert.IsNotNull(sujeto);


            Persona cliente = animal as Persona;//si no puede haces la conversion, el operador as regresa null.en este parrafo se convierte de interfaces a clasess
            //Persona cliente1 = (Persona)animal; //Esta linea marca error.
            Assert.IsNull(cliente);
            PastorAleman solovino = (PastorAleman)animal;
            Assert.IsNotNull(solovino);

        }
        [TestMethod]
        public void ExtensionMethodsOnInterfacesTest()
        {
            Celular MotoG = new Celular();
            decimal resultado= MotoG.Multiplicar(5, 5);
            Assert.AreEqual(25, resultado);

        }
    }
   
   
}
