using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectModel
{
	public class Munger
	{
		List<ArticleInfo> _rows;

		public int Items { get; set; }
		public Munger()
		{
			_rows = new List<ArticleInfo>();
		}

		public void Munge(string str)
		{
			Items++;
			var raw = str.Replace("\n", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
			var title = raw.Substring(0, raw.IndexOf('.') + 1);
			var subject = GetSubject(raw);
			var abs = GetAbstract(raw);
			var keywords = GetKeywords(raw);
			_rows.Add(new ArticleInfo { Title = title, Subject = subject, Abstract = abs, Keywords = keywords });
		}

		private static string GetSubject(string raw)
		{
			var begin = raw.IndexOf("Subjects:") + "Subjects:".Length;
			var end = raw.IndexOf("Classification:") - begin;
			return raw.Substring(begin, end);
		}

		private static string GetAbstract(string raw)
		{
			var begin = raw.IndexOf("Abstract:") + "Abstract:".Length;
			var end = raw.IndexOf("Subjects:") - begin;
			return raw.Substring(begin, end);
		}

		private static string GetKeywords(string raw)
		{
			var begin = raw.IndexOf("Keywords:") + "Keywords:".Length;
			var end = raw.IndexOf("Abstract:") - begin;
			return raw.Substring(begin, end);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			int i = 0;
			foreach (var row in _rows)
			{
				sb.Append(row.ToString());
				if (i++ < _rows.Count - 1)
				{
					sb.Append("\n");
				}
			}
			return sb.ToString();
		}

		public void Clear()
		{
			_rows.Clear();
			Items = 0;
		}
	}
}
