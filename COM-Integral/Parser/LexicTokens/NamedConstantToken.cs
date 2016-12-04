using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser.LexicTokens
{
	public sealed class NamedConstantToken : DoubleToken
	{
		public NamedConstantToken(string name, double value):base(value)
		{
			Name = name;
		}

		public string Name { get; set; }

		public override string ToString()
		{
			return Name + " = " + Value;
		}
	}
}
