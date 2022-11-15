//Alondra Yocelin Osornio Vega
using System;
using System.IO;
namespace GENERADOR
{
    public class Error : Exception
    {
        public Error(string mensaje, StreamWriter log)
        {
            log.WriteLine(mensaje);
        }
    }
}