﻿//Alondra Yocelin Osornio Vega
using System;
namespace GENERADOR{
    public class Program{

        static void Main(string[] args){
            using(Lenguaje a = new Lenguaje()){
                try{
                    
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}