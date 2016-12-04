using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MathParser.LexicTokens
{
	public sealed class FunctionCallToken : LexicToken
	{
		public MethodInfo Method { get; set; }
	}
}
