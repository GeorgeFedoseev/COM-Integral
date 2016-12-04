using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace MathParser.SyntaxTokens
{
	public abstract class UnaryOpSyntaxToken : SyntaxToken
	{
		public sealed override double Evaluate(Tree<SyntaxToken> tree, EvaluationContext context)
		{
			var leaf = tree.Leafs[0];
			double value = leaf.Value.Evaluate(leaf, context);

			return Evaluate(value);
		}

		protected abstract double Evaluate(double value);

		public sealed override Expression Compile(Tree<SyntaxToken> context)
		{
			var leaf = context.Leafs[0];
			var expression = leaf.Value.Compile(leaf);

			return Compile(expression);
		}

		protected abstract Expression Compile(Expression expression);
	}

	public class NegateSyntaxToken : UnaryOpSyntaxToken
	{
		protected override double Evaluate(double value)
		{
			return -value;
		}

		protected override Expression Compile(Expression expression)
		{
			return Expression.Negate(expression);
		}
	}

	public class UnaryStaticFunctionSyntaxToken : UnaryOpSyntaxToken
	{
		public UnaryStaticFunctionSyntaxToken(MethodInfo method)
		{
			if (method == null)
				throw new ArgumentNullException("method");

			this.method = method;
		}

		private MethodInfo method;
		protected override double Evaluate(double value)
		{
			return (double)method.Invoke(null, new object[] { value });
		}

		protected override Expression Compile(Expression expression)
		{
			return Expression.Call(method, expression);
		}

		public override string ToString()
		{
			return method.Name;
		}
	}

}
