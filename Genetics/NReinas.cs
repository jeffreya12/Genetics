using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
    class NReinas
    {
        const int n = 8;
        const int tamanioPoblacion = 1;
        const int tasaMutacion = 10;
        static Individuo[] poblacion = new Individuo[tamanioPoblacion];

        static void Main(string[] args)
        {
            /*
            Individuo individuo;
            int ind = 0;
            while (true)
            {
                ind++;
                //poblacion[i] = new Individuo(n);
                individuo = new Individuo(n);
                if (individuo.getFitness() == 0) break;
            }

            Console.Write(individuo.ToString() + "\n" + ind + "\n");
            System.Threading.Thread.Sleep(500000);
            */
            
            Individuo solution = geneticAlg();

            Console.Out.WriteLine(solution.ToString());
            Console.ReadLine();
        }

        public static void crearPoblacion()
        {
            for (int i = 0; i < tamanioPoblacion; i++)
            {
                poblacion[i] = new Individuo(n);
            }
        }

        public static Individuo cruzar(Individuo padreX, Individuo padreY)
        {
            Individuo nuevoIndividuo;

            int puntoDeCruce = n / 2;

            int[] primeraMitad = padreX.getPosiciones().Take(puntoDeCruce).ToArray();
            int[] segundaMitad = padreY.getPosiciones().Skip(puntoDeCruce).ToArray();
            int[] nuevasPosiciones = primeraMitad.Concat(segundaMitad).ToArray();

            nuevoIndividuo = new Individuo(nuevasPosiciones);

            return nuevoIndividuo;
        }

        public static Individuo elegirPadre()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int total = 0;
            int randomFitness;

            // Get current total fitness
            for (int i = 0; i < tamanioPoblacion; i++)
            {
                total += poblacion[i].getFitness();
            }

            randomFitness = rnd.Next(0, total);

            // Choose random parent, higher fitness has higher chance
            for (int i = 0; i < tamanioPoblacion; i++)
            {
                if (randomFitness < poblacion[i].getFitness())
                {
                    return poblacion[i];
                }
                randomFitness = randomFitness - poblacion[i].getFitness();
            }

            return null;
        }

        public static Individuo geneticAlg()
        {
            Individuo child;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Individuo[] tempPopulation = new Individuo[tamanioPoblacion];

            int highestFitness = 100000000;
            int generation = 0;

            crearPoblacion();

            child = poblacion[0];

            while (true)
            {
                generation++;

                // Begin creation of new generation
                for (int i = 0; i < tamanioPoblacion; i++)
                {
                    // Choose two parents and create a child
                    child = cruzar(elegirPadre(), elegirPadre());

                    // Check to see if child is a solution
                    if (child.getFitness() == 0)
                    {
                        Console.Out.WriteLine("Fitness: " + child.getFitness() + " Generation: " + generation);
                        return child;
                    }

                    // Mutation change
                    if (tasaMutacion > rnd.Next(0, 100))
                    {
                        child.mutar();
                    }

                    // Check childs fitness
                    if (child.getFitness() < highestFitness)
                    {
                        highestFitness = child.getFitness();
                    }

                    tempPopulation[i] = child;
                }

                poblacion = tempPopulation;
                Console.Out.WriteLine("Fitness: " + highestFitness + " Generation: " + generation);
            }
        }
    }
}
