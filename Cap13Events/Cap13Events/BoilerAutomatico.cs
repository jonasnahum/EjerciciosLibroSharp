﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Cap13Events
// hay la declaracion de un evento, luego el EventHandler y el metodo que cumple con la firma que esta en la declaracion del Evento.los metodos se agregan al EventHandelr con += y un evento es como un delegado pero que guarda varios metodos.
{
    public class ArgumentosCambioDeEstado:EventArgs//hereda de EventArgs como lo hacen los eventos.
    {//recibe un objeto estado y lo guarda.
        public Estado Estado { get; set; }
        public ArgumentosCambioDeEstado(Estado estado)
        {
            Estado = estado;
        }
    }
    public class Quemador
    {
        Timer Timer { get; set; }
        public TanqueDeAgua Tanque { get; set; }

        public Quemador(TanqueDeAgua tanque) 
        {
            Timer = new Timer(100);//instancia de Timer.
            Timer.Enabled = false;
            Timer.Elapsed += Encender;
            Tanque = tanque;
        }

        private void Encender(object sender, ElapsedEventArgs e)
        {
            Tanque.Temperatura += 1;
            System.Diagnostics.Debug.WriteLine("{0}°C ", Tanque.Temperatura);

        }
        public void CalentarAgua() 
        {
            Timer.Enabled = true;
        }
        public void DejarDeCalentar()
        {
            Timer.Enabled = false;  
        }
    }

    public class Termostato 
    {
        //Action<object, ArgumentosCambioDeEstado> //los delegados Action no regresan ningun valor.
        //void Handler(object sender, ArgumentosCambioDeEstado args)
        public event EventHandler<ArgumentosCambioDeEstado> EnCambioDeEstado;//un evento, una clase especial de delegado que puede guardar muchos metodos. se llama EnCambioDeEstado.// todos los metodos que se agregen aqui, deben implementar la firma que regresa void, y recibe un object y el otro objeto que hereda de envent argrs.
        public Termostato(TanqueDeAgua tanque)
        {
            TemLimiteSup = 40;
            TemLimiteInferior = 20;
            Estado = Estado.Piloto;
            Tanque = tanque;
            Tanque.EnCambioTemperatura += ChecarEstado;//handler, cuando se haga una instancia de este objeto, el metodo ChecarEstado se va ejecutar.
        }

        public TanqueDeAgua Tanque { get; set; }
        public float TemLimiteSup { get; set; }
        public float TemLimiteInferior { get; set; }
        public Estado Estado { get; set; }

        private void ChecarEstado(object sender, EventArgs e)
        {
            Estado estadoActual = Estado;
            if (Tanque.Temperatura > TemLimiteSup)
            {
                Estado = Estado.Piloto;
            }
            if (Tanque.Temperatura < TemLimiteInferior)
            {
                Estado = Estado.Encendido;
            }
            if (estadoActual != Estado)
            {
                if (EnCambioDeEstado!=null)//si no es nulo, lanzar el evento.
                {
                    EnCambioDeEstado(this, new ArgumentosCambioDeEstado(Estado));//se dispara el evento es decir, se manda llamar a todos los metodos que se registraron en este evento. y se les pasa los parametros correspondientes con la firma. this es el parametro sender, y se refiere a una instancia de esta clase que lanzo el evento.
                }
            }
        }
    }
    public class TanqueDeAgua
    {
        public event EventHandler<System.EventArgs> EnCambioTemperatura;
        private float mTemperatura = 0;
        public float Temperatura 
        {
            get { return mTemperatura; }
            set 
            {
                mTemperatura = value;
                if (EnCambioTemperatura != null)//si no es nulo, lanzar el evento.
                {
                    EnCambioTemperatura(this, new EventArgs());//se dispara el evento es decir, se manda llamar a todos los metodos que se registraron en este evento. y se les pasa los parametros correspondientes con la firma. this es el parametro sender, y se refiere a una instancia de esta clase que lanzo el evento.
                }; 
            }
        }
    }

    public class BoilerAutomatico
    {
        TanqueDeAgua Tanque { get; set; }//aqui se guarda un objeto que viene del constructor, y asi se puede accesar via instancia a la clase TanqueDeAgua en esta clase.
        private Timer Timer { get; set; }
        private Termostato Termostato { get; set; }//Termostato y Quemador, las clases, no estan conectadas, se van a conectar a travez de un evento.
        private Quemador Quemador { get; set; }

        public BoilerAutomatico() 
        {
            Tanque = new TanqueDeAgua();
            Termostato = new Termostato(Tanque);
            Timer = new Timer(100);//cada 100 milisegundsw va ejecutar lo que haya en el metodo.
            Timer.Enabled = false;//no esta habilitado.
            Quemador = new Quemador(Tanque);    
            Timer.Elapsed += DespacharAgua;//este es un EventHandler y como tal guarda un arreglo de metodos, es como un delegado, pero un arreglo de delegados. y se le agregan metodos con el +=.DespacharAgua es un metodo que cumple con la firma de Time.Elapsed.

            Termostato.EnCambioDeEstado += ManejarQuemador;//este es un EventHandlere//ManejarQuemador es el nombre del metodo que se va guardar en CambioDeEstado que es el evento y que tiene cierta firma.
        }

        void ManejarQuemador(object sender, ArgumentosCambioDeEstado e)//un metodo que simplemente cumple con la firma, recibe un object y un objeto que hereda de eventArgs.
        {
            switch (e.Estado)//e. es un objeto de la clase ArgumentosCambioDeEstado, que hereda de args y que tiene la propiedad Estado.
            {
                case Estado.Encendido:
                    System.Diagnostics.Debug.WriteLine("Quemador Encendido");
                    Quemador.CalentarAgua();
                    break;
                case Estado.Piloto:
                    Quemador.DejarDeCalentar();
                    System.Diagnostics.Debug.WriteLine("Quemador Apagado");
                    break;
                default:
                    Quemador.DejarDeCalentar();
                    System.Diagnostics.Debug.WriteLine("Quemador Apagado");
                    break;
            }
        }

        private void DespacharAgua(object sender, ElapsedEventArgs e)//cada vez que pase un segundo, se va ejecutar este metodo.//porque este metodo esta agregado al evento Timer.elapsed.
        {
            Tanque.Temperatura -= 1;//es igual a TempAgua=TemAgua-1.
            System.Diagnostics.Debug.WriteLine("{0}°C ", Tanque.Temperatura);
        }

        public void DespacharAguaInicio()
        {
            Timer.Enabled = true;//este activa el timer, porque en el constructor esta desactivado. y este es un evento que manejara el EvetHandler Timer.Elapsed += DespacharAgua, es decir, se va activar el metodo DespacharAgua el cual decrementa la temperatura del agua -1 grado cada vez que se manda llamar.           
        }
        public void DespacharAguaFin() 
        {
            Timer.Enabled = false;//desactiva el timer.
        }

    }
    public enum Estado 
    {
        Encendido,
        Piloto
    }
}
