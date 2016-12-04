using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParser.SyntaxTokens;

namespace MathParser.SyntaxTokenReaders {
	public sealed class NegateSyntaxTokenReader : SyntaxTokenReader {
		/// <summary>
		/// Initializes a new instance of the <see cref="NegateSyntaxTokenReader"/> class.
		/// </summary>
		public NegateSyntaxTokenReader() {
			Priority = Priorities.Negate;
		}

		public override Tree<SyntaxToken> Read(LinkedList<MixedToken> tokens, Grammar grammar) {
			// Do not create negate syntax token if we are able to create a subtract syntax token
			var lastNegateNode = tokens.FindLastNode(t => t.Value.IsLexicToken &&
				t.Value.LexicToken is SubtractToken && (t.Previous != null && !t.Previous.Value.IsTree || t.Previous == null));
			if (lastNegateNode != null) {
				var next = lastNegateNode.Next;
				if (next == null)
					throw new ParserException("Unexpected argument of 'negate' operator.");

				if (!next.Value.IsTree)
					throw new ParserException("Argument of 'negate' operator was not parsed.");

				NegateSyntaxToken token = new NegateSyntaxToken();
				Tree<SyntaxToken> tree = new Tree<SyntaxToken>(token);
				tree.Leafs.Add(next.Value.Tree);

				tokens.AddBefore(lastNegateNode, new MixedToken(tree));
				tokens.Remove(lastNegateNode);
				tokens.Remove(next);

				return tree;
			}

			return null;
		}

		public override int GetPosition(LinkedList<MixedToken> tokens) {
			// Do not create negate syntax token if we are able to create a subtract syntax token
			var lastNegateNode = tokens.FindLastNode(t => t.Value.IsLexicToken &&
				t.Value.LexicToken is SubtractToken && (t.Previous != null && !t.Previous.Value.IsTree || t.Previous == null));
			return tokens.IndexOf(lastNegateNode);
		}
	}
}
