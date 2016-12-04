using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
	public static class Priorities
	{
		public static double Number = 0;
		public static double FunctionCall = 1.5;
		public static double Brackets = 1;
		public static double Multiplication = 2;
		public static double Addition = 3;
		public static double Power = 1.5;
		public static double Negate = 1.75;
	}
}
