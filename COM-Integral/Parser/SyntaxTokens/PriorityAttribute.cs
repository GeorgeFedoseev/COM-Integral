using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser.SyntaxTokens
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class PriorityAttribute : Attribute
	{
		private readonly int priority;
		public int Priority
		{
			get { return priority; }
		}

		public PriorityAttribute(int priority)
		{
			this.priority = priority;
		}
	}
}
