using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MathParser
{
	public abstract class LexicToken { }

	public class MultiplyToken : LexicToken { }

	public class AddToken : LexicToken { }

	public class DivideToken : LexicToken { }

	public class SubtractToken : LexicToken { }

	public class WhitespaceToken : LexicToken { }

	public class LeftBracketToken : LexicToken { }

	public class RightBracketToken : LexicToken { }

	public class PowerToken : LexicToken { }

	public class DoubleToken : LexicToken
	{
		private readonly double value;
		public double Value
		{
			get { return this.value; }
		}

		public DoubleToken(double value)
		{
			this.value = value;
		}

		public override string ToString()
		{
			return String.Format("{0}: {1}", base.ToString(), value);
		}
	}

	public class ParameterToken : LexicToken
	{
		private readonly string parameterName;
		public string ParameterName
		{
			get { return parameterName; }
		}

		public ParameterToken(string parameterName)
		{
			if (String.IsNullOrEmpty(parameterName))
				throw new ArgumentNullException("parameterName");

			this.parameterName = parameterName;
		}

		public override string ToString()
		{
			return String.Format("{0}: {1}", base.ToString(), parameterName);
		}
	}
}
