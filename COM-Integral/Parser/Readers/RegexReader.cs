using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MathParser.Readers
{
	public abstract class RegexReader : TokenReader
	{
		public RegexReader(string pattern)
		{
			regex = new Regex(pattern, RegexOptions.Compiled);
		}

		private readonly Regex regex;
		public override InputStream TryRead(InputStream stream, out LexicToken token)
		{
			token = null;
			Match match = regex.Match(stream.Content);
			if (match.Success)
			{
				token = GetToken(match.Value);
				return stream.Move(match.Value.Length);
			}
			return stream;
		}

		protected abstract LexicToken GetToken(string value);
	}
}
