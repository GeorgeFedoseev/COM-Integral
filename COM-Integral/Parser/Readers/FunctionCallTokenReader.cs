using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParser.LexicTokens;

namespace MathParser.Readers
{
	public sealed class FunctionCallTokenReader : TokenReader
	{
		public IEnumerable<StaticFunction> Functions { get; set; }

		public override InputStream TryRead(InputStream stream, out LexicToken token)
		{
			token = null;

			foreach (var function in Functions)
			{
				if (stream.Content.StartsWith(function.Name))
				{
					FunctionCallToken functionToken = new FunctionCallToken { Method = function.Method };
					token = functionToken;

					return stream.Move(function.Name.Length);
				}
			}

			return stream;
		}
	}
}
