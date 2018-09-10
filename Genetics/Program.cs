using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 8;
            //Individuo[] poblacion = new Individuo[n];
            Individuo individuo;
            //for (int i = 0; i < 1000000; i++)
            while (true)
            {
                //poblacion[i] = new Individuo(n);
                individuo = new Individuo(n);
                if (individuo.getFitness() == 0) break;
            }

            Console.Write(individuo.ToString());
            System.Threading.Thread.Sleep(500000);

        }
    }
}
