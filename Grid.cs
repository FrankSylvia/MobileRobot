using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MobileRobot
{
    /* Grid class

        Constructor 
            create a two-dimensional array of System.Objects base on user defined dimensions

        populate
            open Obstructions map file 
            for each line,
                generate either a Rock, Hole or Spinner Object and insert into the grid 
                at it row & column location

        recordLocation - write out the current robot location to the screen

        dump - write out location of all obstructions in the grid to the screen

    */
    class Grid
    {

        private int rows;
        private int cols;

        object[,] array;

        Obstruction ob;

        Coords Current;
        //Coords Target;

        public Grid(String rows, String cols)
        {
            this.rows = Convert.ToInt32(rows);
            this.cols = Convert.ToInt32(cols);


            array = new object[this.rows, this.cols];
        }

        public int getRows()
        {
            return this.rows;
        }
        public int getCols()
        {
            return this.cols;
        }

        public Coords getCurrent()
        {
            return Current;
        }

        public void setCurrent(Coords current)
        {
            this.Current = current;
        }

        public Object getLocationObject(int r, int c)
        {
            Object loc = (Obstruction)array[r, c];
            return loc;

        }

        public void setLocationObject(int r, int c, object obj)
        {
            array[r, c] = obj;


        }
        public void populate(String filename)
        {
            StreamReader reader=null;
            try
            {
                reader = new StreamReader(File.OpenRead(filename));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Length < 2)
                    {
                        continue;
                    }
                    var values = line.Split(',');
                    int r = Convert.ToInt32(values[1]);
                    int c = Convert.ToInt32(values[2]);

                    if ((r >= this.rows) || (c >= this.cols))
                    {
                        Console.WriteLine("Obstruction location is out of bounds" + line);
                        continue;
                    }

                    if (values[0] == "Rock")
                    {
                        ob = new Rock(values[0], values[1], values[2]);
                        setLocationObject(r, c, ob);
                    }
                    else if (values[0] == "Spinner")
                    {
                        ob = new Spinner(values[0], values[1], values[2], values[3]);
                        setLocationObject(r, c, ob);
                    }
                    else if (values[0] == "Hole")
                    {
                        int endr = Convert.ToInt32(values[4]);
                        int endc = Convert.ToInt32(values[5]);

                        if ((endr >= this.rows) || (endc >= this.cols))
                        {
                            Console.WriteLine("Hole destination location is out of bounds" + line);
                            continue;
                        }
                        ob = new Hole(values[0], values[1], values[2], values[4], values[5]);
                        setLocationObject(r, c, ob);
                    }
                    else {
                        Console.WriteLine("Invalid Obstruction type: " + line);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void recordLocation()
        {
            Console.WriteLine(Current.r.ToString() + "," + Current.c.ToString() + " " + Current.d);
        }
        
        public void init(Coords starting)
        {
            Current = starting;
        }
    public void dump()
        {
            for (int r=0;r<this.rows;r++)
            {
                for (int c=0;c<this.cols;c++)
                {

                    if (array[r,c] != null)
                    {
                        Obstruction ob = (Obstruction) getLocationObject(r, c);

                        
                        Console.WriteLine(r.ToString() + "," + c.ToString() + " " + ob.GetType().ToString());
                    }
                }
            }
        }
    }
}
