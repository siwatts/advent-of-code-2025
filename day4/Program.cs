using System.Diagnostics;
using System.IO;

namespace AOC
{
    public class Warehouse
    {
        private List<List<char>> grid = new List<List<char>>();
        private int maxX = -1;
        private int maxY = -1;
        private const char paperChar = '@';
        public Warehouse()
        {
        }
        public void AddRow(string line)
        {
            if (maxX != -1 && maxX != line.Length - 1)
            {
                throw new InvalidDataException("Line with different length " + line.Length + " added to grid of width " + maxX+1);
            }
            else if (maxX == -1)
            {
                maxX = line.Length - 1;
            }

            grid.Add(line.ToList<char>());
            maxY++;
        }
        private bool IsPaperRoll(int x, int y)
        {
            if (x >= 0 && x <= maxX)
            {
                if (y >= 0 && y <= maxY)
                {
                    return (grid[y][x] == paperChar);
                }
            }
            // Out of bounds returns false
            return false;
        }
        private int NumberOfAdjacentPaperRolls(int x, int y)
        {
            // xxx, y-1, x-1 to  x+1
            // xox,  y , x-1 AND x+1
            // xxx, y+1, x-1 to  x+1
            int sum = 0;
            for (int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    // Skip the centre i = x, j = y
                    if (i != x || j != y)
                    {
                        if (IsPaperRoll(i,j))
                        {
                            sum++;
                        }
                    }
                }
            }

            return sum;
        }
        public int SumAccessiblePaperRolls()
        {
            int sum = 0;
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    if (IsPaperRoll(x, y))
                    {
                        // See if it is accessible
                        int num = NumberOfAdjacentPaperRolls(x, y);
                        // This paper roll is accessible if it has < 4 neighbours
                        if (num < 4)
                        {
                            sum++;
                        }
                    }
                }
            }
            return sum;
        }
    }
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("--\nAoC 2025 Day 4\n--");
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
                expP1 = 13;
                //expP2 = X;
            }

            // Read file
            String? line;
            int lineNr = 0;
            Warehouse warehouse = new Warehouse();
            using (var streamReader = new StreamReader(filename))
            {
                while ((line = streamReader.ReadLine()) != null && (!debugmode || lineNr < debuglimit))
                {
                    if (debugmode)
                    {
                        Console.WriteLine(line);
                    }

                    // <<<< Process line >>>>
                    warehouse.AddRow(line);

                    lineNr++;
                }
            }

            // Post-processing
            if (debugmode)
            {
            }
            resP1 = warehouse.SumAccessiblePaperRolls();

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

