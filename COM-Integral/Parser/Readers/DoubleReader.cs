using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MathParser.Readers
{
	public class DoubleReader : RegexReader
	{
		//public DoubleReader() : base(@"^-?[0-9]+(?:\.[0-9]+)?(?:[eE]-?[0-9]+)?") { }
		public DoubleReader() : base(@"^[0-9]+(?:\.[0-9]+)?(?:[eE]-?[0-9]+)?") { }

		protected override LexicToken GetToken(string value)
		{
			var d = Double.Parse(value, CultureInfo.InvariantCulture);
			return new DoubleToken(d);
		}
	}
}
