using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap10Exceptionhandling;
using System.Diagnostics;

namespace Cap10Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TryCatchTest()
        {
            Calculadora calculadora = new Calculadora();
            calculadora.Dividir(10, 1);
            calculadora.DividirSeguro(10, 2);
        }
        [TestMethod]
        public void EscuelaTest()
        {
            Alumno[] generacion2004=new Alumno[6];//Que abra un lugar en memoria para un array de tipo Alumno llamado generacion 2004 con 6 espacios.
            generacion2004[0] = new Alumno("juan", 10);
            generacion2004[1] = new Alumno("maria", 5);
            generacion2004[2] = new Alumno("pedro", 4);
            generacion2004[3] = new Alumno("pablo", 8);
            generacion2004[4] = new Alumno("lucy", 9);
            generacion2004[5] = new Alumno("sofy", 3);

            Alumno[] generacion2005 = new Alumno[6];// son 3 arrays de tipo alumno.
            generacion2005[0] = new Alumno("juan", 10);
            generacion2005[1] = new Alumno("maria", 5);
            generacion2005[2] = new Alumno("pedro", 4);
            generacion2005[3] = new Alumno("pablo", 8);
            generacion2005[4] = new Alumno("lucy", 9);
        
            Alumno[] generacion2006 = new Alumno[6];//array.
            generacion2006[0] = new Alumno("juan", 10);//se hace un objeto y se asigna en la primera casilla.
            generacion2006[1] = new Alumno("maria", 5);
            generacion2006[2] = new Alumno("pedro", 4);
            generacion2006[3] = new Alumno("pablo", 8);
            generacion2006[4] = new Alumno("lucy", 9);
            generacion2006[5] = new Alumno("sofy", 3);

            Alumno[][] generaciones = new Alumno[][]{ generacion2004, generacion2005, generacion2006 };// dos dimensiones.un array de tipo Alumno, llamado generaciones que contiene 3 generaciones, las dos casillas, no se.
            Escuela escuela = new Escuela();//objeto escuela.

            foreach (Alumno[] generacion in generaciones)//tipo array, singular, plural.
            {
                try//LO QUE POSIBLEMENTE TENGA UNA EXEPCION
                {
                    escuela.AvanzarGrado(generacion);//En la clase escuela, Metodo Avanzar de grado,el cual es de tipo Alumno[] y recibe como parametro un array de tipo Alumno[] que en este caso es generacion.
                }
                catch (CupoLlenoException e)//COMO MANEJARLA DEPENDIENDO DEL TIPO
                {
                    Debug.Print("Avisarle a la mama de Sofy que ya esta el cupo lleno..");
                }
                finally//SIEMPRE SE EJECUTA HAYA O NO EXEPCION.
                {
                    Debug.Print("nuestra escuela Clausura de Generacion");
                }
            }
        }
    }
}
