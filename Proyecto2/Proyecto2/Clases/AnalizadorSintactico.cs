using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Proyecto2.Clases
{
    class AnalizadorSintactico
    {

        public int numPreAnalisis;
        public Token preanalisis;
        public List<Token> listaTokens;
        private List<ErrorSintactico> le;
        public int numError2;
        public DataTable tblError = new DataTable();
        private DataRow fila2;
        private string listaErrores = "";
        private int num = 0;

        public AnalizadorSintactico()
        {
            le = new List<ErrorSintactico>();
        }

        public void parser(List<Token> lista)
        {
            listaTokens = lista;
            preanalisis = listaTokens[0];
            numPreAnalisis = 0;
            A();
        }

        private void A()
        {
            match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.LLAVEA);
            B();
            match(Token.Tipo.LLAVEC);
        }

        private void B()
        {
            //match(Token.Tipo.CORCHETEA);
            //match(Token.Tipo.RESERVADA);
            //match(Token.Tipo.CORCHETEC);
            //match(Token.Tipo.DOSPUNTOS);
            //match(Token.Tipo.PARENTESISA);
            //match(Token.Tipo.NUMERO);
            //match(Token.Tipo.PARENTESISC);
            //match(Token.Tipo.PUNTOCOMA);
            C();
        }

        private void C()
        {

            if (preanalisis.getLexema().Equals("["))
            {
                try
                {
                    match(Token.Tipo.CORCHETEA);
                    if (preanalisis.getLexema().Equals("Nivel"))
                    {
                        BN();
                    }
                    else if (preanalisis.getLexema().Equals("Enemigo"))
                    {
                        BE();
                    }
                    else if (preanalisis.getLexema().Equals("Personaje"))
                    {
                        BP();
                    }else if (preanalisis.getLexema().Equals("Intervalo"))
                    {
                        Intervalo();
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Se acabo la lista", e);
                }
            }
        }

        private void Intervalo()
        {
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            match(Token.Tipo.NUMERO);
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
            C();
        }

        private void BN()
        {
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.LLAVEA);
            BNC();
            match(Token.Tipo.LLAVEC);
            C();
        }

        private void BNC()
        {
            D();
            IP();
            US();
            P();
        }

        private void D()
        {
            match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            match(Token.Tipo.NUMERO);
            match(Token.Tipo.COMA);
            match(Token.Tipo.NUMERO);
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
        }

        private void IP()
        {
            match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            match(Token.Tipo.NUMERO);
            match(Token.Tipo.COMA);
            match(Token.Tipo.NUMERO);
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
        }

        private void US()
        {
            match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            match(Token.Tipo.NUMERO);
            match(Token.Tipo.COMA);
            match(Token.Tipo.NUMERO);
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
        }

        private void P()
        {
            match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.LLAVEA);
            E();
            match(Token.Tipo.LLAVEC);
        }

        private void E()
        {
            if (preanalisis.getLexema().Equals("["))
            {

                try
                {
                    match(Token.Tipo.CORCHETEA);
                    if (preanalisis.getLexema().Equals("Casilla"))
                    {
                        Casilla();
                    }
                    else if (preanalisis.getLexema().Equals("Varias_Casillas"))
                    {
                        VariasCasillas();
                    }
                    else if (preanalisis.getLexema().Equals("Variable"))
                    {
                        Variable();
                    }
                    
                }
                catch
                {
                    Console.WriteLine("Error en recursividad casillas");
                }

            }
            else if(preanalisis.GetTipoEnString().Equals("Identificador"))
            {
                Asignacion();
            }
        }

        private void Casilla()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            Opciones3();
            match(Token.Tipo.COMA);
            Opciones3();
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
            E();
        }


        private void VariasCasillas()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            Opciones();
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
            E();
        }

        private void Variable()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            I();
            match(Token.Tipo.PUNTOCOMA);
            E();
        }

        private void Asignacion()
        {
            match(Token.Tipo.ID);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.IGUAL);
            J();
            match(Token.Tipo.PUNTOCOMA);
            E();
        }
        private void Asignacion2()
        {
            match(Token.Tipo.ID);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.IGUAL);
            J();
            match(Token.Tipo.PUNTOCOMA);
            G();
        }

        private void J()
        {
            
            if (preanalisis.GetTipoEnString().Equals("Identificador"))
            {
                match(Token.Tipo.ID);
                if (!preanalisis.getLexema().Equals(";"))
                {
                    Expresion();
                }
            }
            else if (preanalisis.GetTipoEnString().Equals("Digito"))
            {
                match(Token.Tipo.NUMERO);
                if (!preanalisis.getLexema().Equals(";"))
                {
                    Expresion();
                }
            }
        }

        private void Expresion()
        {
            
            if (preanalisis.getLexema().Equals("+"))
            {
                match(Token.Tipo.MAS);
                Opciones3();
            }
            else if (preanalisis.getLexema().Equals("-"))
            {
                match(Token.Tipo.MENOS);
                Opciones3();
            }
            else if (preanalisis.getLexema().Equals("/"))
            {
                match(Token.Tipo.DIVISION);
                Opciones3();
            }
            else if (preanalisis.getLexema().Equals("*"))
            {
                match(Token.Tipo.POR);
                Opciones3();
            }
            
        }

        private void Opciones3()
        {
            if (preanalisis.GetTipoEnString().Equals("Identificador"))
            {
                match(Token.Tipo.ID);
            }
            else if (preanalisis.GetTipoEnString().Equals("Digito"))
            {
                match(Token.Tipo.NUMERO);
            }
        }

        private void Opciones()
        {

            Opciones3();

            if (preanalisis.GetTipo() == Token.Tipo.PUNTO)
            {
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.PUNTO);
                Opciones3();
                match(Token.Tipo.COMA);
                Opciones3();
                if (preanalisis.GetTipo() == Token.Tipo.PUNTO)
                {
                    match(Token.Tipo.PUNTO);
                    match(Token.Tipo.PUNTO);
                    Opciones3();
                }
            }
            else if (preanalisis.GetTipo() == Token.Tipo.COMA)
            {
                match(Token.Tipo.COMA);
                Opciones3();
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.PUNTO);
                Opciones3();
            }
        }

        private void I()
        {
            match(Token.Tipo.ID);
            if (preanalisis.getLexema().Equals(","))
            {
                match(Token.Tipo.COMA);
                I();
            }
        }

        private void BE()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.LLAVEA);
            F();
            match(Token.Tipo.LLAVEC);
            C();
        }

        private void F()
        {
            if (preanalisis.getLexema().Equals("["))
            {
                try
                {
                    match(Token.Tipo.CORCHETEA);
                    if (preanalisis.getLexema().Equals("Caminata"))
                    {
                        Caminata();
                    }
                }
                catch
                {
                    Console.WriteLine("Error en recursividad de caminatas");
                }
            }
           
        }

        private void Caminata()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            Opciones2();
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
            F();
        }

        private void Opciones2()
        {
            match(Token.Tipo.NUMERO);
            if (preanalisis.GetTipo() == Token.Tipo.PUNTO)
            {
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.NUMERO);
                match(Token.Tipo.COMA);
                match(Token.Tipo.NUMERO);
            }
            else if (preanalisis.GetTipo() == Token.Tipo.COMA)
            {
                match(Token.Tipo.COMA);
                match(Token.Tipo.NUMERO);
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.NUMERO);
               
            }
        }

        private void Opciones4()
        {
            Opciones3();
            if (preanalisis.GetTipo() == Token.Tipo.PUNTO)
            {
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.PUNTO);
                Opciones3();
                match(Token.Tipo.COMA);
                Opciones3();
            }
            else if (preanalisis.GetTipo() == Token.Tipo.COMA)
            {
                match(Token.Tipo.COMA);
                Opciones3();
                match(Token.Tipo.PUNTO);
                match(Token.Tipo.PUNTO);
                Opciones3();
            }
        }

        private void BP()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.LLAVEA);
            G();
            match(Token.Tipo.LLAVEC);
            C();
        }

        private void G()
        {
            if (preanalisis.getLexema().Equals("["))
            {
                try
                {
                    match(Token.Tipo.CORCHETEA);
                    if (preanalisis.getLexema().Equals("Paso"))
                    {
                        Paso();
                    }
                    else if(preanalisis.getLexema().Equals("Caminata"))
                    {
                        Caminata2();
                    }
                    else if (preanalisis.getLexema().Equals("Variable"))
                    {
                        Variable2();
                    }
                }
                catch
                {
                    Console.WriteLine("Error en varios pasos");
                }
            }
            else if (preanalisis.GetTipoEnString().Equals("Identificador"))
            {
                Asignacion2();
            }
        }

        private void Paso()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            Opciones3();
            match(Token.Tipo.COMA);
            Opciones3();
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
            G();
        }

        private void Caminata2()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            match(Token.Tipo.PARENTESISA);
            Opciones4();
            match(Token.Tipo.PARENTESISC);
            match(Token.Tipo.PUNTOCOMA);
            G();
        }

        private void Variable2()
        {
            //match(Token.Tipo.CORCHETEA);
            match(Token.Tipo.RESERVADA);
            match(Token.Tipo.CORCHETEC);
            match(Token.Tipo.DOSPUNTOS);
            I();
            match(Token.Tipo.PUNTOCOMA);
            G();
        }

        private void match(Token.Tipo t)
        {

           
            if (t != preanalisis.GetTipo())
            {
                num++;
                string descripion = "Se esperaba " + getTipoError(t);
                string lex = preanalisis.getLexema();
                int fila = preanalisis.GetFila();
                int col = preanalisis.GetCol();
                le.Add(new ErrorSintactico(num,lex,descripion,fila,col));

                //listaErrores += "No. "+ num +" "+"Se encontro "+"\""+preanalisis.getLexema()+"\""+ " Error Sintactico, se esperaba " + getTipoError(t) + " " +"Fila: "+preanalisis.GetFila() + " " +"Columna: " +preanalisis.GetCol()+"\r\n"+"------------------------------------------------------------ ";
                //Console.WriteLine("Se esperaba "+ getTipoError(t) + preanalisis.GetFila() + preanalisis.GetCol());
                numError2 +=1;
            }

            if (preanalisis.GetTipo() != Token.Tipo.ULTIMO)
            {
                numPreAnalisis += 1;
                preanalisis = listaTokens[numPreAnalisis];

            }
        }

        // mensaje de que se esperaba
        private string getTipoError(Token.Tipo t)
        {
            switch (t)
            {
                case Token.Tipo.COMA:
                    return "Coma";
                case Token.Tipo.CORCHETEA:
                    return "Corchete izquierdo";
                case Token.Tipo.CORCHETEC:
                    return "Corchete derecho";
                case Token.Tipo.DIVISION:
                    return "Signo division";
                case Token.Tipo.DOSPUNTOS:
                    return "Dos puntos";
                case Token.Tipo.ID:
                    return "Identificador";
                case Token.Tipo.IGUAL:
                    return "Signo igual";
                case Token.Tipo.LLAVEA:
                    return "LLave izquierda";
                case Token.Tipo.LLAVEC:
                    return "LLave derecha";
                case Token.Tipo.MAS:
                    return "Signo mas";
                case Token.Tipo.MENOS:
                    return "Signo menos";
                case Token.Tipo.NUMERO:
                    return "numero";
                case Token.Tipo.PARENTESISA:
                    return "Parentesis abierto";
                case Token.Tipo.PARENTESISC:
                    return "Parentesis derecho";
                case Token.Tipo.POR:
                    return "Signo por";
                case Token.Tipo.PUNTO:
                    return "punto";
                case Token.Tipo.PUNTOCOMA:
                    return "Punto y coma";
                case Token.Tipo.RESERVADA:
                    return "palabra reservada";
                default:
                    return "Desconocido";
            }
        }

        public void imprimir()
        {
            imprimirListaE(le);
        }

        public void imprimirListaE(List<ErrorSintactico> e)
        {

            tblError.Clear();
            tblError.Columns.Add("No", typeof(string));
            tblError.Columns.Add("Lexema", typeof(string));
            tblError.Columns.Add("Descripcion", typeof(string));
            tblError.Columns.Add("Fila", typeof(string));
            tblError.Columns.Add("Columna", typeof(string));

            foreach (ErrorSintactico t in e)
            {


                fila2 = tblError.NewRow();
                tblError.NewRow();
                fila2["No"] = num++;
                fila2["Lexema"] = t.Lexema;
                fila2["Descripcion"] = t.Tipo;
                fila2["Fila"] = t.Fila;
                fila2["Columna"] = t.Col;
                tblError.Rows.Add(fila2);
            }
            convertirhtml2(tblError);

        }

        public string convertirhtml2(DataTable dt)
        {
            string html = "<h1>Lista de  Errores Sintacticos</h1>";

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

    }// cierre clase
}
