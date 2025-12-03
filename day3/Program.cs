using System.Diagnostics;
using System.IO;

namespace AOC
{
    public class Program
    {
        public static int GetMaxJoltage(string line)
        {
            int firstDigitPos = -1;
            int firstDigit = -1;
            int lastPos = line.Length - 1;
            // First digit will always be the highest digit in the string that isn't the final digit
            for (int i = 9; i >= 0; i--)
            {
                firstDigitPos = line.IndexOf(i.ToString().First());
                if (firstDigitPos != -1 && firstDigitPos != lastPos)
                {
                    firstDigit = i;
                    break;
                }
            }
            if (firstDigitPos == -1)
            {
                throw new InvalidDataException("Found no first digit in line '"+line+"'");
            }
            // Second digit will be the highest digit in the string remaining to the right
            int lastDigitPos;
            int lastDigit = -1;
            for (int i = 9; i >= 0; i--)
            {
                lastDigitPos = line.IndexOf(i.ToString().First(), firstDigitPos + 1);
                if (lastDigitPos != -1)
                {
                    lastDigit = i;
                    break;
                }
            }

            if (firstDigit == -1 || lastDigit == -1)
            {
                throw new InvalidDataException("Digit(s) not found, firstDigit = " + firstDigit.ToString() + ", lastDigit = " + lastDigit.ToString());
            }

            return (10 * firstDigit) + lastDigit;
        }
        public static int Main(string[] args)
        {
            Console.WriteLine("--\nAoC 2025 Day 3\n--");
            // For testing
            int debuglimit = 10;
            bool debugmode = false;

            // User args
            List<string> argv = args.ToList();
            string filename = "test";
            if (argv.Count == 0)
            {
                Console.WriteLine("Assume default input file '{0}'", filename);
            }
            else if (argv.Count > 0)
            {
                filename = argv[0];
                Console.WriteLine("Taking CLI input file name '{0}'", filename);
            }
            if (argv.Count > 1)
            {
                Console.WriteLine("Reading 2nd input param\n    -d / --debug for debug printing");
                if (argv[1] == "-d" || argv[1] == "--debug")
                {
                    debugmode = true;
                }
                else
                {
                    debugmode = false;
                }
            }
            if (debugmode)
            {
                Console.WriteLine("--\nDEBUG MODE : ON\nLINE LIMIT : {0}", debuglimit);
            }

            // Variables for output
            long res = 0;
            // <<<< Expected output for testing >>>>
            long? exp = null;
            if (filename == "test")
            {
                exp = 357;
            }

            // Read file
            String? line;
            int lineNr = 0;
            using (var streamReader = new StreamReader(filename))
            {
                while ((line = streamReader.ReadLine()) != null && (!debugmode || lineNr < debuglimit))
                {
                    if (debugmode)
                    {
                        Console.WriteLine(line);
                    }

                    // <<<< Process line >>>>
                    res += GetMaxJoltage(line);

                    lineNr++;
                }
            }

            // Post-processing
            if (debugmode)
            {
            }

            // Output
            Console.WriteLine("--");
            Console.WriteLine("Res = {0}", res);
            if (exp != null)
            {
                Console.WriteLine("Exp = {0}", exp);
                if (res == exp)
                {
                    Console.WriteLine("Test: PASS");
                }
                else
                {
                    Console.WriteLine("Test: FAIL");
                }
            }

            Console.WriteLine("--\nEnd.");
            return 0;
        }
    }
}

