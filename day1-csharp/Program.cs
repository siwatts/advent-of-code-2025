using System.Diagnostics;
using System.IO;

namespace AOC
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("--\nAoC 2025 Day 1\n--");
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

            // Read file
            String? line;
            int lineNr = 0;
            int dial = 50;
            bool P2 = true;
            using (var streamReader = new StreamReader(filename))
            {
                while ((line = streamReader.ReadLine()) != null && (!debugmode || lineNr < debuglimit))
                {
                    if (debugmode)
                    {
                        Console.WriteLine(line);
                    }

                    // <<<< Process line >>>>
                    bool right = line[0] == 'R';
                    int num = Int32.Parse(line.Substring(1));
                    // Apply rotation
                    while (num > 0)
                    {
                        int remainingDial;
                        int thisMove;
                        if (right)
                        {
                            remainingDial = 100-dial;
                        }
                        else
                        {
                            remainingDial = dial;
                            if (remainingDial == 0)
                            {
                                remainingDial = 100;
                            }
                        }
                        if (!P2)
                        {
                            thisMove = num;
                        }
                        else
                        {
                            thisMove = Math.Min(remainingDial, num);
                        }
                        num -= thisMove;
                        if (right)
                        {
                            dial += thisMove;
                        }
                        else
                        {
                            dial -= thisMove;
                        }
                        while (dial > 99)
                        {
                            dial -= 100;
                        }
                        while (dial < 0)
                        {
                            dial += 100;
                        }
                        if (dial == 0)
                        {
                            res++;
                        }
                    }

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

