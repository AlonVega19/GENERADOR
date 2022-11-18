//Alondra Yocelin Osornio Vega
using System;
using System.IO;
namespace GENERADOR
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using(Lenguaje a = new Lenguaje("c2.gram"))
                {
                    a.gramatica();
                    /*
                    while(!a.FinArchivo())
                    {
                        a.NextToken();
                    }*/
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}