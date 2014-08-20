using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace WebRequestProrgram
{
    public class State : IDisposable//esta clase se utiliza en la clase program.
    {
        public State(WebRequest webRequest)
        {
            WebRequest = webRequest;
        }
        public WebRequest WebRequest { get; private set; }
        private ManualResetEventSlim _ResetEvent =
        new ManualResetEventSlim();
        public ManualResetEventSlim ResetEvent
        { get { return _ResetEvent; } }
        public void Dispose()
        {
            ResetEvent.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    class Program
    {//APM async programing .. va avisando el estado del thread, si ya acabo a travez de un handler, que en este caso, cada 100 milisec. evalua si ha acabado.
        static void Main(string[] args)
        {
            string url = "http://www.google.com";
            if (args.Length>0)//si tiene algo...
            {
                url = args[0];
            }
            Console.Write(url);
            WebRequest webRequest = WebRequest.Create(url);//aqui se crea otro hilo.
            State state = new State(webRequest);

            IAsyncResult asyncResult = webRequest.BeginGetResponse(GetResponseAsyncComplete, state);//se da start al hilo que se acaba de crear. se va ejecutar el metodo GetResponseComplete cuando termine.
            while (!asyncResult.AsyncWaitHandle.WaitOne(100))//cuando el otro hilo de webRequest regresa false, se sale del ciclo y mientras no, habra loop cada 100 milisegundos.
            {
                Console.Write('.');
            }
            Console.ReadLine();
        }

        private static void GetResponseAsyncComplete(IAsyncResult asyncResult)//recibe asyncResult que trae el state.
        {
            State completedState = (State)asyncResult.AsyncState;
            WebResponse response = (WebResponse)completedState.WebRequest.EndGetResponse(asyncResult);
            StreamReader reader = null; 
            try
            {
                reader = new StreamReader(response.GetResponseStream());
                
                int lenght = reader.ReadToEnd().Length;
                Console.WriteLine(FormatBytes(lenght));
                
            }
            catch (AggregateException es)
            {
                foreach (Exception e in es.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }  
            }
            finally 
            {
                if (reader != null) 
                {
                    reader.Dispose();
                }
            }
            completedState.ResetEvent.Set();
            completedState.Dispose();
        }
        static public string FormatBytes(long bytes)
        {
            string[] magnitudes = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(1024, magnitudes.Length);
            return string.Format("{1:##.##} {0}",magnitudes.FirstOrDefault(magnitude=>bytes>(max/=1024))?? "0 Bytes",(decimal)bytes/(decimal)max).Trim();
        }
    }
}
