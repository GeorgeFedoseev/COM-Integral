using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MathParser
{
	public sealed class InputStream
	{
		private readonly string initialContent;

		private string content;
		public string Content
		{
			get { return content; }
		}

		public InputStream(string inputString)
		{
			if (String.IsNullOrEmpty(inputString))
				throw new ArgumentNullException("inputString");

			this.initialContent = inputString;
			this.content = inputString;
		}

		private int position = 0;
		public int Position
		{
			get { return position; }
		}

		public bool IsEmpty
		{
			get { return content.Length == 0; }
		}

		public InputStream Move(int shift)
		{
			var result = new InputStream(initialContent);
			result.position = position + shift;
			result.content = initialContent.Substring(result.position);

			return result;
		}

		public override string ToString()
		{
			return String.Format("{0}|{1}", initialContent.Substring(0, position), content);
		}

		[DebuggerStepThrough]
		public static implicit operator InputStream(string input)
		{
			return new InputStream(input);
		}

		public static implicit operator string(InputStream stream)
		{
			return stream.content;
		}
	}
}
