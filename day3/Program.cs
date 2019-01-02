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
            Console.WriteLine("Non-overlap: " + FindNonOverlap(baseInput));
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
            int[,] mainSheet = CreateSheet(LENGTH, inputs);

            // Count how many places are filled
            return mainSheet.Cast<int>().Where(x => x >= 2).Count();
        }

        /// <summary>
        /// Helper fxn to create a sheet based on a given set of inputs
        /// </summary>
        /// <param name="LENGTH"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private static int[,] CreateSheet(int LENGTH, List<string> inputs)
        {
            int[,] sheet = new int[LENGTH, LENGTH];
            string[] currentOrder;

            // Fill the main sheet with zeros
            for (int i = 0; i < LENGTH; i++)
            {
                for (int j = 0; j < LENGTH; j++)
                {
                    sheet[i, j] = 0;
                }
            }

            // Place all of the orders onto the sheet
            foreach (string order in inputs)
            {
                // Parse out order information
                int[] orderInfo = ParseInputs(order);
                int inchesFromLeft = orderInfo[0];
                int inchesFromTop = orderInfo[1];
                int height = orderInfo[2];
                int width = orderInfo[3];

                // Place a rectangle with these specifications into the array
                for (int i = inchesFromLeft; i < inchesFromLeft + width; i++)
                {
                    for (int j = inchesFromTop; j < inchesFromTop + height; j++)
                    {
                        sheet[i, j] += 1;
                    }
                }
            }

            return sheet;
        }

        /// <summary>
        /// Finds the non-overlapping sheet order in a given input
        /// </summary>
        private static string FindNonOverlap(List<string> inputs)
        {
            int[,] currentSheet = CreateSheet(1200, inputs);

            int[] tempInputs;
            int inchesFromLeft;
            int inchesFromTop;
            int width;
            int height;

            foreach (string current in inputs)
            {
                // Check if this is a non-overlapping input
                tempInputs = ParseInputs(current);
                inchesFromLeft = tempInputs[0];
                inchesFromTop = tempInputs[1];
                height = tempInputs[2];
                width = tempInputs[3];

                if (CheckPositions(inchesFromLeft, inchesFromTop, width, height, currentSheet))
                {
                    return current;
                }
            }

            return "-1";
        }

        /// <summary>
        /// Checks all the positions of a given input to see if there is any overlap
        /// </summary>
        private static bool CheckPositions(int inchesFromLeft, int inchesFromTop, int width, int height, int[,] currentSheet)
        {
            for (int xPos = inchesFromLeft; xPos < inchesFromLeft + width; xPos++)
            {
                for (int yPos = inchesFromTop; yPos < inchesFromTop + height; yPos++)
                {
                    if (currentSheet[xPos, yPos] != 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static int[] ParseInputs(string input)
        {
            // split up the order into the individual parts
            string[] currentOrder = input.Split(' ');
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

            return new int[] { inchesFromLeft, inchesFromTop, height, width };
        }
    }
}
