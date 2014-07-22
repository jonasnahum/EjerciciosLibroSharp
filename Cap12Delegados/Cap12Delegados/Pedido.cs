using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap12Delegados
{
    public class Pedido
    {
        public delegate decimal ObtenerPrecio(Pedido p);//delegate, valor de retorno, nombre del metodo y parametros.
        public int Garrafones { get; set; }
        public decimal CalcularTotal(ObtenerPrecio metodo)//se esta implementando la firma de Obtener precio, metodo que regresa un decimal y recibe un pedido. se esta mandando llamar un metodo con las caracteristicas del delegado.// aqui es una variable de tipo delegado, que guarda un metodo.
        {
            return metodo(this);//parentesis ehecutan el metodo que se recibe como parametro, y this, es el parametro del mettodo CalcularPrecio.
        }                       //this hace referencia a un objeto, a una instancia de clase Pedido, en este caso ped, la cual tiene el no. de Garrafones para que CalcularPrecio trabaje.
        
    }
    public class CalculadoraDePrecios
    {
        public CalculadoraDePrecios(decimal precioUnitario, decimal iva)
        {
            this.PrecioUnitario = precioUnitario;
            this.Iva = iva;
        }
        public decimal PrecioUnitario { get; set; }
        public decimal Iva { get; set; }
        public decimal CalcularPrecio(Pedido pedido)//este metodo se pasa como parametro a Pedido.CalcularTotal.//(this) es un pedido, su objeto ped es de tipo Pedido y ya con esto se cumple lo necesario en el delegado para poder ser enviado como parametro al metodo CalcularTotal. y a la vez como una variable de tipo delegado.
        {
            decimal total = pedido.Garrafones * PrecioUnitario;
            total = total * Iva;
            return total;
        }
    }
}
