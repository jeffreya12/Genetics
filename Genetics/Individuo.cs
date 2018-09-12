using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
    class Individuo
    {
        private int[] posiciones;               //Arreglo de posiciones en el tablero
        private int fitness;

        public Individuo(int[] posiciones)      //Gererar individuo a partir de un arreglo
        {
            this.posiciones = posiciones;
            calcularFitness();
        }

        public Individuo(int n)                 //Gererar individuo a partir de una tamaño
        {
            posiciones = new int[n];
            generarIndividuo();
            calcularFitness();
        }

        private void generarIndividuo()         //Gererar individuo aleatorio
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < posiciones.Length; i++)
            {
                posiciones[i] = rnd.Next(1, posiciones.Length + 1);
            }
        }

        private void calcularFitness()      //Calcula el fitness de un indivuo
                                            //El mejor fitness es 0
        {
            int dx, dy;
            int choques = 0;

            //Cuenta la cantidad de elementos repetidos en el array como choques
            choques += Math.Abs(posiciones.Length - posiciones.Distinct().Count());

            for (int i = 0; i < posiciones.Length; i++)
            {
                for (int j = 0; j < posiciones.Length; j++)
                {
                    if (i != j)
                    {
                        //Calcula su hay un choque diagonal
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

            //Cambia aleatoriamente un valor en el arreglo
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

        public void printTablero()
        {
            string tablero = "";
            for (int i = posiciones.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < posiciones.Length; j++)
                {
                    if (posiciones[j] == i + 1)
                    {
                        tablero += "O\t";
                    }
                    else
                    {
                        tablero += "X\t";
                    }
                }
                tablero += "\n\n";
            }

            Console.Out.Write(tablero);
        }

        override public String ToString()
        {
            String salida = "\nPosiciones: ";
            for(int i = 0; i < posiciones.Length; i++)
            {
                salida = salida + posiciones[i].ToString() + " ";
            }
            salida = salida + "\nFitness: " + fitness.ToString() + "\n\n";
            return salida;
        }
    }
}
