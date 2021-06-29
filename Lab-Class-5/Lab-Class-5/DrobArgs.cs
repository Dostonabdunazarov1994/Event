using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Class_5
{
    class DrobArgs : EventArgs
    {
        public int OldP { get; set; }
        public int OldQ { get; set; }
        public DrobArgs(int oldP, int oldQ)
        {
            OldP = oldP;
            OldQ = oldQ;
        }
    }
}
