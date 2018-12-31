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

            StreamReader reader = new StreamReader("calibrationOutput.txt");
            List<string> baseInput = new List<string>();

            // Read in the input from a text file and add all the lines to the baseInput as strings
            string newLine = "";
            while ((newLine = reader.ReadLine()) != null)
            {
                baseInput.Add(newLine);
            }
        }
    }
}
