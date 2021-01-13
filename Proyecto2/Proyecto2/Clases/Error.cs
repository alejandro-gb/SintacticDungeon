using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class Error
    {
        private string valor;
        private string esperado;
        private int fila;
        private int columna;
        
        public Error(string c,string e, int fila, int columna)
        {
            this.valor = c;
            this.esperado = e;
            this.fila = fila;
            this.columna = columna;
        }

        public Error(string c, int fila)
        {
            this.valor = c;
            this.fila = fila;
        }

        public string getEsperado()
        {
            return esperado;
        }
        public string GetValor()
        {
            return valor;
        }

        public int GetFila()
        {
            return fila;
        }

        public int GetColumna()
        {
            return columna;
        }

    }// cierre clase
}
