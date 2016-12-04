using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
	public static class StringExtensions
	{
		public static ParsingResult Parse(this string expression)
		{
			Parser parser = new Parser();
			return parser.Parse(expression);
		}

		public static ParsingResult ParseWithParameters(this string expression, params string[] parameters)
		{
			Parser parser = new Parser(parameters);
			return parser.Parse(expression);
		}
	}
}
