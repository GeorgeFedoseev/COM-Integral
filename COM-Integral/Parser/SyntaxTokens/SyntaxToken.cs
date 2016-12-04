using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MathParser
{
	public abstract class SyntaxToken
	{
		public abstract double Evaluate(Tree<SyntaxToken> tree, EvaluationContext context);
		public abstract Expression Compile(Tree<SyntaxToken> context);
		public virtual Tree<SyntaxToken> Optimize(Tree<SyntaxToken> tree)
		{
			return tree;
		}
	}
}
