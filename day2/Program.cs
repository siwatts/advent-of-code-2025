using System.Diagnostics;
using System.IO;

namespace AOC
{
    public class Program
    {
        public static bool IsInvalidID(long id)
        {
            // Split number in half, see if it is a repeat of the same digits
            // If odd number of digits immediately discard
            int digits = (int)Math.Log10(id) + 1;
            if (digits % 2 != 0)
            {
                return false;
            }
            // Even number of digits means we can split it
            int halfDigits = digits / 2;
            long left = 0;
            long right = 0;
            for (int i = 0; i < halfDigits; i++)
            {
                left *= 10;
                left += id % 10;
                id /= 10;
            }
            for (int i = 0; i < halfDigits; i++)
            {
                right *= 10;
                right += id % 10;
                id /= 10;
            }

            return (left == right);
        }
        public static int Main(string[] args)
        {
            Console.WriteLine("--\nAoC 2025 Day 2\n--");
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
            long? exp;
            if (filename == "test")
            {
                exp = 1227775554;
            }
            else
            {
                exp = null;
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
                    // List of ranges comma separated, aa-bb,cc-dd
                    List<string> ranges = line.Split(',').ToList();
                    long start;
                    long end;
                    foreach (string range in ranges)
                    {
                        List<string> vals = range.Split('-').ToList();
                        start = Int64.Parse(vals[0]);
                        end = Int64.Parse(vals[1]);
                        // Loop over all values inclusive
                        for (long id = start; id <= end; id++)
                        {
                            if (IsInvalidID(id))
                            {
                                res += id;
                            }
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
                Console.WriteLine("Exp = {0}", res);
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

