using System;
using System.Collections.Generic;
using System.Linq;

namespace MarathonTest
{
    internal class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();

            int[] args = Array.ConvertAll(input.Split(" "), s => int.Parse(s));

            List<int> piles = new List<int>();
            List<int> tmpPiles = new List<int>();

            //Defined as N in problem
            int totalBoxes = args[0];

            //Defined as M in problem
            int maxPileSize = args[1];
            
            //Defined as P in problem
            int divider = args[2];

            //How many piles we're starting with
            int totalPiles = 0;

            if(totalBoxes <= maxPileSize)
            {
                //Only one trip necessary.
                Console.WriteLine("1");
            }
            else
            {
                //Divide into initial piles.
                piles = DistributeBoxes(totalBoxes, divider).ToList();

                do
                {
                    tmpPiles = piles.Where(p => p > maxPileSize).ToList();

                    //Remove oversized piles.
                    piles.RemoveAll(p => p > maxPileSize);

                    foreach (var subpile in tmpPiles)
                    {
                        piles.AddRange(DistributeBoxes(subpile, divider).ToList());
                    }
                }
                while(
                    //loop while we have any piles over the max.
                    piles.Where(p => p > maxPileSize).Any()
                );


                //Remove zero piles.
                piles.RemoveAll(p => p == 0);

                //For debugging and sanity check, write out all piles.
                //foreach(var pile in piles)
                //{
                //    Console.Write(pile.ToString() + "; ");
                //}

                totalPiles = piles.Count;

                Console.WriteLine(totalPiles);
            }          
        }

        //Distribute boxes evenly across targetPile
        public static IEnumerable<int> DistributeBoxes(int total, int divider)
        {
            if (divider == 0)
                yield return 0;

            int rest = total % divider;
            double result = total / (double)divider;

            for (int i = 0; i < divider; i++)
            {
                if (rest-- > 0)
                    yield return (int)Math.Ceiling(result);
                else
                    yield return (int)Math.Floor(result);
            }
        }
    }
}
