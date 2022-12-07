//Alondra Yocelin Osornio Vega
using System;
using System.Collections.Generic;
//Requerimiento 1: Construir un metodo para escribir en el archivo lenguaje.cs identando el codigo
//                 { incrementa un tabulador, } decrementa un tabulador
//Requerimiento 2: Declarar un atributo primeraProduccion de tipo string y actualizarlo con la primera
//                 produccion de la gramatica
//Requerimiento 3: La primera produccion es publica y el resto privadas
//Requerimiento 4: El constructor Lexico() parametrizado debe validar que la extension del archivo a compilar sea .gen y si no es .gen debe lanzar una excepcion 
//Requerimiento 5: Resolver la ambigüedad de ST y SNT 
//Requerimiento 6: Recorrer linea por linea el archivo .gram 
//Requerimiento 7: Implementar el or y la cerradura epsilon
namespace GENERADOR
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        string primeraProduccion;
        bool primeraProduccionPublica;
        List<string> listaSNT;


        public Lenguaje(string nombre) : base(nombre)
        {
            primeraProduccion = "";
            primeraProduccionPublica = true;
            listaSNT = new List<string>();
        }
        public Lenguaje()
        {
            primeraProduccion = "";
            primeraProduccionPublica = true;
            listaSNT = new List<string>();
        }
        public void Dispose()
        {
            cerrar();
        }

        private bool esSNT(string contenido)
        {
            return listaSNT.Contains(contenido);
        }
        private void agregarSNT(string contenido)
        {
            //Requerimiento 6
            listaSNT.Add(contenido);
        }

        public void gramatica()
        {
            int contadorDeTabular = 0;
            cabecera();
            primeraProduccion = getContenido();
            Programa(primeraProduccion);
            cabeceraLenguaje();
            listaProducciones();
            contadorDeTabular = 1;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "}");
            lenguaje.WriteLine("}");
        }
        private void cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.ST);
            match(Tipos.FinProduccion);
        }
        private void Programa(string produccionPrincipal)
        {
            agregarSNT("Programa");
            agregarSNT("Librerias");
            agregarSNT("variables");
            agregarSNT("Lista identificadores");
            int contadorDeTabular = 0;
            programa.WriteLine("using System;");
            programa.WriteLine("using System.IO;");
            programa.WriteLine("using System.Collections.Generic;");
            programa.WriteLine();
            programa.WriteLine("namespace Generico");
            programa.WriteLine("{");
            contadorDeTabular++;
            programa.WriteLine(tabula(contadorDeTabular) + "public class Program");
            programa.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            programa.WriteLine(tabula(contadorDeTabular) + "static void Main(string[] args)");
            programa.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            programa.WriteLine(tabula(contadorDeTabular) + "try");
            programa.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            programa.WriteLine(tabula(contadorDeTabular) + "using (Lenguaje a = new Lenguaje())");
            programa.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            programa.WriteLine(tabula(contadorDeTabular) + "a." + produccionPrincipal + "();");
            contadorDeTabular--;
            programa.WriteLine(tabula(contadorDeTabular) + "}");
            contadorDeTabular--;
            programa.WriteLine(tabula(contadorDeTabular) + "}");
            programa.WriteLine(tabula(contadorDeTabular) + "catch (Exception e)");
            programa.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            programa.WriteLine(tabula(contadorDeTabular) + "Console.WriteLine(e.Message);");
            contadorDeTabular--;
            programa.WriteLine(tabula(contadorDeTabular) + "}");
            contadorDeTabular--;
            programa.WriteLine(tabula(contadorDeTabular) + "}");
            contadorDeTabular--;
            programa.WriteLine(tabula(contadorDeTabular) + "}");
            contadorDeTabular--;
            programa.WriteLine(tabula(contadorDeTabular) + "}");
        }
        
        private void cabeceraLenguaje()
        {
            int contadorDeTabular = 0;
            lenguaje.WriteLine("using System;");
            lenguaje.WriteLine("using System.Collections.Generic;");
            lenguaje.WriteLine("namespace Generico");
            lenguaje.WriteLine("{");
            contadorDeTabular++;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "public class Lenguaje : Sintaxis, IDisposable");
            lenguaje.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "public Lenguaje(string nombre) : base(nombre)");
            lenguaje.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            contadorDeTabular--;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "}");
            lenguaje.WriteLine(tabula(contadorDeTabular) + "public Lenguaje()");
            lenguaje.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            contadorDeTabular--;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "}");
            lenguaje.WriteLine(tabula(contadorDeTabular) + "public void Dispose()");
            lenguaje.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "cerrar();");
            contadorDeTabular--;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "}");
        }
        private void listaProducciones()
        {
            int contadorDeTabular = 2;
            if(primeraProduccionPublica)
            {
                lenguaje.WriteLine(tabula(contadorDeTabular) + "public void " + getContenido() + "()");
                primeraProduccionPublica = false;
            }
            else
            {
                lenguaje.WriteLine(tabula(contadorDeTabular) + "private void " + getContenido() + "()");
            }
            lenguaje.WriteLine(tabula(contadorDeTabular) + "{");
            contadorDeTabular++;
            match(Tipos.ST);
            match(Tipos.Produce);
            simbolos();
            match(Tipos.FinProduccion);
            contadorDeTabular--;
            lenguaje.WriteLine(tabula(contadorDeTabular) + "}");
            if (!FinArchivo())
            {
                listaProducciones();
            }
        }

        private void simbolos()
        {
            int contadorDeTabular = 3;
            if(getContenido() == "(")
            {
                match("(");
                lenguaje.WriteLine(tabula(contadorDeTabular) + "if()");
                lenguaje.WriteLine(tabula(contadorDeTabular) + "{");
                contadorDeTabular++;
                simbolos();
                match(")");
                contadorDeTabular--;
                lenguaje.WriteLine(tabula(contadorDeTabular) + "}");
            }
            else if(esTipo(getContenido()))
            {
                lenguaje.WriteLine(tabula(contadorDeTabular) + "match(Tipos." + getContenido() + ");");
                match(Tipos.ST);
            }
            else if(esSNT(getContenido()))
            {
                lenguaje.WriteLine(tabula(contadorDeTabular) + "match(\"" + getContenido() + "\");");
                match(Tipos.ST);
            }
            else if(getClasificacion() == Tipos.ST)
            {
                lenguaje.WriteLine(tabula(contadorDeTabular) + "match(\"" + getContenido() + "\");");
                match(Tipos.ST);
            }        
            if(getClasificacion() != Tipos.FinProduccion && getContenido() != ")")
            {
                simbolos();
            }
        }

        private bool esTipo(string clasificacion)
        {
            switch(clasificacion)
            {
                case "Identificador":
                case "Numero":
                case "Caracter":
                case "Asignacion":
                case "Inicializacion":
                case "OperadorLogico":
                case "OperadorRelacional":
                case "OperadorTernario":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "FinSentencia":
                case "Cadena":
                case "TipoDato":
                case "caseZona":
                case "Condicion":
                case "Ciclo":
                    return true;
            }
            return false;
        }

        private string tabula(int contadorDeTabular)
        {
            string tab = "";
            for(int i = 0; i < contadorDeTabular; i++)
            {
                tab += "\t";
            }
            return tab;
        }
    }
}