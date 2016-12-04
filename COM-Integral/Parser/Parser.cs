using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MathParser
{
	public sealed class Parser : Component
	{

        public double Evaluate(string expression) {
            return Parse(expression).Evaluate();
        }

        private Grammar grammar = new Grammar();
        //private readonly ParameterExpressionEqualityComparer comparer = new ParameterExpressionEqualityComparer();

        public Parser() { }

		public Parser(params string[] parameters)
		{
			if (parameters == null)
				throw new ArgumentNullException("parameters");

			foreach (var parameter in parameters)
			{
				if (String.IsNullOrEmpty(parameter))
					throw new ArgumentNullException("parameter");

				grammar.Parameters.Add(parameter);
			}
		}		

		public ParsingResult Parse(string expression)
		{
            if (grammar == null) {
                Console.WriteLine("Parser component already disposed!");
            }

			InputStream input = new InputStream(expression);
			var tokens = grammar.Parse(input);
			var filteredTokens = grammar.Filter(tokens);
			var mixedTokens = grammar.ConvertToMixed(filteredTokens);
			var ast = grammar.CreateAST(mixedTokens);

			if (mixedTokens.Count != 1)
				throw new ParserException("Wrong expression.");

			var optimizedAst = ast.Optimize();

			var result = new ParsingResult
			{
				Tree = optimizedAst,
				ParameterNames = grammar.Parameters,
				ParameterExpressions = grammar.ParameterExpressions.Values.ToArray()
			};

            
			return result;
		}

        ~Parser () {
            Dispose(false);
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }


    }
}
