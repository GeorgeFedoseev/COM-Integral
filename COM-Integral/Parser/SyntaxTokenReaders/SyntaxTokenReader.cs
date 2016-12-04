using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AST = MathParser.Tree<MathParser.SyntaxToken>;

namespace MathParser
{
	public abstract class SyntaxTokenReader
	{
		public double Priority { get; set; }

		public abstract AST Read(LinkedList<MixedToken> tokens, Grammar grammar);

		public abstract int GetPosition(LinkedList<MixedToken> tokens);
	}
}
