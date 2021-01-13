using Proyecto2.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2
{
    public partial class Game : Form
    {
        
        public int posInicialX { get; private set; }
        public int posInicialY { get; private set; }
        public int intervalo { get; private set; }
        public int[,] pared { get; private set; }
        public int posFinalX { get; private set; }
        public int posFinalY { get; private set; }
        public List<Coordenada> listaCordenadas { get; set; }
        public List<Enemigos> listaEnemigos{ get; set; }

        public int tamx;
        public int tamy;
        public List<Coordenada> listapared { get; set; }
       
        //Constructor
        public Game(int pix, int piy, int interval, int[,] wall, int pfx, int pfy, List<Coordenada> lc,int tx, int ty,List<Enemigos> le,List<Coordenada> lcp)
        {
            
            this.posInicialX = pix;
            this.posInicialY = piy;
            this.intervalo = interval;
            this.pared = wall;
            this.posFinalX = pfx;
            this.posFinalY = pfy;
            this.listaCordenadas = lc;
            this.listaEnemigos = le;
            this.listapared = lcp;
            InitializeComponent();
            this.Size = new Size(tx*50+18,ty*50+200);    
        }

        // pintar el form
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            // Cada vez que llamemos al método refresh del Form, se ejecutará este método, que es el del evento paint
            // Definimos un objeto Graphics que se usará para dibujar sobre el formulario
            Graphics g = e.Graphics;

            // Dibujamos el fondo
            //Bitmap imagen = new Bitmap("fondo.png");
            //g.DrawImage(imagen, 0, 0);

            // Dibujamos la casilla según el lugar en el que esté
            Bitmap imagen = new Bitmap("guerrero.png");
            g.DrawImage(imagen, 50 * posInicialX, 50 * posInicialY); // Lo multiplico por 50 porque la imagen casilla es de 50*50


            // Dibujamos los obstaculos
            imagen = new Bitmap("pared.jpg");
            for (var i = 0; i <= pared.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= pared.GetUpperBound(1); j++)
                {
                    if ((pared[i, j] == 1))
                        g.DrawImage(imagen, 50 * j, 50 * i);// Lo multiplico por 50 porque la imagen obstaculo es de 50*50
                }
            }

            //imagen = new Bitmap("enemigo.png");


            //dibujo la meta
            imagen = new Bitmap("Corona.png");
            g.DrawImage(imagen,50* posFinalX, 50* posFinalY);

            foreach (Enemigos enemigo in listaEnemigos)
            {
                imagen = new Bitmap("enemigo.png");
                g.DrawImage(imagen, 50*enemigo.X,50*enemigo.Y);
            }

        }

        //animar
        private void animacion(List<Coordenada> lista)
        {
            foreach (var c in lista)
            {
                // Actualizamos la posición en x segun la coordenada del elemento de la lista
                posInicialX = c.x;
                // Actualizamos la posición en y segun la coordenada del elemento de la lista
                posInicialY = c.y;

                //choques

                foreach (Coordenada cor in listapared)
                {
                    int x = cor.x;
                    int y = cor.y;
                    if (posInicialX == x && posInicialY == y)
                    {
                        MessageBox.Show("GAME OVER", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }



                // Esperamos 200 milisegundos
                Thread.Sleep(intervalo);
                // Refrescamos el formulario para que se repinte el fondo y la casilla en sus nuevas coordenadas
                this.Refresh();
            }

        }

        // evento del boton
        private void btnStart_Click(object sender, EventArgs e)
        {
            List<Coordenada> lista = new List<Coordenada>();
            List<Coordenada> lista2 = new List<Coordenada>();

            foreach (Coordenada c in listaCordenadas)
            {
                lista.Add(new Coordenada(c.x,c.y));
                Console.WriteLine("personaje cordenada x,y "+c.x+","+c.y);

                foreach (Enemigos ene in listaEnemigos)
                {
                    //animacion2(ene, ene.Cordenadas);
                   
                        animacion2(ene,ene.Cordenadas);
                    
                }
                //animar esta
                animacion(c);
                //animacion(lista);

            }
            



            //ganar o perder
            if (posInicialX == posFinalX && posInicialY == posFinalY)
            {
                MessageBox.Show("YOU WIN¡", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("GAME OVER", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }



        private void animacion(Coordenada c)
        {
            posInicialX = c.x;
            // Actualizamos la posición en y segun la coordenada del elemento de la lista
            posInicialY = c.y;

            //choques

            foreach (Coordenada cor in listapared)
            {
                int x = cor.x;
                int y = cor.y;
                if (posInicialX == x && posInicialY == y)
                {
                    MessageBox.Show("GAME OVER", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }



            // Esperamos 200 milisegundos
            Thread.Sleep(intervalo);
            // Refrescamos el formulario para que se repinte el fondo y la casilla en sus nuevas coordenadas
            this.Refresh();
        }

        public void animacion2(Enemigos ene,List<Coordenada> cor)
        {
            foreach (var c in cor)
            {
                // Actualizamos la posición en x segun la coordenada del elemento de la lista
                ene.X = c.x;
                // Actualizamos la posición en y segun la coordenada del elemento de la lista
                ene.Y = c.y;

                
                //choques
                foreach (Coordenada cord in cor)
                {
                    if (posInicialX == cord.x && posInicialY == cord.y)
                    {
                        MessageBox.Show("GAME OVER", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                // Esperamos 200 milisegundos
                Thread.Sleep(intervalo);
                // Refrescamos el formulario para que se repinte el fondo y la casilla en sus nuevas coordenadas
                this.Refresh();
            }

        }
        //load del form
        private void Game_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }// cierre clase
}
