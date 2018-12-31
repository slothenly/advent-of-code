using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            // INVENTORY MANAGEMENT
            // Take the input given and return a count of how many double character inputs
            // and how many triple character inputs are in a given entry
            // EX: abac has 'a' twice so it has a double character input
            // EX: aabcda has 'a' three times, so it has a triple character input

            // FIELDS
            StreamReader reader = new StreamReader("puzzle-input.txt");
            List<string> baseInput = new List<string>();

            // Read in the input from a text file and add all the lines to the baseInput string
            string newLine = "";
            while ((newLine = reader.ReadLine()) != null)
            {
                baseInput.Add(newLine);
            }

            // Get the counts for amount of repeated doubles and triples then print them
            int[] outputs = GetRepeats(baseInput);
            Console.WriteLine("Doubles: " + outputs[0].ToString());            
            Console.WriteLine("Triples: " + outputs[1].ToString());
            Console.WriteLine("Checksum: " + (outputs[0] * outputs[1]).ToString());
            Console.WriteLine();
            Console.WriteLine("Correct String: " + GetCorrectString(baseInput));
            Console.ReadLine();
        }

        /// <summary>
        /// Gets the amount of doubles and triples in a string
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private static int[] GetRepeats(List<string> inputs)
        {
            int doubles = 0;
            int triples = 0;

            // Count the amount if inputs for each character via dictionary
            foreach (string s in inputs)
            {
                Dictionary<char, int> inputCounts = new Dictionary<char, int>();
                char[] chars = s.ToCharArray();
                bool modDoubles = false;
                bool modTrips = false;

                // Loop through and modify dictionary based on characters in the string
                for (int i = 0; i < chars.Length; i++)
                {
                    if (!inputCounts.ContainsKey(chars[i]))
                    {
                        inputCounts[chars[i]] = 1;
                    }
                    else
                    {
                        inputCounts[chars[i]]++;
                    }
                }

                // Modify doubles and triples based on if there's a string with 2 or 3 
                foreach (var item in inputCounts)
                {
                    if (item.Value == 2)
                    {
                        modDoubles = true;
                    }
                    if (item.Value == 3)
                    {
                        modTrips = true;
                    }
                }

                if (modDoubles)
                {
                    doubles++;
                }
                if (modTrips)
                {
                    triples++;
                }
            }

            int[] returned = { doubles, triples };
            return returned;
        }

        /// <summary>
        /// Gets the correct string input for a given set of inputs
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private static string GetCorrectString(List<string> inputs)
        {
            string returned = "";
            const int INPUT_LINE_LENGTH = 26;

            // Compare every line against every other line
            foreach (string main in inputs)
            {
                foreach (string comparator in inputs)
                {
                    // Make sure the lines being compared are not the same
                    if (main != comparator)
                    {
                        int similars = 0;
                        int diffIndex = -1;
                        char[] input1 = main.ToCharArray();
                        char[] input2 = comparator.ToCharArray();

                        for (int i = 0; i < INPUT_LINE_LENGTH; i++)
                        {
                            if (input1[i] == input2[i])
                            {
                                similars++;
                            }
                            else
                            {
                                // If this is the first difference noted, mark the current index
                                if (diffIndex == -1)
                                {
                                    diffIndex = i;
                                }
                                // If another has been noted, break
                                else
                                {
                                    break;
                                }
                            }
                        }

                        if (similars == INPUT_LINE_LENGTH - 1)
                        {
                            string final = "";
                            for (int i = 0; i < INPUT_LINE_LENGTH; i++)
                            {
                                if (i != diffIndex)
                                {
                                    final += main[i];
                                }
                            }
                            Console.WriteLine("MAIN STRING: " + main);
                            Console.WriteLine("COMP STRING: " + comparator);
                            Console.WriteLine("COMP STRING LENGTH: " + comparator.Length);
                            Console.WriteLine("Similarities:" + similars);
                            Console.WriteLine("Differences: " + (INPUT_LINE_LENGTH - similars).ToString());
                            Console.WriteLine("Difference Index: " + diffIndex.ToString());
                            return final;
                        }
                    }
                }
            }

            return returned;
        }
    }
}
