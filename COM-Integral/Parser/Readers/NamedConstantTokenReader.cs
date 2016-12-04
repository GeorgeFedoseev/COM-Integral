using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParser.LexicTokens;

namespace MathParser.Readers
{
	public class NamedConstantTokenReader : TokenReader
	{
		public IEnumerable<NamedConstant> Constants { get; set; }

		public override InputStream TryRead(InputStream stream, out LexicToken token)
		{
			token = null;

			foreach (var constant in Constants)
			{
				if (stream.Content.StartsWith(constant.Name))
				{
					NamedConstantToken constantToken = new NamedConstantToken(constant.Name, constant.Value);
					token = constantToken;

					return stream.Move(constant.Name.Length);
				}
			}

			return stream;
		}
	}
}
