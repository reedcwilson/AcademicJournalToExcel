using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectModel
{
	public class ArticleInfo
	{
		public string Title { get; set; }
		public string Subject { get; set; }
		public string Abstract { get; set; }
		public string Keywords { get; set; }
		public string SessionId { get; set; }

		public override string ToString()
		{
			return string.Join("\t", Title, Subject, Abstract, Keywords);
		}
	}
}
