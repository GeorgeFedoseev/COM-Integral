using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MathParser.TextModel
{
	public static class TextHelper
	{
		public static string GetText(FlowDocument flowDocument)
		{
			StringBuilder builder = new StringBuilder();

			GetText(builder, flowDocument.Blocks);

			return builder.ToString();
		}

		private static void GetText(StringBuilder builder, BlockCollection blocks)
		{
			foreach (var block in blocks)
			{
				Paragraph paragraph = block as Paragraph;
				if (paragraph != null)
				{
					GetText(builder, paragraph.Inlines);
				}

				Section section = block as Section;
				if (section != null)
				{
					GetText(builder, section.Blocks);
				}
			}
		}

		private static void GetText(StringBuilder builder, InlineCollection inlines)
		{
			foreach (var inline in inlines)
			{
				Run run = inline as Run;
				if (run != null)
				{
					builder.Append(run.Text);
				}

				Span span = inline as Span;
				if (span != null)
				{
					GetText(builder, span.Inlines);
				}
			}
		}
	}
}
