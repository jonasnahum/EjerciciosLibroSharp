using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Cap12Delegados
{
    public class ExpressionTreeBuilder
    {
        /// <summary>
        /// Metodo que regresa un expresion tree que contine una funcion para sumar dos enteros.        
        /// </summary>
        /// <returns></returns>
        public Expression<Func<int, int, int>> SumarEnteros()
        {
            //Func<int, int, int> sumaEnteros = (a, b) => a + b;
            //Lo que queremos lograr es (int a, int b) => a + b;

            ParameterExpression variableA = Expression.Parameter(typeof(int), "a");
            ParameterExpression variableB = Expression.Parameter(typeof(int), "b");
            BinaryExpression sumarAyB = Expression.Add(variableA, variableB);

            //se construlle el expressionTree que equivale a (int a, int b) => a + b
            Expression<Func<int, int, int>> expressionTree =
                Expression.Lambda<Func<int, int, int>>(sumarAyB, new ParameterExpression[] { variableA, variableB });

            return expressionTree;
        }

        //que divida dos numeros, reciba dos decimal y regrese un decimal.
        public Expression<Func<decimal, decimal, decimal>> DividirNumeros()
        {
            //func<int,int,decimal>DividirNumeros=(a,b)=>a/b;
            //lo que queremos lograr es (int a, intb)=> a/b;

            ParameterExpression variableA = Expression.Parameter(typeof(decimal), "a");
            ParameterExpression variableB = Expression.Parameter(typeof(decimal), "b");
            BinaryExpression dividirAyB = Expression.Divide(variableA, variableB);
            
            //(int a, intb)=>a/b

            Expression<Func<decimal, decimal, decimal>> expressionArbol=
                Expression.Lambda<Func<decimal, decimal, decimal>>(dividirAyB, new ParameterExpression[] { variableA, variableB });
            return expressionArbol;
 
        }
    }
}
