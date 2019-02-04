using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileRobot
{
    class Obstruction
    {
        private String type;
        private int startRow;
        private int startCol;

        public Obstruction(String type, String startRow, String startCol)
        {
            this.type = type;
            this.startRow = Convert.ToInt32(startRow);
            this.startCol = Convert.ToInt32(startCol);
        }

        public String Type
        {
            get {
                return type;  
            }
            set {
                type = value;
            }
                    
            
        }
        public int StartRow
        {
            get
            {
                return startRow;
            }
            set
            {
                startRow = value;
            }
        }

        public int StartCol
        {
            get
            {
                return startCol;
            }
            set
            {
                startCol = value;
            }
 

        }

        public virtual void dump()

        {

            Console.WriteLine       ("Type      = " + this.type);
            Console.WriteLine("Start Row = " + this.startRow);
            Console.WriteLine("Start Col = " + this.startCol);
        }
        
    }
}
