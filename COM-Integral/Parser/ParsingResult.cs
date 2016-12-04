using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using AST = MathParser.Tree<MathParser.SyntaxToken>;

namespace MathParser
{
	public sealed class ParsingResult
	{
		public AST Tree { get; internal set; }

		public IEnumerable<string> ParameterNames { get; internal set; }

		internal ParameterExpression[] ParameterExpressions { get; set; }

		public double Evaluate()
		{
			EvaluationContext context = new EvaluationContext();
			return Tree.Evaluate(context);
		}

		public double Evaluate(double parameterValue)
		{
			EvaluationContext context = new EvaluationContext();
			var firstParam = ParameterNames.FirstOrDefault();
			if (firstParam != null)
			{
				context.Parameters[firstParam] = parameterValue;
			}
			else
			{
				throw new ParserException();
			}
			return Tree.Evaluate(context);
		}

		public double Evaluate(EvaluationContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			return Tree.Evaluate(context);
		}

		public Expression<TFunc> ToExpression<TFunc>()
		{
			var compiled = Tree.Compile();
			var lambda = Expression.Lambda<TFunc>(compiled, ParameterExpressions);
			return lambda;
		}
	}
}
