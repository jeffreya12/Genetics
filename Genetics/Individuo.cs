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

        public Individuo(int[] posiciones)
        {
            this.posiciones = posiciones;
            calcularFitness();
        }

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
            int dx, dy;
            int choques = 0;
            choques += Math.Abs(posiciones.Length - posiciones.Distinct().Count());

            for (int i = 0; i < posiciones.Length; i++)
            {
                for (int j = 0; j < posiciones.Length; j++)
                {
                    if (i != j)
                    {
                        dx = Math.Abs(i - j);
                        dy = Math.Abs(posiciones[i] - posiciones[j]);
                        if (dx == dy)
                        {
                            choques += 1;
                        }
                    }
                }
            }
            fitness = choques;
        }

        public void mutar()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            int randomColumna = rnd.Next(0, posiciones.Length);
            int randomFila = rnd.Next(1, posiciones.Length + 1);

            posiciones[randomColumna] = randomFila;
        }


        public int[] getPosiciones()
        {
            return (int[])posiciones.Clone();
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
