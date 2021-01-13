using Proyecto2.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2
{
    public partial class Principal : Form
    {

        //Variables utilizadas
        string archivo;
        private List<Token> ltokens= new List<Token>();
        private List<Token> error = new List<Token>();
        private List<Error> es = new List<Error>();
        public int posinicialx;
        public int posinicialy;
        public int intervalo;
        public int[,] pared;
        public int posfinalx;
        public int posfinaly;
        public List<Coordenada> lc;
        public List<Coordenada> lce;
        public List<Coordenada> lcp;
        public List<Enemigos> le;
        List<Variable> variables;
        List<Variable> variablesp;
        public int tx;
        public int ty;

        //constructor
        public Principal()
        {
            InitializeComponent();
        }

        //metodo load del form
        private void Principal_Load(object sender, EventArgs e)
        {

        }

        // abrir
        private void MSOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscar = new OpenFileDialog();
            //buscar.Filter = "Todos los archivos|*.dis";
            if (buscar.ShowDialog() == DialogResult.OK)
            {
                string ubicacion = buscar.FileName;
                archivo = ubicacion;    
                this.Text = buscar.FileName;
                string leer = File.ReadAllText(ubicacion);
                TextArea.Text = leer;
            }

        }

        // guardar
        private void MSSave_Click(object sender, EventArgs e)
        {
            string ubicacion = archivo;
            if (ubicacion != null)
            {
                File.WriteAllText(ubicacion, TextArea.Text);
            }
            else
            {
                abrirVentanaGuardar();
            }
        }

        //guardar como
        private void MSSaveAs_Click(object sender, EventArgs e)
        {
            abrirVentanaGuardar();
        }

        // metodo para guardar
        private void abrirVentanaGuardar()
        {
            SaveFileDialog guardar = new SaveFileDialog();
            //guardar.Filter = "Documento de texto|*.plst";
            guardar.Title = "Guardar";
            guardar.FileName = "Sin titulo";
            var resultado = guardar.ShowDialog();
            if (resultado == DialogResult.OK)
            {

                StreamWriter write = new StreamWriter(guardar.FileName);
                foreach (object line in TextArea.Lines)
                {
                    write.WriteLine(line);
                }
                write.Close();
                archivo = guardar.FileName;
            }
        }

        //analizar
        private void MSAnalizar_Click(object sender, EventArgs e)
        {
            pintar(@"\[", TextArea, Color.SkyBlue);
            pintar(@"\]", TextArea, Color.SkyBlue);
            pintar("(Principal|Intervalo|Duracion|Bloque_Nivel|Bloque_Enemigo|Bloque_Personaje|Nivel|Dimensiones|Inicio_personaje|Ubicacion_Salida|Pared|Casilla|Varias_Casillas|Enemigo|Caminata|Personaje|Paso|Variable)", TextArea, Color.Blue);
            pintar(":", TextArea, Color.Yellow);
            pintar(@"\{", TextArea, Color.Red);
            pintar(@"\}", TextArea, Color.Red);
            pintar(";", TextArea, Color.Purple);
            pintar(@"\(", TextArea, Color.Green);
            pintar(@"\)", TextArea, Color.Green);
            pintar("0|1|2|3|4|5|6|7|8|9", TextArea, Color.Turquoise);
            pintar("-", TextArea, Color.Fuchsia);

            string entrada = TextArea.Text;
            AnalizadorLexico aLex = new AnalizadorLexico();
            ltokens = aLex.Scanner(entrada);
            aLex.imprimirLista(ltokens);
            ltokens.Add(new Token(Token.Tipo.ULTIMO));
            if (aLex.numError == 0)
            {
                MessageBox.Show("El analisis lexico fue realizado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                AnalizadorLexico al = new AnalizadorLexico();
                error = al.Scanner(entrada);
                al.imprimirListaE(aLex.errors);
            }
            AnalizadorSintactico parser = new AnalizadorSintactico();
            parser.parser(ltokens);
            if (parser.numError2 == 0)
            {
                MessageBox.Show("El analisis Sintactico fue realizado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                parser.imprimir();
            }
            
            try
            {
                //lc.Clear();
                //le.Clear();
                //lcp.Clear();
                //variables.Clear();
                //variablesp.Clear();
                //lce.Clear();
                
                obtenerDatos();
            }
            catch
            {
                MessageBox.Show("Hubo un error al generar el juego", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //pintar 
        private void pintar(string pattern, RichTextBox rit, Color color)
        {
            MatchCollection resultados;

            Regex obj = new Regex(pattern, RegexOptions.IgnoreCase);
            resultados = obj.Matches(rit.Text);

            foreach (Match palabra in resultados)
            {
                rit.SelectionStart = palabra.Index;
                rit.SelectionLength = palabra.Length;
                rit.SelectionColor = color;
            }
        }

        //acerca de
        private void MSAbout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        // salir
        private void MSExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //tablero de juego
        private void tableroDeJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numenemigos > 3)
            {
                MessageBox.Show("No se pueden incluir mas de 3 enemigos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }else if (tx < 3 | tx > 20 | ty < 3 | ty > 15)
            {
                MessageBox.Show("Dimensiones fuera de rango", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Game game = new Game(posinicialx, posinicialy, intervalo, pared, posfinalx, posfinaly, lc, tx, ty, le,lcp);
                game.Show();
            }
           
        }

        int numenemigos = 0;
        //obtener datos
        public void obtenerDatos()
        {
            lc = new List<Coordenada>();
            le = new List<Enemigos>();
            lcp = new List<Coordenada>();
            variablesp = new List<Variable>();
            variables = new List<Variable>();

            int tempx=0;
            int tempy=0;
            int aux=0;
            int aux2=0;
            int aux3=0;
            int aux4=0;
          
            //recorrer la lista
            for (int i = 1 ; i<ltokens.Count-1 ; i++)
            {
                Token values = ltokens[i];
                Token val = ltokens[i - 1];
                Token a;
                Token b;
                Token c;
                Token d;
                

                // obtener intervalo
                if (values.getLexema().Equals("Intervalo") && val.getLexema().Equals("["))
                {
                    a = ltokens[i+4];
                    intervalo = int.Parse(a.getLexema());
                }

                //obtner dimensiones
                if (values.getLexema().Equals("Dimensiones") && val.getLexema().Equals("["))
                {
                    a = ltokens[i + 4];
                    tx = int.Parse(a.getLexema());
                    a = ltokens[i+6];
                    ty = int.Parse(a.getLexema());

                    pared = new int[ty,tx];

                    for (int x = 0 ; x <= ty - 1; x++)
                    {
                        for (int y = 0; y <= tx - 1; y++)
                        {
                            pared[x, y] = 0;
                        }
                    }
                }

                if (values.getLexema().Equals("Pared") && val.getLexema().Equals("["))
                {
                    int numv = 0;
                    string nombre;
                    int valor = 0;

                    //separar el bloque de la lista
                    int e = values.getnumero();
                    int r = e;
                    int temporal = ltokens.Count - 1;
                    for (int v = r; v <= temporal; v++)
                    {
                        Token n = ltokens[v];
                        Token m = ltokens[v - 1];
                        if (n.getLexema().Equals("}") && m.getLexema().Equals(";"))
                        {
                            temporal = n.getnumero();
                        }
                    }

                    for (int g = e; g <= temporal; g++)
                    {
                        Token k = ltokens[g];
                        Token l = ltokens[g - 1];

                        //obtener variables

                        if (k.getLexema().Equals("Variable") && l.getLexema().Equals("["))
                        {
                            int x = k.getnumero();
                            int y = x;
                            int temp = ltokens.Count - 1;
                            for (int w = y;w <= temp; w++)
                            {
                                Token k1 = ltokens[w];
                                Token k2 = ltokens[w-1];
                                if (k1.getLexema().Equals(";"))
                                {
                                    temp = k1.getnumero();
                                }
                            }
                            //Console.WriteLine("el valor donde termina el bloque variable es " + temp);
                            numv++;
                            nombre = ltokens[g + 3].getLexema();
                            variablesp.Add(new Variable(numv, nombre, valor));
                            if (ltokens[g + 4].getLexema().Equals(","))
                            {
                                numv++;
                                nombre = ltokens[g + 5].getLexema();
                                variablesp.Add(new Variable(numv, nombre, valor));
                                if (ltokens[g + 6].getLexema().Equals(","))
                                {
                                    numv++;
                                    nombre = ltokens[g + 7].getLexema();
                                    variablesp.Add(new Variable(numv, nombre, valor));
                                    if (ltokens[g + 8].getLexema().Equals(","))
                                    {
                                        numv++;
                                        nombre = ltokens[g + 9].getLexema();
                                        variablesp.Add(new Variable(numv, nombre, valor));
                                        if (ltokens[g + 10].getLexema().Equals(","))
                                        {
                                            numv++;
                                            nombre = ltokens[g + 11].getLexema();
                                            variablesp.Add(new Variable(numv, nombre, valor));
                                            if (ltokens[g + 12].getLexema().Equals(","))
                                            {
                                                numv++;
                                                nombre = ltokens[g + 13].getLexema();
                                                variablesp.Add(new Variable(numv, nombre, valor));
                                            }
                                        }

                                    }

                                }


                            }
                        }

                        //asignar varibles
                        int valortemp2 = 0;
                        int valorreal2 = 0;
                        Variable variable = variablesp.FirstOrDefault(x => x.Nombre.Equals(k.getLexema()));
                        if (variable != null)
                        {
                            if (ltokens[g + 1].getLexema().Equals(":") && ltokens[g + 2].getLexema().Equals("="))
                            {
                                if (ltokens[g+4].getLexema().Equals(";"))
                                {
                                    string test = ltokens[g + 3].getLexema();
                                    try
                                    {
                                        variable.Valor = int.Parse(test);
                                    }
                                    catch
                                    {
                                        Variable variable2 = variablesp.FirstOrDefault(x => x.Nombre.Equals(test));
                                        if (variable2 != null)
                                        {
                                            variable.Valor = variable2.Valor;
                                        }
                                    }
                                }
                                else
                                {
                                    string test2 = ltokens[g + 3].getLexema();
                                    string operador = ltokens[g + 4].getLexema();
                                    string test3 = ltokens[g + 5].getLexema();
                                    try
                                    {
                                        valorreal2 = int.Parse(test2);
                                    }
                                    catch
                                    {
                                        Variable variable3 = variablesp.FirstOrDefault(x => x.Nombre.Equals(test2));
                                        if (variable3 != null)
                                        {
                                            valorreal2 = variable3.Valor;
                                        }
                                    }
                                    try
                                    {
                                        valortemp2 = int.Parse(test3);
                                    }
                                    catch
                                    {
                                        Variable variable3 = variablesp.FirstOrDefault(x => x.Nombre.Equals(test3));
                                        if (variable3 != null)
                                        {
                                            valortemp2 = variable3.Valor;
                                        }
                                    }
                                    if (operador.Equals("+"))
                                    {
                                        variable.Valor = valorreal2 + valortemp2;
                                    }
                                    if (operador.Equals("-"))
                                    {
                                        variable.Valor = valorreal2 - valortemp2;
                                    }
                                    if (operador.Equals("/"))
                                    {
                                        variable.Valor = valorreal2 / valortemp2;
                                    }
                                    if (operador.Equals("*"))
                                    {
                                        variable.Valor = valorreal2 * valortemp2;
                                    }
                                }

                            }
                        }

                        //obtener casillas de pared
                        if (k.getLexema().Equals("Casilla") && l.getLexema().Equals("["))
                        {
                            a = ltokens[g + 4];
                            try
                            {
                                tempx = int.Parse(a.getLexema());
                            }
                            catch
                            {
                                Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(a.getLexema()));
                                if (v1 != null)
                                {
                                    tempx = v1.Valor;
                                }
                            }
                            b = ltokens[g + 6];
                            try
                            {
                                tempy = int.Parse(b.getLexema());
                            }
                            catch
                            {
                                Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(b.getLexema()));
                                if (v1 != null)
                                {
                                    tempy = v1.Valor;
                                }
                            }
                            pared[tempy, tempx] = 1;
                            lcp.Add(new Coordenada(tempx, tempy));
                        }

                        // colocar varias casillas a la vez
                        if (k.getLexema().Equals("Varias_Casillas") && l.getLexema().Equals("["))
                        {
                            //primer numero
                            a = ltokens[g + 4];

                            // si la x es fija y lo que sigue es coma entonces y es variable
                            if (ltokens[g + 5].getLexema().Equals(","))
                            {
                                try
                                {
                                    tempx = int.Parse(a.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(a.getLexema()));
                                    if (v1 != null)
                                    {
                                        tempx = v1.Valor;
                                    }
                                }
                                //segundo numero
                                b = ltokens[g + 6];
                                try
                                {
                                    aux = int.Parse(b.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(b.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux = v1.Valor;
                                    }
                                }
                                //tercer numero
                                c = ltokens[g + 9];
                                try
                                {
                                    aux2 = int.Parse(c.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(c.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux2 = v1.Valor;
                                    }
                                }
                                //rellenar las filas
                                if (aux <= aux2)
                                {
                                    for (int y = aux; y <= aux2; y++)
                                    {
                                        tempy = y;
                                        pared[tempy, tempx] = 1;
                                        lcp.Add(new Coordenada(tempx, tempy));
                                    }
                                }
                                if (aux >= aux2)
                                {
                                    for (int y = aux; y >= aux2; y--)
                                    {
                                        tempy = y;
                                        pared[tempy, tempx] = 1;
                                        lcp.Add(new Coordenada(tempx, tempy));
                                    }
                                }
                            }

                            //si no viene coma entonces la x varia
                            else
                            {
                                //primer numero
                                try
                                {
                                    aux = int.Parse(a.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(a.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux = v1.Valor;
                                    }
                                }
                                    //segundo numero
                                b = ltokens[g + 7];
                                try
                                {
                                    aux2 = int.Parse(b.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(b.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux2 = v1.Valor;
                                    }
                                }
                                // tomar y como fijo y tercer numero
                                c = ltokens[g + 9];
                                if (ltokens[g + 10].getLexema().Equals("."))
                                {
                                    try
                                    {
                                        aux3 = int.Parse(c.getLexema());
                                    }
                                    catch
                                    {
                                        Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(c.getLexema()));
                                        if (v1 != null)
                                        {
                                            aux3 = v1.Valor;
                                        }
                                    }
                                    //cuarto numero
                                    d = ltokens[g + 12];
                                    try
                                    {
                                        aux4 = int.Parse(d.getLexema());
                                    }
                                    catch
                                    {
                                        Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(d.getLexema()));
                                        if (v1 != null)
                                        {
                                            aux4 = v1.Valor;
                                        }
                                    }
                                    if (aux <= aux2 && aux3 <= aux4)
                                    {
                                        for (int g3 = 0; g3 <= aux2; g3++)
                                        {
                                            tempx = g3;
                                            for (int h = 0; h <= aux4; h++)
                                            {
                                                tempy = h;
                                                pared[tempy, tempx] = 1;
                                                lcp.Add(new Coordenada(tempx, tempy));
                                            }
                                        }
                                    }
                                    if (aux >= aux2 && aux3 >= aux4)
                                    {
                                        for (int g1 = aux; g1 >= aux2; g1--)
                                        {
                                            tempx = g1;
                                            for (int h = aux3; h >= aux4; h--)
                                            {
                                                tempy = h;
                                                pared[tempy, tempx] = 1;
                                                lcp.Add(new Coordenada(tempx, tempy));
                                            }
                                        }
                                    }
                                    if (aux >= aux2 && aux3 <= aux4)
                                    {
                                        for (int g2 = aux; g2 >= aux2; g2--)
                                        {
                                            tempx = g2;
                                            for (int h = 0; h <= aux4; h++)
                                            {
                                                tempy = h;
                                                pared[tempy, tempx] = 1;
                                                lcp.Add(new Coordenada(tempx, tempy));
                                            }
                                        }
                                    }
                                    if (aux <= aux2 && aux3 >= aux4)
                                    {
                                        for (int g3 = 0; g3 <= aux2; g3++)
                                        {
                                            tempx = g3;
                                            for (int h = aux3; h >= aux4; h--)
                                            {
                                                tempy = h;
                                                pared[tempy, tempx] = 1;
                                                lcp.Add(new Coordenada(tempx, tempy));
                                            }
                                        }
                                    }

                                }
                                try
                                {
                                    tempy = int.Parse(c.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variablesp.FirstOrDefault(x => x.Nombre.Equals(c.getLexema()));
                                    if (v1 != null)
                                    {
                                        tempy = v1.Valor;
                                    }
                                }
                                //llenar las columnas
                                if (aux <= aux2)
                                {
                                    for (int z = aux; z <= aux2; z++)
                                    {
                                        tempx = z;
                                        pared[tempy, tempx] = 1;
                                        lcp.Add(new Coordenada(tempx, tempy));

                                    }
                                }
                                if (aux >= aux2)
                                {
                                    for (int z = aux; z >= aux2; z--)
                                    {
                                        tempx = z;
                                        pared[tempy, tempx] = 1;
                                        lcp.Add(new Coordenada(tempx, tempy));
                                    }
                                }
                            }
                        }//bloque varias casillas
                    }//recorrer bloque pared

                }//bloque pared

                //obtener inicio del personaje
                if (values.getLexema().Equals("Inicio_personaje") && val.getLexema().Equals("["))
                {
                    a = ltokens[i+4];
                    posinicialx = int.Parse(a.getLexema());
                    a = ltokens[i + 6];
                    posinicialy = int.Parse(a.getLexema());
                }

                //obtener ubicacion de la salida
                if (values.getLexema().Equals("Ubicacion_Salida") && val.getLexema().Equals("["))
                {
                    a = ltokens[i + 4];
                    posfinalx = int.Parse(a.getLexema());
                    a = ltokens[i + 6];
                    posfinaly = int.Parse(a.getLexema());
                }

                //obtener datos personaje
                if (values.getLexema().Equals("Personaje") && val.getLexema().Equals("["))
                {
                    int numv = 0;
                    string nombre;
                    int valor = 0;
                    //inicio del bloque
                    int q = values.getnumero();
                    int p = q;
                    int f = 0;
                    int temporal2 = ltokens.Count - 1;
                    //encontrar final del bloque
                    for (int j = p; p <= temporal2; p++)
                    {
                        
                        Token z = ltokens[p];
                        Token x = ltokens[p - 1];

                        if (z.getLexema().Equals("}") && x.getLexema().Equals(";"))
                        {
                            temporal2 = z.getnumero();
                            f = z.getnumero();
                        }
                        
                    }

                   //buscar datos en el bloque
                    for (int h = q; h<=f;h++)
                    {
                        Token y = ltokens[h];
                        Token w = ltokens[h-1];

                        //encontrar variables
                        if (y.getLexema().Equals("Variable") && w.getLexema().Equals("["))
                        {
                            numv++;
                            nombre = ltokens[h + 3].getLexema();
                            variables.Add(new Variable(numv,nombre,valor));
                            if (ltokens[h+4].getLexema().Equals(","))
                            {
                                numv++;
                                nombre = ltokens[h + 5].getLexema();
                                variables.Add(new Variable(numv,nombre,valor));
                               
                            }
                            if (ltokens[h + 6].getLexema().Equals(","))
                            {
                                numv++;
                                nombre = ltokens[h + 7].getLexema();
                                variables.Add(new Variable(numv, nombre, valor));
                               
                            }
                            if (ltokens[h + 8].getLexema().Equals(","))
                            {
                                numv++;
                                nombre = ltokens[h + 9].getLexema();
                                variables.Add(new Variable(numv, nombre, valor));
                            }
                            if (ltokens[h + 10].getLexema().Equals(","))
                            {
                                numv++;
                                nombre = ltokens[h + 11].getLexema();
                                variables.Add(new Variable(numv, nombre, valor));
                            }
                            if (ltokens[h + 12].getLexema().Equals(","))
                            {
                                numv++;
                                nombre = ltokens[h + 13].getLexema();
                                variables.Add(new Variable(numv, nombre, valor));
                            }
                        }
                        int valortemp = 0;
                        int valorreal = 0;
                        //asignar variables
                        Variable variable = variables.FirstOrDefault(x=> x.Nombre.Equals(y.getLexema()));
                        if (variable != null)
                        {
                            if (ltokens[h+1].getLexema().Equals(":") && ltokens[h+2].getLexema().Equals("="))
                            {
                                if (ltokens[h+4].getLexema().Equals(";"))
                                {
                                    string test = ltokens[h + 3].getLexema();
                                    try
                                    {
                                        variable.Valor = int.Parse(test);
                                    }
                                    catch
                                    {
                                        Variable variable2 = variables.FirstOrDefault(x => x.Nombre.Equals(test));
                                        if (variable2 != null)
                                        {
                                           variable.Valor = variable2.Valor;
                                        }
                                    }
                                }
                                else
                                {
                                    string test2 = ltokens[h + 3].getLexema();
                                    string operador = ltokens[h + 4].getLexema();
                                    string test3 = ltokens[h + 5].getLexema();
                                    try
                                    {
                                        valorreal = int.Parse(test2);
                                    }
                                    catch
                                    {
                                        Variable variable3 = variables.FirstOrDefault(x => x.Nombre.Equals(test2));
                                        if (variable3 != null)
                                        {
                                            valorreal = variable3.Valor;
                                        }
                                    }
                                    try
                                    {
                                        valortemp = int.Parse(test3);
                                    }
                                    catch
                                    {
                                        Variable variable3 = variables.FirstOrDefault(x => x.Nombre.Equals(test3));
                                        if (variable3 != null)
                                        {
                                            valortemp = variable3.Valor;
                                        }
                                    }
                                    if (operador.Equals("+"))
                                    {
                                        variable.Valor = valorreal + valortemp;
                                    }
                                    if (operador.Equals("-"))
                                    {
                                        variable.Valor = valorreal - valortemp;
                                    }
                                    if (operador.Equals("/"))
                                    {
                                        variable.Valor = valorreal / valortemp;
                                    }
                                    if (operador.Equals("*"))
                                    {
                                        variable.Valor = valorreal * valortemp;
                                    }

                                }
                                
                                    


                                
                            }
                         
                           
                        }

                        //encontrar caminata
                        if (y.getLexema().Equals("Caminata") && w.getLexema().Equals("["))
                        {
                            //primer numero
                a = ltokens[h + 4];
                           // si le sigue una coma es porque la x es fija y y cambia
                if (ltokens[h + 5].getLexema().Equals(","))
                            {

                                //tomar el valor de x
                                try
                                {
                                    tempx = int.Parse(a.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variables.FirstOrDefault(x => x.Nombre.Equals(a.getLexema()));
                                    if (v1 != null)
                                    {
                                        tempx = v1.Valor;
                                    }
                                }
                                

                               // tomar el segundo numero
                    b = ltokens[h + 6];
                                try
                                {
                                    aux = int.Parse(b.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variables.FirstOrDefault(x => x.Nombre.Equals(b.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux = v1.Valor;
                                    }
                                }

                               // tomar el siguiente numero
                    c = ltokens[h + 9];
                                try
                                {
                                    aux2 = int.Parse(c.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variables.FirstOrDefault(x => x.Nombre.Equals(c.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux2 = v1.Valor;
                                    }
                                }

                                if (aux <= aux2)
                                {
                                    for (int e = aux; e <= aux2; e++)
                                    {
                                        tempy = e;
                                        lc.Add(new Coordenada(tempx, tempy));
                                    }
                                }
                                if (aux >= aux2)
                                {
                                    for (int r = aux; r >= aux2; r--)
                                    {
                                        tempy = r;
                                        lc.Add(new Coordenada(tempx, tempy));
                                    }
                                }

                            }



                            //si no viene coma es porque la y es fija
                else
                {
                                try
                                {
                                    aux = int.Parse(a.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variables.FirstOrDefault(x => x.Nombre.Equals(a.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux = v1.Valor;
                                    }
                                }
                                b = ltokens[h + 7];
                                try
                                {
                                    aux2 = int.Parse(b.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variables.FirstOrDefault(x => x.Nombre.Equals(b.getLexema()));
                                    if (v1 != null)
                                    {
                                        aux2 = v1.Valor;
                                    }
                                }
                               // y fija
                    c = ltokens[h + 9];
                                try
                                {
                                    tempy = int.Parse(c.getLexema());
                                }
                                catch
                                {
                                    Variable v1 = variables.FirstOrDefault(x => x.Nombre.Equals(c.getLexema()));
                                    if (v1 != null)
                                    {
                                        tempy = v1.Valor;
                                    }
                                }
                                if (aux <= aux2)
                                {
                                    for (int t = aux; t <= aux2; t++)
                                    {
                                        tempx = t;
                                        lc.Add(new Coordenada(tempx, tempy));
                                    }
                                }
                                if (aux >= aux2)
                                {
                                    for (int u = aux; u >= aux2; u--)
                                    {
                                        tempx = u;
                                        lc.Add(new Coordenada(tempx, tempy));
                                    }
                                }

                            }
                        }

                        //encontrar pasos
                        if (y.getLexema().Equals("Paso") && w.getLexema().Equals("["))
                        {
                            a = ltokens[h + 4];
                            try
                            {
                                tempx = int.Parse(a.getLexema());
                            }
                            catch
                            {
                                Variable v1 = variables.FirstOrDefault(x => x.Nombre.Equals(a.getLexema()));
                                if (v1 != null)
                                {
                                    tempx = v1.Valor;
                                }
                            }
                            a = ltokens[h + 6];
                            try
                            {
                                tempy = int.Parse(a.getLexema());
                            }
                            catch
                            {
                                Variable v2 = variables.FirstOrDefault(x => x.Nombre.Equals(a.getLexema()));
                                if (v2 != null)
                                {
                                    tempy = v2.Valor;
                                }
                            }
                            
                            lc.Add(new Coordenada(tempx, tempy));
                        }

                    }
                }

                //obtener caminatas de enemigos
                if (values.getLexema().Equals("Enemigo") && val.getLexema().Equals("["))
                {
                    numenemigos++;
                    lce = new List<Coordenada>();

                    a = ltokens[i+9];
                    aux3 = int.Parse(a.getLexema());
                    if (ltokens[i + 10].getLexema().Equals(","))
                    {
                        b = ltokens[i+11];
                        aux4 = int.Parse(b.getLexema());
                    }
                    else
                    {
                        b = ltokens[i + 14];
                        aux4 = int.Parse(b.getLexema());
                    }
                   


                    int e = values.getnumero();
                    int r = e;
                    int t = 0;
                    int temporal = ltokens.Count - 1;
                    for (int v = r; v <= temporal ; v++ )
                    {
                        Token n = ltokens[v];
                        Token m = ltokens[v - 1];
                        if (n.getLexema().Equals("}") && m.getLexema().Equals(";"))
                        {
                            temporal = n.getnumero();
                        }
                    }
                    for (int g = e; g <= temporal; g++)
                    {
                        Token k = ltokens[g];
                        Token l = ltokens[g-1];

                        if (k.getLexema().Equals("Caminata") && l.getLexema().Equals("["))
                        {
                            //primer numero
                            a = ltokens[g + 4];

                            // si le sigue una coma es porque la x es fija y y cambia
                            if (ltokens[g + 5].getLexema().Equals(","))
                            {

                                //tomar el valor de x
                                tempx = int.Parse(a.getLexema());

                                // tomar el segundo numero
                                b = ltokens[g + 6];
                                aux = int.Parse(b.getLexema());

                                // tomar el siguiente numero
                                c = ltokens[g + 9];
                                aux2 = int.Parse(c.getLexema());

                                if (aux <= aux2)
                                {
                                    for (int ex = aux; ex <= aux2; ex++)
                                    {
                                        tempy = ex;
                                        lce.Add(new Coordenada(tempx, tempy));
                                    }
                                }
                                if (aux >= aux2)
                                {
                                    for (int s = aux; s >= aux2; s--)
                                    {
                                        tempy = s;
                                        lce.Add(new Coordenada(tempx, tempy));
                                    }
                                }

                            }
                            //si no viene coma es porque la y es fija
                            else
                            {
                                aux = int.Parse(a.getLexema());
                                b = ltokens[g + 7];
                                aux2 = int.Parse(b.getLexema());
                                // y fija
                                c = ltokens[g + 9];
                                tempy = int.Parse(c.getLexema());
                                if (aux <= aux2)
                                {
                                    for (int f = aux; f <= aux2; f++)
                                    {
                                        tempx = f;
                                        lce.Add(new Coordenada(tempx, tempy));
                                    }
                                }
                                if (aux >= aux2)
                                {
                                    for (int u = aux; u >= aux2; u--)
                                    {
                                        tempx = u;
                                        lce.Add(new Coordenada(tempx, tempy));
                                    }
                                }

                            }
                        }

                    }

                    le.Add(new Enemigos(aux3,aux4,lce));

                }
            }

            foreach (Variable va in variables)
            {
                Console.WriteLine("varible personje > nombre "+ va.Nombre+" valor "+ va.Valor);
            }
            foreach (Variable va in variablesp)
            {
                Console.WriteLine("varible pared > nombre " + va.Nombre + " valor " + va.Valor);
            }

        }//cierre obtener

        
    }// cierre clase
}
