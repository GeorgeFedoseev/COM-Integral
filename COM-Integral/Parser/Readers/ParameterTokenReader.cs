using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MathParser.Readers
{
	public class ParameterTokenReader : TokenReader
	{
		private IEnumerable<string> parameterNames = null;
		public IEnumerable<string> ParameterNames
		{
			get { return parameterNames; }
			set { parameterNames = value; }
		}

		public override InputStream TryRead(InputStream stream, out LexicToken token)
		{
			token = null;

			foreach (var parameterName in parameterNames)
			{
				if (stream.Content.StartsWith(parameterName))
				{
					ParameterToken paramToken = new ParameterToken(parameterName);
					token = paramToken;

					return stream.Move(parameterName.Length);
				}
			}

			return stream;
		}
	}
}
