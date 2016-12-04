using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParser.SyntaxTokens;
using AST = MathParser.Tree<MathParser.SyntaxToken>;

namespace MathParser.SyntaxTokenReaders {
	public abstract class BinaryOpSyntaxTokenReader<TLexicToken, TSyntaxToken> : SyntaxTokenReader where TSyntaxToken : SyntaxToken, new() {
		private string opName;
		public string OpName {
			get { return opName; }
			set { opName = value; }
		}

		protected void VerifyNode(LinkedListNode<MixedToken> node, string opName) {
			if (node.Previous == null)
				throw new ParserException(String.Format("Operator '{0}': expected first parameter.", opName));
			if (node.Next == null)
				throw new ParserException(String.Format("Operator '{0}': expected second parameter.", opName));
		}

		public sealed override int GetPosition(LinkedList<MixedToken> tokens) {
			var node = tokens.FindFirst(t => t.IsLexicToken && t.LexicToken is TLexicToken);

			return tokens.IndexOf(node);
		}

		public sealed override AST Read(LinkedList<MixedToken> tokens, Grammar grammar) {
			var node = tokens.FindFirst(t => t.IsLexicToken && t.LexicToken is TLexicToken);
			if (node != null) {
				VerifyNode(node, OpName);

				var arg1 = node.Previous.Value;
				var arg2 = node.Next.Value;

				if (arg1.Tree == null)
					throw new ParserException(String.Format("Operation {0}: first parameter not parsed.", OpName));
				if (arg2.Tree == null)
					throw new ParserException(String.Format("Operation {0}: second parameter not parsed.", OpName));

				TSyntaxToken token = new TSyntaxToken();
				var tree = new AST(token);

				tree.Leafs.Add(arg1.Tree);
				tree.Leafs.Add(arg2.Tree);

				node.Value.Tree = tree;

				node.RemovePrevious();
				node.RemoveNext();

				return tree;
			}

			return null;
		}
	}

	public sealed class MultiplySyntaxTokenReader : BinaryOpSyntaxTokenReader<MultiplyToken, MultiplySyntaxToken> {
		public MultiplySyntaxTokenReader() {
			Priority = Priorities.Multiplication;
			OpName = "*";
		}
	}

	public sealed class DivideSyntaxTokenReader : BinaryOpSyntaxTokenReader<DivideToken, DivideSyntaxToken> {
		public DivideSyntaxTokenReader() {
			Priority = Priorities.Multiplication;
			OpName = "/";
		}
	}

	public sealed class AddSyntaxTokenReader : BinaryOpSyntaxTokenReader<AddToken, AddSyntaxToken> {
		public AddSyntaxTokenReader() {
			Priority = Priorities.Addition;
			OpName = "+";
		}
	}

	public sealed class SubtractSyntaxTokenReader : BinaryOpSyntaxTokenReader<SubtractToken, SubtractSyntaxToken> {
		public SubtractSyntaxTokenReader() {
			Priority = Priorities.Addition;
			OpName = "-";
		}
	}

	public sealed class PowerSyntaxTokenReader : BinaryOpSyntaxTokenReader<PowerToken, PowerSyntaxToken> {
		public PowerSyntaxTokenReader() {
			Priority = Priorities.Power;
			OpName = "**";
		}
	}
}
