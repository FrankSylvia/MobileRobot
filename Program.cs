using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Classes:
    Main
        sets up for running or testing
    Globals
        constants used in project
    Grid
        manages array on locations where robot can go
    Robot
        processes movement command from a file
    Obstruction
        object placed in the grid

        Rock
            prevent robot from moving ot this location
        Hole
            when entering the location the robot will reappear at the specified other location
        Spinner
            changes the robot's direction



*/

namespace MobileRobot
{

    public struct Coords
    {
        public int r, c;
        public String d;

        public Coords(int R, int C, String D)
        {
            r = R;
            c = C;
            d = D;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            String rows, cols;
            String coordsEntry;
            Coords Current = new Coords();

            Console.WriteLine("Starting MobileRobot");

            // Testing setup stuff
            // Move desired test files to the C:\\Realgy\\Test folder
            // Store various tests in sub-folders
            String ObstructionsFile = "C:\\Realgy\\Test\\obstructions.csv";
            String StartupFile = "C:\\Realgy\\Test\\Startup.txt";
            String test = "";
            StreamReader startupReader = new StreamReader(File.OpenRead(StartupFile));
            test = startupReader.ReadLine();
            startupReader.Close();



            if (test.Length > 0) {
                // Apply the test startup data
                var t1 = test.Split(',');
                rows = t1[0];
                cols = t1[1];
                Current.r = Convert.ToInt32(t1[2]);
                Current.c = Convert.ToInt32(t1[3]);
                Current.d = t1[4];
            }
            else
            {
                // Prompt for startup data 
                Console.WriteLine("Number of rows?");
                rows = Console.ReadLine();

                Console.WriteLine("Number of columns?");
                cols = Console.ReadLine();

                Console.WriteLine("Starting coordinates and direction? e.g. 1,5,< or > or ^ or v");
                coordsEntry = Console.ReadLine();
                var temp = coordsEntry.Split(',');

                Current.r = Convert.ToInt32(temp[0]);
                Current.c = Convert.ToInt32(temp[1]);
                Current.d = temp[2];
                
            }
            if ((Current.r >= Convert.ToInt32(rows)) || (Current.c >= Convert.ToInt32(cols)))
            {
                Console.WriteLine("Starting Location is out of bounds");
                Console.ReadKey();
                return;
            }

            if (!((Current.d == "<") || (Current.d == ">") || (Current.d == "^") || (Current.d == "v")))
            {
                Console.WriteLine("Starting direction is invalid. Must be < or > or ^ or v");
                Console.ReadKey();
                return;

            }

            Grid RobotGrid = new Grid(rows, cols);
            RobotGrid.populate(ObstructionsFile);
            RobotGrid.dump();

            Robot Robbie = new Robot(RobotGrid,rows,cols,Current);
 
            Console.ReadKey();
        }

    }
}
