using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AST = MathParser.Tree<MathParser.SyntaxToken>;

namespace MathParser.SyntaxTokenReaders {
	public sealed class BracketSyntaxTokenReader : SyntaxTokenReader {
		public BracketSyntaxTokenReader() {
			Priority = Priorities.Brackets;
		}

		public override AST Read(LinkedList<MixedToken> tokens, Grammar grammar) {
			var leftBracket = tokens.FindLast(t => t.IsLexicToken && t.LexicToken is LeftBracketToken);
			if (leftBracket != null) {
				var rightBracket = tokens.FindFirst(leftBracket, t => t.IsLexicToken && t.LexicToken is RightBracketToken);
				if (rightBracket == null)
					throw new ParserException("Unmatched left bracket.");

				var subList = leftBracket.GetSubList(rightBracket);
				var result = grammar.CreateAST(subList);
				tokens.AddBefore(leftBracket, new MixedToken(result));
				leftBracket.RemoveSubList(rightBracket);

				return result;
			}

			return null;
		}

		public override int GetPosition(LinkedList<MixedToken> tokens) {
			var leftBracket = tokens.FindLast(t => t.IsLexicToken && t.LexicToken is LeftBracketToken);

			return tokens.IndexOf(leftBracket);
		}
	}
}
