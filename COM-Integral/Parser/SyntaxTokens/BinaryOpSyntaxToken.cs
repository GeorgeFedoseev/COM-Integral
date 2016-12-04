using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MathParser.SyntaxTokens;

namespace MathParser
{
	public abstract class BinaryOpSyntaxToken : SyntaxToken
	{
		public sealed override double Evaluate(Tree<SyntaxToken> tree, EvaluationContext context)
		{
			var left = tree.Leafs[0];
			var right = tree.Leafs[1];

			return Evaluate(left.Value.Evaluate(left, context), right.Value.Evaluate(right, context));
		}

		protected abstract double Evaluate(double left, double right);

		public sealed override Expression Compile(Tree<SyntaxToken> context)
		{
			var left = context.Leafs[0];
			var right = context.Leafs[1];

			return Compile(left.Value.Compile(left), right.Value.Compile(right));
		}

		protected abstract Expression Compile(Expression left, Expression right);
	}

	public class AddSyntaxToken : BinaryOpSyntaxToken
	{
		protected override double Evaluate(double left, double right)
		{
			return left + right;
		}

		protected override Expression Compile(Expression left, Expression right)
		{
			return Expression.Add(left, right);
		}

		public override Tree<SyntaxToken> Optimize(Tree<SyntaxToken> tree)
		{
			var leftTree = tree.Leafs[0];
			var rightTree = tree.Leafs[1];
			var left = leftTree.Value as DoubleConstantSyntaxToken;
			var right = rightTree.Value as DoubleConstantSyntaxToken;

			if (left != null && left.Value == 0)
				return rightTree;
			if (right != null && right.Value == 0)
				return leftTree;
			return tree;
		}

		public override string ToString()
		{
			return "+";
		}
	}

	public class SubtractSyntaxToken : BinaryOpSyntaxToken
	{
		protected override double Evaluate(double left, double right)
		{
			return left - right;
		}

		protected override Expression Compile(Expression left, Expression right)
		{
			return Expression.Subtract(left, right);
		}

		public override Tree<SyntaxToken> Optimize(Tree<SyntaxToken> tree)
		{
			var leftTree = tree.Leafs[0];
			var rightTree = tree.Leafs[1];
			var left = leftTree.Value as DoubleConstantSyntaxToken;
			var right = rightTree.Value as DoubleConstantSyntaxToken;

			if (right != null && right.Value == 0)
				return leftTree;
			return tree;
		}

		public override string ToString()
		{
			return "-";
		}
	}

	public class MultiplySyntaxToken : BinaryOpSyntaxToken
	{
		protected override double Evaluate(double left, double right)
		{
			return left * right;
		}

		protected override Expression Compile(Expression left, Expression right)
		{
			return Expression.Multiply(left, right);
		}

		public override string ToString()
		{
			return "*";
		}
	}

	public class DivideSyntaxToken : BinaryOpSyntaxToken
	{
		protected override double Evaluate(double left, double right)
		{
			return left / right;
		}

		protected override Expression Compile(Expression left, Expression right)
		{
			return Expression.Divide(left, right);
		}

		public override string ToString()
		{
			return "/";
		}
	}

	public class PowerSyntaxToken : BinaryOpSyntaxToken
	{
		protected override double Evaluate(double left, double right)
		{
			return Math.Pow(left, right);
		}

		protected override Expression Compile(Expression left, Expression right)
		{
			return Expression.Power(left, right);
		}
	}
}
