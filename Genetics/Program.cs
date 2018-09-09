using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
    class Program
    {

        static int n = 8;
        static Individuo[] poblacion = new Individuo[n];
        static Boolean found = false;

        static void generarPoblacion()
        {
            for (int i = 0; i < n; i++)
            {
                poblacion[i] = new Individuo(n);
                if (poblacion[i].getFitness() == 0)
                {
                    found = true;
                }
            }
        }

        static void Main(string[] args)
        {
            while (!found)
            {
                generarPoblacion();
            }
        }
    }
}
