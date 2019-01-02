using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            // SANTA'S SUIT FABRIC
            // Santa's elves have located the fabric for his suit for Christmas. The issue is they're
            // struggling to figure out how to cut up the fabric to MAKE his suit. All his else have been
            // submitting orders for different rectangular pieces of the fabric. The claims are formatted as follows:
            // EX: #123 @ 3,2: 5x4
            // EX: '#123' is the order number
            // EX: '3,2' specifies the rect is 3 inches from the left and 2 inches from the top
            // EX: '5x4' specifies the rect is 5 inches wide and 4 inches tall

            StreamReader reader = new StreamReader("size-orders.txt");
            List<string> baseInput = new List<string>();

            // Read in the input from a text file and add all the lines to the baseInput as strings
            string newLine = "";    
            while ((newLine = reader.ReadLine()) != null)
            {
                baseInput.Add(newLine);
            }

            Console.WriteLine("Crossover Squares: " + FindCrossover(baseInput));
            Console.ReadLine();
        }

        /// <summary>
        /// Returns the amount of crossover tiles based on a list of entries
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private static int FindCrossover(List<string> inputs)
        {
            const int LENGTH = 1200;
            int[,] mainSheet = new int[LENGTH, LENGTH];
            string[] currentOrder;

            // Fill the main sheet with zeros
            for (int i = 0; i < LENGTH; i++)
            {
                for (int j = 0; j < LENGTH; j++)
                {
                    mainSheet[i, j] = 0;
                }
            }

            // Place all of the orders onto the sheet
            foreach (string order in inputs)
            {
                // split up the order into the individual parts
                currentOrder = order.Split(' ');
                string[] placement = currentOrder[2].Split(',');
                string[] dimensions = currentOrder[3].Split('x');

                // claim placement (top & left)
                int inchesFromLeft = int.Parse(placement[0]);
                List<char> topInput = placement[1].ToCharArray().ToList();
                topInput.RemoveAt(topInput.Count - 1);
                string top = new string(topInput.ToArray());
                int inchesFromTop = int.Parse(top);

                // claim dimensions (height & width)
                int height = int.Parse(dimensions[1]);
                int width = int.Parse(dimensions[0]);

                Console.WriteLine("LEFT: " + inchesFromLeft);
                Console.WriteLine("TOP: " + inchesFromTop);
                Console.WriteLine("WIDTH: " + width);
                Console.WriteLine("HEIGHT: " + height);

                // Place a rectangle with these specifications into the array
                for (int i = inchesFromLeft; i < inchesFromLeft + width; i++)
                {
                    for (int j = inchesFromTop; j < inchesFromTop + height; j++)
                    {
                        mainSheet[i, j] += 1;
                    }
                }
            }

            // Count how many places are filled
            return mainSheet.Cast<int>().Where(x => x >= 2).Count();
        }
    }
}
