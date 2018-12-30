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
            int firstDoubles = GetFirstDoubledFreq(baseInput);

            Console.WriteLine("Current Calibration: \n" + currentCalibration);
            Console.WriteLine();
            Console.WriteLine("First Doubled Frequency: \n" + firstDoubles);
            Console.ReadLine();
        }

        /// <summary>
        /// Checks the calibration of a given input and returns the end value
        /// </summary>
        /// <param name="baseInput"></param>
        private static int CheckCalibration(List<string> baseInput)
        {
            int current = 0;
            List<int> inputList = baseInput.Select(x => int.Parse(x)).ToList();
            foreach (int input in inputList)
            {
                current += input;
            }

            // Return out the resulting number
            return current;
        }

        /// <summary>
        /// Gets the first doubled frquency from a given string input
        /// </summary>
        /// <param name="baseInput"></param>
        /// <returns></returns>
        private static int GetFirstDoubledFreq(List<string> baseInput)
        {
            // Parse out each line and add it to the integer 'current'
            int current = 0;
            Dictionary<int, bool> frequencies = new Dictionary<int, bool>();
            List<int> inputList = baseInput.Select(x => int.Parse(x)).ToList();

            while (true)
            {
                foreach (int input in inputList)
                {
                    current += input;

                    // If a frequency is already in frequencies, return it
                    if (frequencies.ContainsKey(current))
                    {
                        return current;
                    }
                    else
                    {
                        int newFreq = current;
                        frequencies.Add(newFreq, true);
                        //Console.WriteLine(newFreq);           // Bugtesting
                    }
                }
            }
        }
    }
}
