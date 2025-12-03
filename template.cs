using System.Diagnostics;
using System.IO;

namespace AOC
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("--\nAoC 2025 Day X\n--");
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
            long resP1 = 0;
            long resP2 = 0;
            // <<<< Expected output for testing >>>>
            long? expP1 = null;
            long? expP2 = null;
            if (filename == "test")
            {
                //expP1 = X;
                //expP2 = X;
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

                    lineNr++;
                }
            }

            // Post-processing
            if (debugmode)
            {
            }

            // Output
            Console.WriteLine("--");
            Console.WriteLine("ResP1 = {0}", resP1);
            if (expP1 != null)
            {
                Console.WriteLine("ExpP1 = {0}", expP1);
                if (resP1 == expP1)
                {
                    Console.WriteLine("Test: PASS");
                }
                else
                {
                    Console.WriteLine("Test: FAIL");
                }
            }
            Console.WriteLine("--");
            Console.WriteLine("ResP2 = {0}", resP2);
            if (expP2 != null)
            {
                Console.WriteLine("ExpP2 = {0}", expP2);
                if (resP2 == expP2)
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

