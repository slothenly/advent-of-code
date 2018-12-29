using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            // CHRONAL CALIBRATION
            // One of Santa's elves have detected a calibration anomaly. An input device
            // returns out the frequency change of the calibration tool from the last check-in.
            // Values are returned out into a single file, I need to calculate the final value of all
            // those inputs to detect if the device is calibrated. 

            StreamReader reader = new StreamReader("calibrationOutput.txt");
            List<string> baseInput = new List<string>();

            // Read in the input from a text file and add all the lines to the baseInput as strings
            string newLine = "";
            while ((newLine = reader.ReadLine()) != null)
            {
                baseInput.Add(newLine);
            }

            int currentCalibration = CheckCalibration(baseInput);

            Console.WriteLine("Current Calibration Levels: " + currentCalibration);
            Console.ReadLine();
        }

        /// <summary>
        /// Checks the calibration of a given input and returns the end value
        /// </summary>
        /// <param name="baseInput"></param>
        private static int CheckCalibration(List<string> baseInput)
        {
            // Parse out each line and add it to the integer 'current'
            int current = 0;
            foreach (string input in baseInput)
            {
                char[] inputChars = input.ToCharArray();
                char sign = inputChars[0];
                char[] inputValueChars = new char[inputChars.Length - 1];

                // Add all but the first character to a char array
                for (int i = 1; i < inputChars.Length; i++)
                {
                    inputValueChars[i - 1] = inputChars[i];
                }

                // Parse the number and return it into an integero
                int inputValue;
                string inputValueString = new string(inputValueChars);
                int.TryParse(inputValueString, out inputValue);

                // Actually calculate the end value
                if (inputChars[0] == '+')
                {
                    current += inputValue;
                }
                else
                {
                    current -= inputValue;
                }
            }

            // Return out the resulting number
            return current;
        }
    }
}
