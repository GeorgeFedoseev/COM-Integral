using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MathParser.SyntaxTokens;
using AST = MathParser.Tree<MathParser.SyntaxToken>;

namespace MathParser
{
	public static class TreeExtensions
	{
		public static double Evaluate(this AST tree, EvaluationContext context)
		{
			return tree.Value.Evaluate(tree, context);
		}

		public static Expression Compile(this AST tree)
		{
			return tree.Value.Compile(tree);
		}

		public static AST Optimize(this AST tree)
		{
			EvaluationContext context = EvaluationContext.Empty;

			if (tree.IsConstant())
			{
				return tree.AsConstant();
			}

			for (int i = 0; i < tree.Leafs.Count; i++)
			{
				var subTree = tree.Leafs[i];
				if (subTree.IsConstant())
				{
					tree.Leafs[i] = subTree.AsConstant();
				}
			}

			return tree.Value.Optimize(tree);
		}

		private static AST AsConstant(this AST tree)
		{
			return new AST(new DoubleConstantSyntaxToken(tree.Evaluate(EvaluationContext.Empty)));
		}

		public static bool IsConstant(this AST tree)
		{
			bool isConstantLeaf = tree.IsLeaf && tree.Value is DoubleConstantSyntaxToken;
			bool isConstantTree = !tree.IsLeaf;

			if (!isConstantLeaf)
			{
				foreach (var subTree in tree.Leafs)
				{
					if (!subTree.IsConstant())
					{
						isConstantTree = false;
						break;
					}
				}
			}

			return isConstantLeaf || isConstantTree;
		}

		public static IEnumerable<AST> LeafsToRoot(this AST tree)
		{
			foreach (var subTree in tree.Leafs)
			{
				foreach (var subSubTree in subTree.LeafsToRoot())
				{
					yield return subSubTree;
				}
			}
			yield return tree;
		}

		public static IEnumerable<AST> RootToLeafsLeftToRight(this AST tree)
		{
			yield return tree;

			foreach (var subTree in tree.Leafs)
			{
				foreach (var subSubTree in subTree.RootToLeafsLeftToRight())
				{
					yield return subSubTree;
				}
			}
		}

		public static string ToExpressionString(this AST tree)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var subTree in tree.LeafsToRoot())
			{
				builder.Append(subTree.Value.ToString());
				builder.Append(' ');
			}
			builder.Remove(builder.Length - 1, 1);

			return builder.ToString();
		}

		public static string ToPolishInversedNotationString(this AST tree)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var subTree in tree.RootToLeafsLeftToRight())
			{
				builder.Append(subTree.Value.ToString());
				builder.Append(' ');
			}
			builder.Remove(builder.Length - 1, 1);

			return builder.ToString();
		}
	}
}
