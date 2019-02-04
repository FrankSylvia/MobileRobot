using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileRobot
{
    class Rock : Obstruction
    {

        public Rock(string type, string startRow, string startCol) :
            base(type, startRow, startCol)
        {

        }

        public override void dump()
        {
            base.dump();
        }
    }
}

