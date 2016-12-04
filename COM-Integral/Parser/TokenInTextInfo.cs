using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
	public class TokenInTextInfo
	{
		public int StartIndex { get; set; }
		public int Length { get; set; }
		public LexicToken Token { get; set; }
	}
}
