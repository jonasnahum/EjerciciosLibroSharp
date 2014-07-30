using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap13Events
{
    public class MetronomeArgs : EventArgs
    {
        public MetronomeArgs(int current)
        {
            this.CurrentInterval = current;
        }
        public int CurrentInterval { get; set; }
    }

    public class MetronomeMachine
    {
        /// <summary>
        /// Inicia una clase metrónomo
        /// </summary>
        /// <param name="intervalCount">Número de veces a ejecutarse</param>
        /// <param name="intervalDuration">Cantidad de tiempo en milisegúndos que pasará antes del siguiente intervalo</param>
        public MetronomeMachine(int intervalCount, int intervalDuration)
        {
            this.IntervalCount = intervalCount;
            this.IntervalDuration = intervalDuration;
        }

        private int IntervalCount { get; set; }
        private int IntervalDuration { get; set; }

        //declarar evento
        public event TickHandler Tick;
        public delegate void TickHandler(MetronomeMachine m, MetronomeArgs e);//el primer parametro es la clase desde donde se emite el evento, el sender, y el segundo deriva de args, y en esta clase, si se quiere, se guardan algunas variables que quieran ser enviadas como parametro.
        //public event EventHandler<MetronomeArgs> Tick; equivale a las dos lineas previas
        
        public void Start()
        {
            while (IntervalCount > 0)//si el numero de vecesaejecutarse es> que 0.
            {
                System.Threading.Thread.Sleep(IntervalDuration);// durar tal cantidad de tiempo en milisegundos.
                if (Tick != null)// siempre checar q no es nulo antes de lanzar evento debido a la inmutabiliad.
                {
                    Tick(this, new MetronomeArgs(IntervalCount)); //lanzar evento los parametros son this, esta clase, y metronomeArgs, que se guarda en una variable dentro de la clase MetronomeArgs y se convierte en CurrentInterval. esta ultima clase siempre tiene que derivar de EventArgs. y se utiliza asi para guardar variables que quieran ser enviadas como parametros a la hora de disparar el evento.
                }
                IntervalCount--;//como es un loop que itera a lo largo de IntervaleCount, se va disminuyendo este numero con cada iteracion, hasta completar todos los eventos indicados.
            }
        }
    }

    public class SoundListener
    {
        /// <summary>
        /// Escucha al metrónomo y emite un sonido
        /// </summary>
        /// <param name="frequency">Frecuencia en herts</param>
        /// <param name="duration">Duración en milisegundos</param>
        public SoundListener(int frequency, int duration)
        {
            this.Frequency = frequency;
            this.Duration = duration;
        }

        public SoundListener() : this(440, 500) { }

        private int Frequency { get; set; }
        private int Duration { get; set; }

        public void Subscribe(MetronomeMachine m)
        {
            m.Tick += EmitSound;//suscribe la variable EmitSound al evento Tick.
        }

        private void EmitSound(MetronomeMachine m, MetronomeArgs e)//metodo que cumple con el delegado del evento., recibe un sender, de donde se emite el evento y un arg.
        {
            Console.Beep(Frequency, Duration);
        }

    }

    public class TextListener
    {
        /// <summary>
        /// Escucha al metrónomo y emite un sonido
        /// </summary>
        /// <param name="frequency">Frecuencia en herts</param>
        /// <param name="duration">Duración en milisegundos</param>
        public TextListener(int frequency, int duration)
        {
            this.Frequency = frequency;
            this.Duration = duration;
        }

        public TextListener() : this(440, 500) { }

        private int Frequency { get; set; }
        private int Duration { get; set; }

        public void Subscribe(MetronomeMachine m)
        {
            m.Tick += PrintToConsole;
        }

        private void PrintToConsole(MetronomeMachine m, MetronomeArgs e)
        {
            System.Diagnostics.Debug.Print("Intervalo: {0}", e.CurrentInterval);
        }

    }
}