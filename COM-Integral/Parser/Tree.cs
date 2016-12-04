using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MathParser
{
	public class Tree<T>
	{
		public Tree(T value)
		{
			this.value = value;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private T value;
		public T Value
		{
			get { return value; }
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly List<Tree<T>> leafs = new List<Tree<T>>();
		public List<Tree<T>> Leafs
		{
			get { return leafs; }
		} 

		public bool IsLeaf
		{
			get { return leafs.Count == 0; }
		}
	}
}
