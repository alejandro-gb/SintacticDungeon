using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2.Clases
{
    public class ErrorSintactico
    {

        int num;
        string lexema;
        string tipo;
        int fila;
        int col;

        public ErrorSintactico(int num, string lexema, string tipo, int fila, int col)
        {
            this.Num = num;
            this.Lexema = lexema;
            this.Tipo = tipo;
            this.Fila = fila;
            this.Col = col;
        }

        public int Num { get => num; set => num = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Col { get => col; set => col = value; }
    }// cierre clase
}
