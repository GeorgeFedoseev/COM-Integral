using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathParser;

namespace TestParserLib {
    class Program {
        static void Main(string[] args) {
            var parser = new Parser("x");
            var func = parser.Parse("2*sin(x)");
            Console.WriteLine(func.Evaluate(Math.PI/2));
            Console.ReadLine();
        }
    }
}
