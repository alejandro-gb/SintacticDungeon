using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2.Clases
{
    class Variable
    {

        int numero;
        string nombre;
        int valor;

        public Variable(int numero, string nombre, int valor)
        {
            this.Numero = numero;
            this.Nombre = nombre;
            this.Valor = valor;
        }

        public int Numero { get => numero; set => numero = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Valor { get => valor; set => valor = value; }
    }
}
