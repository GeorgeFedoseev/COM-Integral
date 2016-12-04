using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MathParser.Readers
{
	public abstract class TokenReader
	{
		public abstract InputStream TryRead(InputStream stream, out LexicToken token);
	}

	public class CharReader : TokenReader
	{
		private readonly char c;
		private readonly LexicToken token;
		public CharReader(char c, LexicToken token)
		{
			this.c = c;
			this.token = token;
		}

		public override InputStream TryRead(InputStream stream, out LexicToken token)
		{
			token = null;

			if (stream.Content[0] == c)
			{
				token = this.token;
				return stream.Move(1);
			}

			return stream;
		}

		public override string ToString()
		{
			return "CharReader: " + c;
		}
	}

	public class StringReader : TokenReader
	{
		private readonly string str;
		private readonly LexicToken token;

		public StringReader(string str, LexicToken token)
		{
			this.str = str;
			this.token = token;
		}

		public override InputStream TryRead(InputStream stream, out LexicToken token)
		{
			token = null;

			if (stream.Content.StartsWith(str))
			{
				token = this.token;
				return stream.Move(str.Length);
			}

			return stream;
		}

		public override string ToString()
		{
			return "StringReader: " + str;
		}
	}

	public class WhitespaceReader : TokenReader
	{
		public override InputStream TryRead(InputStream stream, out LexicToken token)
		{
			token = null;

			char firstChar = stream.Content[0];

			if (firstChar == ' ' ||
				firstChar == '\t' ||
				firstChar == '\n' ||
				firstChar == '\r')
			{
				token = new WhitespaceToken();
				return stream.Move(1);
			}

			return stream;
		}
	}
}
