using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2.Clases
{
    public class Enemigos
    {

        int x;
        int y;
        List<Coordenada> cordenadas;

        public Enemigos(int x, int y, List<Coordenada> cor)
        {
            this.X = x;
            this.Y = y;
            this.Cordenadas = cor;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public List<Coordenada> Cordenadas { get => cordenadas; set => cordenadas = value; }
    }
}
