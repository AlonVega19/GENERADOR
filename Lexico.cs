//Alondra Yocelin Osornio Vega
using System.IO;

namespace GENERADOR
{
    public class Lexico : Token
    {
        protected StreamReader archivo;
        protected StreamWriter log;
        protected StreamWriter lenguaje;
        protected StreamWriter programa;
        const int F = -1;
        const int E = -2;
        protected int linea, posicion = 0;
        int[,] TRAND = new int[,]
        {
            {0,1,5,3,4,5},
            {F,F,2,F,F,F},
            {F,F,F,F,F,F},
            {F,F,F,3,F,F},
            {F,F,F,F,F,F},
            {F,F,F,F,F,F},
        };
        public Lexico()
        {
            linea = 1;
            string path = "c.gram";
            bool existencia = File.Exists(path);
            log = new StreamWriter("c.Log"); 
            log.AutoFlush = true;
            lenguaje = new StreamWriter("C:\\LYAII\\U3\\GENERICO\\Lenguaje.cs");
            lenguaje.AutoFlush = true;
            programa = new StreamWriter("C:\\LYAII\\U3\\GENERICO\\Program.cs");
            programa.AutoFlush = true;
            log.WriteLine("Archivo: c.gram");
            log.WriteLine("Fecha "+DateTime.Now);
            if (existencia == true)
            {
                archivo = new StreamReader(path);
            }
            else
            {
                throw new Error("Error: El archivo prueba no existe", log);
            }
        }
        public Lexico(string nombre)
        {
            linea = 1;
            string pathLog = Path.ChangeExtension(nombre, ".log");
            log = new StreamWriter(pathLog); 
            log.AutoFlush = true;
            lenguaje = new StreamWriter("C:\\LYAII\\U3\\GENERICO\\Lenguaje.cs");
            lenguaje.AutoFlush = true;
            programa = new StreamWriter("C:\\LYAII\\U3\\GENERICO\\Program.cs");
            programa.AutoFlush = true;
            log.WriteLine("Archivo: "+nombre);
            log.WriteLine("Fecha: " +DateTime.Now);
            if (File.Exists(nombre))
            {
                archivo = new StreamReader(nombre);
            }
            else
            {
                throw new Error("Error: El archivo " +Path.GetFileName(nombre)+ " no existe ", log);
            }
        }
        public void cerrar()
        {
            archivo.Close();
            log.Close();
            lenguaje.Close();
            programa.Close();
        }       
        private void clasifica(int estado)
        {
            switch(estado)
            {
                case 1:
                    setClasificacion(Tipos.ST);
                    break;
                case 2:
                    setClasificacion(Tipos.Produce);
                    break;
                case 3:
                    setClasificacion(Tipos.ST);
                    break;
                case 4:
                    setClasificacion(Tipos.FinProduccion);
                    break;
                case 5:
                    setClasificacion(Tipos.ST);
                    break;
            }
        }
        private int columna(char c)
        {
            if(c == 10)
            {
                return 4;
            }
            else if (char.IsWhiteSpace(c))
            {
                return 0;
            }
            else if(c == '-')
            {
                return 1;
            }
            else if (c == '>')
            {
                return 2;
            }
            else if(char.IsLetter(c))
            {
                return 3;
            }
            return 5;
        }
        public void NextToken() 
        {
            string buffer = "";           
            char c;      
            int estado = 0;
            while(estado >= 0)
            {
                c = (char)archivo.Peek(); //Funcion de transicion
                estado = TRAND[estado,columna(c)];
                clasifica(estado);
                if (estado >= 0)
                {
                    archivo.Read();
                    posicion++;
                    if(c == '\n')
                    {
                        linea++;
                    }
                    if (estado >0)
                    {
                        buffer += c;
                    }
                    else
                    {
                        buffer = "";
                    }
                }
            }
            setContenido(buffer); 
            if(estado == E)
            {
                throw new Error("Error lexico: No definido en linea: "+linea, log);
            }
            if(!FinArchivo())
            {
                log.WriteLine(getContenido() + " " + getClasificacion());
            }
        }

        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}