using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MathParser
{
	internal sealed class ParameterExpressionEqualityComparer : IEqualityComparer<ParameterExpression>
	{

		#region IEqualityComparer<ParameterExpression> Members

		public bool Equals(ParameterExpression x, ParameterExpression y)
		{
			return x.Name.Equals(y.Name);
		}

		public int GetHashCode(ParameterExpression obj)
		{
			return obj.Name.GetHashCode();
		}

		#endregion
	}
}
