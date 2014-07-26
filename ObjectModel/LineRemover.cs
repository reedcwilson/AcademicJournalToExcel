using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectModel
{
	public class LineRemover
	{
		public string Remove(string text)
		{
			var sb = new StringBuilder();
			int start = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '\r') // dos mode
				{
					var str = RemoveDash(text.Substring(start, i - start));
					sb.Append(str);
					start = i + 2;
					i++;
				}
				else if (text[i] == '\n') // unix mode
				{
					var str = RemoveDash(text.Substring(start, i - start));
					sb.Append(str);
					start = i + 1;
				}
				else if ((i + 1) == text.Length) // end of string
				{
					var str = text.Substring(start, i - start + 1);
					sb.Append(str);
				}
			}

			return sb.ToString().Trim().Replace("  ", " ");
		}

		private string RemoveDash(string p)
		{
			if (p[p.Length - 1] == '-')
			{
				return p.Substring(0, p.Length - 1);
			}
			return p + " ";
		}
	}
}
