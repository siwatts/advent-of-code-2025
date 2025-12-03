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
        public static bool IsInvalidIDPart2(long id)
        {
            // Split number, see if it is a repeat of the same digits
            // Need to consider possibilities with more than 2 repeat
            int digitCount = (int)Math.Log10(id) + 1;
            if (digitCount == 1)
            {
                // Special case when 1 digit long, cannot repeat
                return false;
            }
            List<short> digitList = new List<short>();
            for (int i = 0; i < digitCount; i++)
            {
                // Strip digits to process multiple times
                digitList.Add((short)(id % 10));
                id /= 10;
            }
            // Digits added from smallest to largest, so reverse them
            digitList.Reverse();

            // Handle special case of fragment length 1 first
            bool invalid = true;
            // Could use digitList.Distinct().Count == 1, but for loop allows us to shortcut
            // on first non-matching digit
            long first = digitList.First();
            foreach (short d in digitList)
            {
                if (first != d)
                {
                    invalid = false;
                    break;
                }
            }
            if (invalid)
            {
                return invalid;
            }

            // Now handle the other cases, we need to build fragments to compare
            int halfDigitCount = digitCount / 2;
            List<long> fragmentList;
            for (int fragmentSize = 2; fragmentSize <= halfDigitCount; fragmentSize++)
            {
                // See if it divides cleanly
                if (digitCount % fragmentSize == 0)
                {
                    // Proceed
                    int fragmentCount = digitCount / fragmentSize;
                    fragmentList = new List<long>();
                    for (int fn = 0; fn < fragmentCount; fn++)
                    {
                        long fragment = 0;
                        for (int i = 0; i < fragmentSize; i++)
                        {
                            fragment *= 10;
                            // Offset i by the digits we've already read in for past fragments
                            fragment += digitList[i + (fn*fragmentSize)];
                        }
                        fragmentList.Add(fragment);
                    }
                    // Check this fragment list for invalid status
                    first = fragmentList.First();
                    invalid = true;
                    foreach (long f in fragmentList)
                    {
                        if (first != f)
                        {
                            invalid = false;
                            break;
                        }
                    }
                    // Return early if we find one
                    if (invalid)
                    {
                        return invalid;
                    }
                }
            }

            return invalid;
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
            long resP2 = 0;
            // <<<< Expected output for testing >>>>
            long? exp;
            long? expP2;
            if (filename == "test")
            {
                exp = 1227775554;
                expP2 = 4174379265;
            }
            else
            {
                exp = null;
                expP2 = null;
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
                            if (IsInvalidIDPart2(id))
                            {
                                resP2 += id;
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
            Console.WriteLine("ResP2 = {0}", resP2);
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

