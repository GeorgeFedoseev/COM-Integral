using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParser.LexicTokens;
using MathParser.SyntaxTokens;

namespace MathParser.SyntaxTokenReaders {
	public sealed class FunctionCallSyntaxTokenReader : SyntaxTokenReader {
		public FunctionCallSyntaxTokenReader() {
			Priority = Priorities.FunctionCall;
		}

		public override Tree<SyntaxToken> Read(LinkedList<MixedToken> tokens, Grammar grammar) {
			var funcNode = tokens.FindLast(t => t.IsLexicToken && t.LexicToken is FunctionCallToken);
			if (funcNode != null) {
				FunctionCallToken funcCallToken = (FunctionCallToken)funcNode.Value.LexicToken;
				var next = funcNode.Next;
				if (next != null) {
					var nextValue = next.Value;
					if (nextValue.IsTree) {
						UnaryStaticFunctionSyntaxToken token = new UnaryStaticFunctionSyntaxToken(funcCallToken.Method);
						Tree<SyntaxToken> tree = new Tree<SyntaxToken>(token);
						tree.Leafs.Add(next.Value.Tree);

						tokens.AddBefore(funcNode, new MixedToken(tree));
						tokens.Remove(funcNode);
						tokens.Remove(next);

						return tree;
					}
				}
			}
			return null;
		}

		public override int GetPosition(LinkedList<MixedToken> tokens) {
			var funcNode = tokens.FindLast(t => t.IsLexicToken && t.LexicToken is FunctionCallToken);

			return tokens.IndexOf(funcNode);
		}
	}
}
