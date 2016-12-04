using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MathParser.SyntaxTokens
{
	public class ParameterSyntaxToken : SyntaxToken
	{
		public string ParameterName { get; set; }
		public ParameterExpression ParameterExpression { get; set; }

		public override double Evaluate(Tree<SyntaxToken> tree, EvaluationContext context)
		{
			return context.Parameters[ParameterName];
		}

		public override Expression Compile(Tree<SyntaxToken> context)
		{
			return ParameterExpression;
		}

		public override string ToString()
		{
			return ParameterName;
		}
	}
}
