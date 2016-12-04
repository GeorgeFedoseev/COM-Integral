using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
	public sealed class NamedConstant
	{
		public NamedConstant() { }

		public NamedConstant(string name, double value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; }
		public double Value { get; set; }
	}
}
