using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using AST = MathParser.Tree<MathParser.SyntaxToken>;

namespace MathParser
{
	public sealed class MixedToken
	{
		public MixedToken(LexicToken lexicToken)
		{
			if (lexicToken == null)
				throw new ArgumentNullException("lexicToken");

			this.lexicToken = lexicToken;
		}

		public MixedToken(AST syntaxToken)
		{
			if (syntaxToken == null)
				throw new ArgumentNullException("syntaxToken");

			this.tree = syntaxToken;
		}

		//public MixedToken(Lazy<AST> syntaxTree)
		//{
		//    if (syntaxTree == null)
		//        throw new ArgumentNullException("syntaxTree");

		//    this.tree = syntaxTree;
		//}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LexicToken lexicToken;
		public LexicToken LexicToken
		{
			get { return lexicToken; }
			set { lexicToken = value; }
		}

		public bool IsLexicToken
		{
			get { return lexicToken != null; }
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AST tree;
		public AST Tree
		{
			get { return tree; }
			set
			{
				tree = value;
				lexicToken = null;
			}
		}

		public bool IsTree
		{
			get { return tree != null; }
		}

		public override string ToString()
		{
			if (IsLexicToken) return lexicToken.ToString();
			else return tree.Value.ToString();
		}
	}
}
