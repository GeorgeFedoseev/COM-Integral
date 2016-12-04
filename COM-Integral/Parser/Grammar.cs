using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParser.Readers;
using MathParser.SyntaxTokenReaders;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using AST = MathParser.Tree<MathParser.SyntaxToken>;
using System.Reflection;

namespace MathParser {
	public class Grammar {
		private readonly List<TokenReader> lexicReaders = new List<TokenReader>();
		private readonly List<SyntaxTokenReader> syntaxReaders = new List<SyntaxTokenReader>();

		private ParameterTokenReader parameterReader = new ParameterTokenReader();
		private NamedConstantTokenReader namedConstantReader = new NamedConstantTokenReader();
		private FunctionCallTokenReader functionReader = new FunctionCallTokenReader();

		public Grammar() {
			lexicReaders.Add(new DoubleReader());
			lexicReaders.Add(new CharReader('+', new AddToken()));
			lexicReaders.Add(new CharReader('-', new SubtractToken()));
			lexicReaders.Add(new StringReader("**", new PowerToken()));
			lexicReaders.Add(new CharReader('*', new MultiplyToken()));
			lexicReaders.Add(new CharReader('/', new DivideToken()));
			lexicReaders.Add(new CharReader('(', new LeftBracketToken()));
			lexicReaders.Add(new CharReader(')', new RightBracketToken()));
			lexicReaders.Add(new WhitespaceReader());
			lexicReaders.Add(parameterReader);
			lexicReaders.Add(functionReader);
			lexicReaders.Add(namedConstantReader);

			syntaxReaders.Add(new AddSyntaxTokenReader());
			syntaxReaders.Add(new ConstantSyntaxTokenReader());
			syntaxReaders.Add(new MultiplySyntaxTokenReader());
			syntaxReaders.Add(new DivideSyntaxTokenReader());
			syntaxReaders.Add(new SubtractSyntaxTokenReader());
			syntaxReaders.Add(new BracketSyntaxTokenReader());
			syntaxReaders.Add(new ParameterSyntaxTokenReader());
			syntaxReaders.Add(new FunctionCallSyntaxTokenReader());
			syntaxReaders.Add(new PowerSyntaxTokenReader());
			syntaxReaders.Add(new NegateSyntaxTokenReader());

			namedConstants.Add(new NamedConstant("Pi", Math.PI));
			namedConstants.Add(new NamedConstant("PI", Math.PI));
			namedConstants.Add(new NamedConstant("pi", Math.PI));

			namedConstants.Add(new NamedConstant("E", Math.E));
			namedConstants.Add(new NamedConstant("e", Math.E));

			RegisterStaticFunctions(typeof(Math));
			RegisterStaticFunctions(typeof(Math), m => m.Name.ToLower());
		}

		private readonly Collection<string> parameters = new Collection<string>();
		public Collection<string> Parameters {
			get { return parameters; }
		}

		private readonly NamedConstantCollection namedConstants = new NamedConstantCollection();
		public NamedConstantCollection NamedConstants {
			get { return namedConstants; }
		}

		private readonly Collection<StaticFunction> registeredFunctions = new Collection<StaticFunction>();
		public Collection<StaticFunction> RegisteredFunctions {
			get { return registeredFunctions; }
		}

		public void AddNamedConstant(string name, double value) {
			namedConstants.Add(new NamedConstant(name, value));
		}

		public void RegisterStaticFunction(string name, MethodInfo method) {
			if (String.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			if (method == null)
				throw new ArgumentNullException("method");

			registeredFunctions.Add(new StaticFunction { Name = name, Method = method });
		}

		public void RegisterStaticFunction(MethodInfo method) {
			if (method == null)
				throw new ArgumentNullException("method");

			registeredFunctions.Add(new StaticFunction { Name = method.Name, Method = method });
		}

		public void RegisterStaticFunctions(Type type) {
			RegisterStaticFunctions(type, m => m.Name);
		}

		public void RegisterStaticFunctions(Type type, Func<MethodInfo, string> nameSelector) {
			if (type == null)
				throw new ArgumentNullException("type");
			if (nameSelector == null)
				throw new ArgumentNullException("nameSelector");

			var methods = from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public)
						  where !method.ContainsGenericParameters
						  where method.ReturnType == typeof(double)
						  let parameters = method.GetParameters()
						  where parameters.Length == 1 && parameters[0].ParameterType == typeof(double)
						  select method;

			foreach (var method in methods) {
				RegisterStaticFunction(nameSelector(method), method);
			}
		}

		private readonly Dictionary<string, ParameterExpression> parameterExpressions = new Dictionary<string, ParameterExpression>();
		public Dictionary<string, ParameterExpression> ParameterExpressions {
			get { return parameterExpressions; }
		}

		private readonly ObservableCollection<TokenInTextInfo> textInfo = new ObservableCollection<TokenInTextInfo>();
		public ObservableCollection<TokenInTextInfo> TextInfo {
			get { return textInfo; }
		}

		public IEnumerable<LexicToken> Parse(InputStream input) {
			textInfo.Clear();
			parameterExpressions.Clear();

			// sorting to prevent situation when parameter 'x1' is parsed as parameter 'x' when 'x' is available, too.
			parameterReader.ParameterNames = parameters.OrderByDescending(s => s.Length);
			namedConstantReader.Constants = NamedConstants;
			functionReader.Functions = registeredFunctions;

			List<LexicToken> tokens = new List<LexicToken>();

			int start = 0;
			do {
				var copy = input;

				foreach (var reader in lexicReaders) {
					LexicToken token = null;

					if (input.IsEmpty)
						break;

					start = input.Position;
					input = reader.TryRead(input, out token);

					if (token != null) {
						TokenInTextInfo info = new TokenInTextInfo { StartIndex = start, Length = input.Position - start, Token = token };
						textInfo.Add(info);

						tokens.Add(token);
					}
				}

				if (input == copy)
					throw new ParserException(String.Format("Unexpected character '{0}' in position {1}.", input.Content[0], input.Position));

			} while (!input.IsEmpty);

			return tokens;
		}

		// todo add external plugging-in filters for situations like '2x' or '2cos x'
		public IEnumerable<LexicToken> Filter(IEnumerable<LexicToken> tokens) {
			foreach (var token in tokens) {
				if (token is WhitespaceToken)
					continue;

				yield return token;
			}
		}

		public LinkedList<MixedToken> ConvertToMixed(IEnumerable<LexicToken> tokens) {
			return new LinkedList<MixedToken>(tokens.Select(t => new MixedToken(t)));
		}

		public AST CreateAST(LinkedList<MixedToken> tokens) {
			if (tokens.Count == 0)
				throw new ParserException("Empty tokens list.");

			if (tokens.Count == 1 && tokens.First.Value.IsTree)
				return tokens.First.Value.Tree;

			//syntaxReaders.Sort((r1, r2) => r1.Priority.CompareTo(r2.Priority));

			var readers = (from gr in
							   (from reader in syntaxReaders
								group reader by reader.Priority into @group
								orderby @group.Key
								select @group)
						   from reader in gr
						   let index = reader.GetPosition(tokens)
						   where index < Int32.MaxValue
						   orderby index
						   select new { reader, index })
								.OrderBy(x => x.reader.Priority).ThenBy(x => x.index);

			AST result = null;
			foreach (var x in readers) {
				AST tree = null;
				do {
					tree = x.reader.Read(tokens, this);

					if (tree != null) result = tree;
					else break;
				} while (tree != null);
			}

			foreach (var token in tokens) {
				if (!token.IsTree)
					throw new ParserException(String.Format("Unexpected token - {0}", token.LexicToken));
			}

			return result;
		}
	}
}
