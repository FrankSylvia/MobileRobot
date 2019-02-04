using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileRobot
{


    class Spinner : Obstruction
    {

        private int count;


        public Spinner(string type, string startRow, string startCol, string count) :
            base(type, startRow, startCol)
        {
            this.count = Convert.ToInt32(count);
        }

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        public String Spin(String direction)
        {

            for (int i = this.Count; i > 0; i--)
            {
                if (direction == Globals.North)
                {
                    direction = Globals.East;
                }
                else if (direction == Globals.East)
                {
                    direction = Globals.South;
                }
                else if (direction == Globals.South)
                {
                    direction = Globals.West;
                }
                else if (direction == Globals.West)
                {
                    direction = Globals.North;
                }
            }

            return direction; 
        }

        public override void dump()
        {
            base.dump();
            Console.WriteLine("Count = " + this.count);
        }
    }
}
