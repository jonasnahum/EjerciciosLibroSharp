using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesCap7
{
    public interface ICalculadora
    {
        decimal Sumar(decimal a,decimal b);
        decimal Restar(decimal a, decimal b);

    }
    public class Celular : ICalculadora
    {

        public decimal Sumar(decimal a, decimal b)
        {
            return a + b;
        }

        public decimal Restar(decimal a, decimal b)
        {
            return a - b;
        }
    }
    public static class MathHelper 
    {
        public static decimal Multiplicar(this ICalculadora calculadora, decimal a, decimal b)//los extension methods usan this dentro de los parametros.la clase y el metodo son estaticos. con ICalculadora, lo que se hace es ponerle este metodo a todas las clases de tipo ICalculadora.
        {
            return a * b;
        }
    }
}
