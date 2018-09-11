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
        const int tamanioPoblacion = 500;
        const int tasaMutacion = 30;
        static Individuo[] poblacion = new Individuo[tamanioPoblacion];

        static void Main(string[] args)
        {
            
            Individuo individuo;
            int ind = 0;
            while (true)
            {
                ind++;
                //poblacion[i] = new Individuo(n);
                individuo = new Individuo(n);
                if (individuo.getFitness() == 0) break;
            }

            Console.Write(individuo.ToString() + "\n" + ind);

            /*
            Individuo solution = genetic();

            Console.Out.WriteLine(solution.ToString());
            */

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
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            int puntoDeCruce = rnd.Next(0, n - 1);

            int[] primeraMitad = padreX.getPosiciones().Take(puntoDeCruce).ToArray();
            int[] segundaMitad = padreY.getPosiciones().Skip(puntoDeCruce).ToArray();
            int[] nuevasPosiciones = primeraMitad.Concat(segundaMitad).ToArray();

            nuevoIndividuo = new Individuo(nuevasPosiciones);

            return nuevoIndividuo;
        }

        public static Individuo elegirPadre()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int mayorFitness = 0;
            int menorFitness = int.MaxValue;
            int randomFitness;

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

            randomFitness = rnd.Next(menorFitness, mayorFitness);

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
            int generation = 0;

            crearPoblacion();

            while (true)
            {
                generation++;

                for (int i = 0; i < tamanioPoblacion; i++)
                {
                    nuevoIndividuo = cruzar(elegirPadre(), elegirPadre());

                    if (nuevoIndividuo.getFitness() == 0)
                    {
                        Console.Out.WriteLine("Fitness: " + nuevoIndividuo.getFitness() + " Generation: " + generation);
                        return nuevoIndividuo;
                    }

                    if (tasaMutacion > rnd.Next(0, 100))
                    {
                        nuevoIndividuo.mutar();
                    }

                    if (nuevoIndividuo.getFitness() < mejorFitness)
                    {
                        mejorFitness = nuevoIndividuo.getFitness();
                    }

                    poblacionTemp[i] = nuevoIndividuo;
                }

                poblacion = poblacionTemp;
                Console.Out.WriteLine("Fitness: " + mejorFitness + " Generation: " + generation);
            }
        }
    }
}
