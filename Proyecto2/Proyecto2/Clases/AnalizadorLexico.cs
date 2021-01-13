using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2.Clases
{
    class AnalizadorLexico
    {
        private int numero;
        private int estadoActual;//variable que indica el estado actual
        public int numError;//conteo del numero de errores registrados
        public DataRow fila;//elemento de fila de tabla
        public DataRow fila2;//elemento de fila de tabla 2
        public DataTable tblResultado = new DataTable();//tabla de tokens
        public DataTable tblError = new DataTable();//tabla de errores
        private string auxLexemaAcumulado;//cadena leida
        private  int posInicial = 0;//posicion inicial
        string[] palabrasReservadas = { "Principal", "Intervalo", "Duracion", "Bloque_Nivel", "Bloque_Enemigo", "Bloque_Personaje","Nivel", "Dimensiones", "Inicio_personaje", "Ubicacion_Salida", "Pared", "Casilla" , "Varias_Casillas" , "Enemigo", "Caminata","Personaje", "Paso","Variable","caminata" };
        string t = "Error Lexico";
        public List<Token> tokens { get; set; }//lista de tokens
        public List<Token> errors { get; set; }// lista de errorres

        public AnalizadorLexico()
        {
            errors = new List<Token>();
        }

        string entrada = "";
        public List<Token> Scanner(string entradas)
        {
            entrada = entradas + "#";
            tokens = new List<Token>();
            numero = 0;
            estadoActual = 0;
            auxLexemaAcumulado = "";
            int ffila = 1;
            int colum = 0;
            char c;

            for (int i =0; i<entrada.Length-1; i+= 1)
            {
                c = entrada[i];
                colum++;

                if (auxLexemaAcumulado.Equals(""))
                {
                    posInicial = i + 1;
                }

                switch (estadoActual)
                {
                    case 0:
                        {
                            if (char.IsDigit(c))
                            {
                                estadoActual = 2;
                                auxLexemaAcumulado += c;
                            }
                            else if (char.IsLetter(c))
                            {
                                estadoActual = 3;
                                auxLexemaAcumulado += c;
                            }
                            else if ((c == '['))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.CORCHETEA, posInicial, ffila,colum,numero);
                            }
                            else if ((c == ']'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.CORCHETEC, posInicial, ffila, colum, numero);
                            }
                            else if ((c == ':'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.DOSPUNTOS, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '{'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.LLAVEA, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '}'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.LLAVEC, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '('))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.PARENTESISA, posInicial, ffila, colum, numero);
                            }
                            else if ((c == ')'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.PARENTESISC, posInicial, ffila, colum, numero);
                            }
                            else if ((c == ';'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.PUNTOCOMA, posInicial, ffila, colum, numero);
                            }
                            else if ((c == ','))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.COMA, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '.'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.PUNTO, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '='))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.IGUAL, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '+'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.MAS, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '*'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.POR, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '/'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.DIVISION, posInicial, ffila, colum, numero);
                            }
                            else if ((c == '-'))
                            {
                                auxLexemaAcumulado += c;
                                numero++;
                                AddToken(Token.Tipo.MENOS, posInicial, ffila, colum, numero);
                            }
                            else if ((c == ' ') || (c == '\t') || (c == '\r'))
                            {
                                estadoActual = 0;
                            }
                            else if ((c == '\n'))
                            {
                                ffila++;
                                posInicial = 0;
                                colum = 0;
                            }
                            else if ((c == '#' & i == entrada.Length - 1))
                            {
                                Console.WriteLine("Analisis Lexico Concluido satisfactoriamente");
                            }
                            else
                            {
                                numError++;
                                Console.WriteLine("Error con: " + c);
                                AddError(Token.Tipo.ERROR, i, ffila, c, colum, t, numError);
                                estadoActual = 0;
                            }
                            break;
                        }
                    case 2:
                        {
                            if ((char.IsDigit(c)))
                            {

                                estadoActual = 2;
                                auxLexemaAcumulado += c;
                            }
                            else
                            {
                                numero++;
                                AddToken(Token.Tipo.NUMERO, posInicial, ffila, colum, numero);
                                i -= 1;
                            }
                            break;
                        }
                    case 3:
                        {
                            if ((char.IsLetter(c)))
                            {
                                estadoActual = 3;
                                auxLexemaAcumulado += c;
                            }
                            else if ((c == '_'))
                            {
                                estadoActual = 3;
                                auxLexemaAcumulado += c;
                            }
                            else if ((char.IsDigit(c)))
                            {
                                estadoActual = 3;
                                auxLexemaAcumulado += c;
                            }
                            else
                            {
                                bool match = false;
                                foreach (string r in palabrasReservadas)
                                {
                                    if (r.Equals(auxLexemaAcumulado))
                                    {
                                        numero++;
                                        AddToken(Token.Tipo.RESERVADA, posInicial, ffila, colum, numero);
                                        i -= 1;
                                        match = true;
                                    }
                                }
                                if (!match)
                                {
                                    numero++;
                                    AddToken(Token.Tipo.ID,posInicial,ffila, colum,numero);
                                    i -= 1;
                                }
                            }
                            break;
                        }
                        
             
                }//cierre swint
            }//cierre for
            return tokens;
            
        }// cierre metodo

        int num = 0;
        public void imprimirLista(List<Token> l)
        {

            tblResultado.Clear();
            tblResultado.Columns.Add("No",typeof(string));
            tblResultado.Columns.Add("Lexema", typeof(string));
            tblResultado.Columns.Add("Tipo", typeof(string));
            tblResultado.Columns.Add("Fila", typeof(string));
            tblResultado.Columns.Add("Columna", typeof(string));

            foreach (Token t in l)
            {
                    fila = tblResultado.NewRow();
                    tblResultado.NewRow();
                    fila["No"] = t.getnumero();
                    fila["Lexema"] = t.getLexema();
                    fila["Tipo"] = t.GetTipoEnString();
                    fila["Fila"] = t.GetFila();
                    fila["Columna"] = t.GetCol();
                    tblResultado.Rows.Add(fila);
            }
            convertirhtml(tblResultado);

        }

        public void imprimirListaE(List<Token> e)
        {

            tblError.Clear();
            tblError.Columns.Add("No", typeof(string));
            tblError.Columns.Add("Lexema", typeof(string));
            tblError.Columns.Add("Tipo", typeof(string));
            tblError.Columns.Add("Fila", typeof(string));
            tblError.Columns.Add("Columna", typeof(string));

            foreach (Token t in e)
            {


                fila2 = tblError.NewRow();
                tblError.NewRow();
                fila2["No"] = num++;
                fila2["Lexema"] = t.getLexema();
                fila2["Tipo"] = "Elemento desconocido";
                fila2["Fila"] = t.GetFila();
                fila2["Columna"] = t.GetCol();
                tblError.Rows.Add(fila2);
            }
            convertirhtml2(tblError);

        }

        public string convertirhtml(DataTable dt)
        {




            string html = "<h1>Lista de  Tokens</h1>";
            
            html += "<table border= \"1\", style=\"margin: 0 auto; \" >";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";


            string rutaCompleta = @"C:\Users\aleja\Desktop\mi archivo.html";
            using (StreamWriter mylogs = File.AppendText(rutaCompleta))         //se crea el archivo
            {

                //se adiciona alguna información y la fecha
                mylogs.WriteLine(html);

                mylogs.Close();


            }
            if (File.Exists(rutaCompleta))
            {
                Process.Start(rutaCompleta);
            }

            return html;
        }

        public string convertirhtml2(DataTable dt)
        {
            string html = "<h1>Lista de  Errores Lexicos</h1>";

             html += "<table border= \"1\", style=\"margin: 0 auto; \" >";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";


            string rutaCompleta = @"C:\Users\aleja\Desktop\mi archivo error.html";
            using (StreamWriter mylogs = File.AppendText(rutaCompleta))         //se crea el archivo
            {

                //se adiciona alguna información y la fecha
                mylogs.WriteLine(html);

                mylogs.Close();


            }
            if (File.Exists(rutaCompleta))
            {
                Process.Start(rutaCompleta);
            }

            return html;
        }

        private void AddToken(Token.Tipo tipo, int posinicial, int fila,int col,int num)
        {
            tokens.Add(new Token(tipo,auxLexemaAcumulado,posinicial,fila,col, num));
            auxLexemaAcumulado = "";
            estadoActual = 0;
        }

        public void AddError(Token.Tipo tipo, int posinicial,int fila, char cod, int col, string t, int num)
        {
            errors.Add(new Token(Token.Tipo.ERROR,cod.ToString(),posinicial,fila,col,num));
            auxLexemaAcumulado = "";
            estadoActual = 0;
            return; 
        }

    }// cierre clase
}
