using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            // ALCHEMICAL REDUCTION
            // I have a chemical formed by an extremely long chain of polymers. The polymer if made of units that
            // react to specific other elements with extreme volitility. If two adjacent units are of the same type
            // but different cases, they will react and be destroyed.  When the base polymer reacts, how many
            // units remain?

            // FIELDS
            StreamReader reader = new StreamReader("basePolymer.txt");
            string baseInput = "";
            char[] baseChars;

            // Read in the input from a text file and add all the lines to the baseInput string
            string newLine = "";
            while ((newLine = reader.ReadLine()) != null)
            {
                baseInput += newLine;
            }

            // Pass that input into a character array
            baseChars = baseInput.ToCharArray();
            Console.WriteLine("Initial setup prepared, working...");
            Console.WriteLine("First character in array: " + baseInput[0]);

            // Recursively edit the array checking for more reactions
            Console.WriteLine("Fully reacted length: " + AdjustArray(baseChars).Length);

            // Recursively edit the array checking for each letter being removed & denote shortest one
            char shortestChainLetter = 'A';
            int shorestChainLength = int.MaxValue;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string fullyReacted = AdjustArray(RemoveAllOfType(c, baseChars));
                Console.WriteLine("Letter: " + c.ToString() + " Length: " + fullyReacted.Length);

                // Allows us to return out shortest letter and length by checking if this is the shortest one yet
                if (fullyReacted.Length < shorestChainLength)
                {
                    shortestChainLetter = c;
                    shorestChainLength = fullyReacted.Length;
                }
            }
            Console.WriteLine("Shortest Chain Letter: " + shortestChainLetter.ToString());
            Console.WriteLine("Shortest Chain Length: " + shorestChainLength.ToString());

            Console.ReadLine();
        }

        /// <summary>
        /// Recursively adjusting array
        /// </summary>
        /// <param name="passedArray"></param>
        /// <returns></returns>
        public static string AdjustArray(char[] passedArray)
        {
            // FIELDS
            bool changed = false;

            // Loops through checking for reactions
            for (int i = 0; i < passedArray.Length-1; i++)
            {
                // Marks off any to be removed
                if (CheckChars(passedArray[i], passedArray[i + 1]))
                {
                    changed = true;
                    passedArray[i] = '0';
                    passedArray[i + 1] = '0';
                }
            }

            // Remove all the zeros and create a new array to be checked or returned
            List<char> charList = new List<char>();
            foreach (char c in passedArray)
            {
                if (c != '0')
                {
                    charList.Add(c);
                }
            }
            char[] newArray = charList.ToArray();

            // Create the returning string
            string returned = new string(newArray);

            // If any changes were made, call this fxn again with the new array
            if (changed)
            {
                returned = AdjustArray(newArray);
            }

            //Console.WriteLine(returned.Length);
            return returned;
        }

        /// <summary>
        /// Checks if two characters that were passed in are opposite cases
        /// </summary>
        public static bool CheckChars(char char1, char char2)
        {
            // Checks if the two passed in characters are the same letter
            if (char1.ToString().ToUpper() == char2.ToString().ToUpper())
            {
                if (char.IsUpper(char1))
                {
                    // First character is uppercase
                    if (char.IsLower(char2))
                    {
                        // Second character is contrary case
                        return true;

                    }
                    else
                    {
                        // Second character is same case
                        return false;
                    }
                }
                else
                {
                    // First character is lowercase
                    if (char.IsUpper(char2))
                    {
                        // Second character is contrary case
                        return true;
                    }
                    else
                    {
                        // Second character is same case
                        return false;
                    }
                }
            }
            else
            {
                // Characters are not the same letter, return false
                return false;
            }
        }

        /// <summary>
        /// Removes all of a given character from the 
        /// </summary>
        /// <param name="character"></param>
        public static char[] RemoveAllOfType(char passedChar, char[] passedArray)
        {
            // Loops through checking for specific character
            for (int i = 0; i < passedArray.Length - 1; i++)
            {
                // Marks off any found to have the same character to be removed
                if (passedArray[i].ToString().ToUpper() == passedChar.ToString().ToUpper())
                {
                    passedArray[i] = '0';
                }
            }

            // Remove all the zeros and create a new array to be checked or returned
            List<char> charList = new List<char>();
            foreach (char c in passedArray)
            {
                if (c != '0')
                {
                    charList.Add(c);
                }
            }

            // Convert the list back into an array and return it
            return charList.ToArray();
        }
    }
}
