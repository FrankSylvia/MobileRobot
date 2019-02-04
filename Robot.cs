using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/* Robot class

    Constructor
        initialize grid starting location
        record starting location
        Open command file 
        process each line - one command per line
        record each location after command has been applied

    processCmd
        identify next targeted location based on current location and current direction
        determine if the target location has an obstruction
        if so, process the obstruction rules
        

*/
namespace MobileRobot
{
    class Robot
    {

        public Grid Robbie;
        Coords Target;
        private int rows;
        private int cols;

        public Robot(Grid RobotGrid, String rows, String cols, Coords Current)
        {

            //Grid RobotGrid = new Grid(rows, cols);
            String RobotCommandsFile = "C:\\Realgy\\Test\\RobotCommands.txt";

            Robbie = RobotGrid;

            try
            {

                RobotGrid.init(Current);

                RobotGrid.recordLocation();

                StreamReader reader = new StreamReader(File.OpenRead(RobotCommandsFile));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    Console.WriteLine(line);
                    if (line.StartsWith("#")) {
                        continue;
                    }
                    processCmd(line);
                    RobotGrid.recordLocation();
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public virtual void processCmd(String cmd)
        {
            String status = "";

            this.rows = Robbie.getRows();
            this.cols = Robbie.getCols();
            Coords Current = Robbie.getCurrent();
            if (Current.d == Globals.East)
            {
                if (cmd == "F")
                {
                    if (Current.c < this.cols - 1)
                    {
                        Target.c = Current.c + 1;
                        Target.r = Current.r;
                        Target.d = Current.d;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "L")
                {
                    if (Current.r > 0)
                    {
                        Target.c = Current.c;
                        Target.r = Current.r - 1;
                        Target.d = Globals.North;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "R")
                {
                    if (Current.r < this.rows - 1)
                    {
                        Target.c = Current.c;
                        Target.r = Current.r + 1;
                        Target.d = Globals.South;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
            }
            else if (Current.d == Globals.West)
            {
                if (cmd == "F")
                {
                    if (Current.c > 0)
                    {
                        Target.c = Current.c - 1;
                        Target.r = Current.r;
                        Target.d = Current.d;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "L")
                {
                    if (Current.r < this.rows - 1)
                    {
                        Target.c = Current.c;
                        Target.r = Current.r + 1;
                        Target.d = Globals.South;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "R")
                {
                    if (Current.r > 0)
                    {
                        Target.c = Current.c;
                        Target.r = Current.r - 1;
                        Target.d = Globals.North;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
            }
            else if (Current.d == Globals.North)
            {
                if (cmd == "F")
                {
                    if (Current.r > 0)
                    {
                        Target.c = Current.c;
                        Target.r = Current.r - 1;
                        Target.d = Current.d;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "L")
                {
                    if (Current.c > 0)
                    {
                        Target.c = Current.c - 1;
                        Target.r = Current.r;
                        Target.d = Globals.West;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "R")
                {
                    if (Current.c < this.cols - 1)
                    {
                        Target.c = Current.c + 1;
                        Target.r = Current.r;
                        Target.d = Globals.East;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
            }
            else if (Current.d == Globals.South)
            {
                if (cmd == "F")
                {
                    if (Current.r < this.rows - 1)
                    {
                        Target.c = Current.c;
                        Target.r = Current.r + 1;
                        Target.d = Current.d;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "L")
                {
                    if (Current.c < this.rows - 1)
                    {
                        Target.c = Current.c + 1;
                        Target.r = Current.r;
                        Target.d = Globals.East;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
                else if (cmd == "R")
                {
                    if (Current.c > 0)
                    {
                        Target.c = Current.c - 1;
                        Target.r = Current.r;
                        Target.d = Globals.West;
                    }
                    else
                    {
                        status = "OOB";
                    }
                }
            }
            if (status == "OOB")
            {
                Console.WriteLine("OOB");
            }
            else
            {
                Object ob = Robbie.getLocationObject(Target.r, Target.c);
                if (ob == null)
                {
                    Current = Target;
                }
                else {
                    Console.WriteLine(ob.GetType().ToString());
                    if (ob.GetType().ToString() == "MobileRobot.Rock")
                    {
                        ob = (Rock)ob;

                    }
                    else if (ob.GetType().ToString() == "MobileRobot.Hole")
                    {
                        Hole hole = (Hole)ob;
                        Target.r = hole.EndRow;
                        Target.c = hole.EndCol;
                        Current = Target;
                    }
                    else if (ob.GetType().ToString() == "MobileRobot.Spinner")
                    {
                        Spinner spinner = (Spinner)ob;
                        Target.d = spinner.Spin(Target.d);
                        Current = Target;
                    }
                }

            }
            Robbie.setCurrent(Current);
        }

    }
}
