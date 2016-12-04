using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
	public sealed class EvaluationContext
	{
		public EvaluationContext()
		{
		}

		private readonly ParameterCollection parameters = new ParameterCollection();
		public ParameterCollection Parameters
		{
			get { return parameters; }
		}

		private static readonly EvaluationContext empty = new EvaluationContext();
		public static EvaluationContext Empty
		{
			get { return empty; }
		} 
	}
}
