using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
    class Individuo
    {
        private int[] posiciones;
        private int fitness;

        public Individuo(int n)
        {
            posiciones = new int[n];
            generarIndividuo();
            calcularFitness();
        }

        private void generarIndividuo()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < posiciones.Length; i++)
            {
                posiciones[i] = rnd.Next(1, posiciones.Length + 1);
            }
        }

        private void calcularFitness()
        {
            fitness = posiciones.Distinct().Count();
        }

        public int[] getPosiciones()
        {
            return posiciones;
        }

        public int getFitness()
        {
            return fitness;
        }

        override public String ToString()
        {
            String salida = "Posiciones: ";
            for(int i = 0; i < posiciones.Length; i++)
            {
                salida = salida + posiciones[i].ToString() + " ";
            }
            salida = salida + "\nFitness: " + fitness.ToString() + "\n\n";
            return salida;
        }
    }
}
