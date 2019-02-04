using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileRobot
{
    class Hole : Obstruction
    {


        private int endRow;
        private int endCol;


        public Hole(string type, string startRow, string startCol, string endCol, string endRow):
            base(type, startRow, startCol)
        {
            this.endRow = Convert.ToInt32(endRow);
            this.endCol = Convert.ToInt32(endCol);
        }

        public int EndRow
        {
            get
            {
                return endRow;
            }
            set
            {
                endRow = value;
            }
        }

        public int EndCol
        {
            get
            {
                return endCol;
            }
            set
            {
                endCol = value;
            }


        }
        public override void dump()
        {
            base.dump();
            Console.WriteLine("End Row = " + this.endRow);
            Console.WriteLine("End Col = " + this.endCol);
        }

    }
}
