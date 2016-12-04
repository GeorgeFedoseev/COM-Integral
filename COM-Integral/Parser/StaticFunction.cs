using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MathParser
{
	public sealed class StaticFunction
	{
		public string Name { get; set; }
		public MethodInfo Method { get; set; }
	}
}
