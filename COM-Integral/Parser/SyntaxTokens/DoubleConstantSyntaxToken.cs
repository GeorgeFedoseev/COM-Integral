using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MathParser.SyntaxTokens
{
	public class DoubleConstantSyntaxToken : SyntaxToken
	{
		public DoubleConstantSyntaxToken() { }
		public DoubleConstantSyntaxToken(double value)
		{
			this.Value = value;
		}

		public double Value { get; set; }

		public override double Evaluate(Tree<SyntaxToken> tree, EvaluationContext context)
		{
			return Value;
		}

		public override Expression Compile(Tree<SyntaxToken> context)
		{
			return Expression.Constant(Value);
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
