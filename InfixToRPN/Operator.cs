using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixToRPN
{
    class Operator
    {
        public string operatorCode { get; set; }
        public int associativity { get; set; }
        public int precedence { get; set; }
    }
}
