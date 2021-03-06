﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
    class NReinas
    {
        static int n;
        static int tamanioPoblacion;
        static int tasaMutacion;
        static Individuo[] poblacion;

        static void Main(string[] args)
        {
            Console.WriteLine("Cantidad de reinas: ");
            n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nTamaño de la población: ");
            tamanioPoblacion = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nTasa de mutación: ");
            tasaMutacion = Convert.ToInt32(Console.ReadLine());

            poblacion = new Individuo[tamanioPoblacion];

            Individuo solucion = genetic();

            Console.WriteLine("\nSolución: \n");

            solucion.printTablero();
            Console.Out.WriteLine(solucion.ToString());
            
            Console.ReadLine();

        }

        public static void crearPoblacion()         //Llena el arreglo de población con individuos aleatorios
        {
            for (int i = 0; i < tamanioPoblacion; i++)
            {
                poblacion[i] = new Individuo(n);
            }
        }

        public static Individuo cruzar(Individuo padreX, Individuo padreY)
        {
            Individuo nuevoIndividuo;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            int puntoDeCruce = rnd.Next(0, n - 1); //Posición aleatoria en que se van a cruzar los individuos

            int[] primeraMitad = padreX.getPosiciones().Take(puntoDeCruce).ToArray();
            int[] segundaMitad = padreY.getPosiciones().Skip(puntoDeCruce).ToArray();
            int[] nuevasPosiciones = primeraMitad.Concat(segundaMitad).ToArray();

            //Crea un nuevo individuo a partir del nuevo ADN
            nuevoIndividuo = new Individuo(nuevasPosiciones);

            return nuevoIndividuo;
        }

        public static Individuo elegirPadre()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int mayorFitness = 0;
            int menorFitness = int.MaxValue;
            int randomFitness;

            //Obtiene el mayor fitness y el menor fitness dentro de la población
            for (int i = 0; i < tamanioPoblacion; i++)
            {
                if(poblacion[i].getFitness() > mayorFitness)
                {
                    mayorFitness = poblacion[i].getFitness();
                }
                else if (poblacion[i].getFitness() < menorFitness)
                {
                    menorFitness = poblacion[i].getFitness();
                }
            }

            //Elije un fitness random dentro del rango de la población
            randomFitness = rnd.Next(menorFitness, mayorFitness);

            //Recorre la población y elije al primer individuo cuyo fitness sea
            //menor al fitness elegido aleatoriamente
            for (int i = 0; i < tamanioPoblacion; i++)
            {
                if (poblacion[i].getFitness() <= randomFitness)
                {
                    return poblacion[i];
                }
            }

            return null;
        }

        public static Individuo genetic()
        {
            Individuo nuevoIndividuo;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Individuo[] poblacionTemp = new Individuo[tamanioPoblacion];

            int mejorFitness = int.MaxValue;
            int generacion = 0;

            //Crea una población inicial aleatoria
            crearPoblacion();

            while (true)
            {
                for (int i = 0; i < tamanioPoblacion; i++)
                {
                    //Se crea un individuo a partir de dos padres aleatorios
                    nuevoIndividuo = cruzar(elegirPadre(), elegirPadre());

                    //Si el nuevo individuo es la solución, termina el ciclo
                    if (nuevoIndividuo.getFitness() == 0)
                    {
                        return nuevoIndividuo;
                    }

                    //Se elige aleatoriamente si el nuevo individuo debe mutar o no
                    if (tasaMutacion > rnd.Next(0, 100))
                    {
                        nuevoIndividuo.mutar();
                    }

                    //Se elige si el nuevo individuo tiene el mejor fitness al momento del cálculo
                    if (nuevoIndividuo.getFitness() < mejorFitness)
                    {
                        mejorFitness = nuevoIndividuo.getFitness();
                    }

                    //El nuevo individuo pasa a ser parte de la nueva población
                    poblacionTemp[i] = nuevoIndividuo;
                }

                //Se reemplaza la población anterior con la nueva
                poblacion = poblacionTemp;
                generacion++;
                Console.Out.WriteLine("Fitness: " + mejorFitness + " Generación: " + generacion);
            }
        }
    }
}
