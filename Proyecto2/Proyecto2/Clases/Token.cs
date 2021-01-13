using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class Token
    {

        // tipo de tokens

        public enum Tipo
        {
            CORCHETEA,
            CORCHETEC,
            DOSPUNTOS,
            PUNTOCOMA,
            COMA,
            PUNTO,
            LLAVEA,
            LLAVEC,
            PARENTESISA,
            PARENTESISC,
            MAS,
            MENOS,
            POR,
            DIVISION,
            NUMERO,
            IGUAL,
            RESERVADA,
            ID,
            OTRO,
            ERROR,
            ULTIMO
        }

        private Tipo tipotoken;
        private string valor;
        private int numero;
        private int fila;
        private int columna;

        public Token(Tipo tipo, int posInicial)
        {
            this.tipotoken = tipo;
            this.PosInicial = posInicial; 
        }

        public Token(Tipo tipo)
        {
            this.tipotoken = tipo;
        }

        public Token(Tipo tipo, string auxLex, int posInicial, int fila)
        {
            this.tipotoken = tipo;
            this.PosInicial = posInicial;
            this.valor = auxLex;
            this.fila = fila;
        }

        public Token(Tipo tipo, string auxLex, int posInicial, int fila, int columna, int num)
        {
            this.tipotoken = tipo;
            this.PosInicial = posInicial;
            this.valor = auxLex;
            this.fila = fila;
            this.columna = columna;
            this.numero = num;
        }

        public string getLexema()
        {
            return valor;
        }

        public int getnumero()
        {
            return numero;
        }

        public int PosInicial
        {
            get;
            set;
        }

        public int GetFila()
        {
            return fila;
        }

        public int GetCol()
        {
            return columna;
        }

        public string GetTipoEnString()
        {
            switch (tipotoken)
            {
                case Tipo.COMA:
                    {
                        return "Signo Coma";
                    }
                case Tipo.CORCHETEA:
                    {
                        return "Signo Corchete Izquierdo";
                    }
                case Tipo.CORCHETEC:
                    {
                        return "Signo Corchete Derecho";
                    }
                case Tipo.DIVISION:
                    {
                        return "Operador division";
                    }
                case Tipo.DOSPUNTOS:
                    {
                        return "Signo Dos Puntos";
                    }
                case Tipo.ERROR:
                    {
                        return "Error";
                    }
                case Tipo.ID:
                    {
                        return "Identificador";
                    }
                case Tipo.IGUAL:
                    {
                        return "Signo Igual";
                    }
                case Tipo.LLAVEA:
                    {
                        return "Signo Llave Izquierda";
                    }
                case Tipo.LLAVEC:
                    {
                        return "Signo Llave Derecha";
                    }
                case Tipo.MAS:
                    {
                        return "Operador Mas";
                    }
                case Tipo.MENOS:
                    {
                        return "Operador Menos";
                    }
                case Tipo.NUMERO:
                    {
                        return "Digito";
                    }
                case Tipo.OTRO:
                    {
                        return "Otro";
                    }
                case Tipo.PARENTESISA:
                    {
                        return "Signo Parentesis Izquierdo";
                    }
                case Tipo.PARENTESISC:
                    {
                        return "Signo Parentesis Derecho";
                    }
                case Tipo.POR:
                    {
                        return "Operador Multiplicacion";
                    }
                case Tipo.PUNTO:
                    {
                        return "Signo Punto";
                    }
                case Tipo.PUNTOCOMA:
                    {
                        return "Signo Punto y Coma";
                    }
                case Tipo.RESERVADA:
                    {
                        return "Palabra Reservada";
                    }
                default:
                    {
                        return "Desconocido";
                    }
            }
        }

        public Tipo GetTipo()
        {
            return tipotoken;
        }

    }// cierre clase
}
