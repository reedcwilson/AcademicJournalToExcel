using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectModel
{
	public class Munger
	{
		List<ArticleInfo> _rows;
		SQLiteConnection _conn;

		public int Items { get; set; }
		public Munger(string file = null, string connectionString = null)
		{
			_rows = new List<ArticleInfo>();
			Initialize(file, connectionString);
		}

		private void Initialize(string file, string connectionString)
		{
			if (connectionString != null && file != null)
			{
				if (!File.Exists(file))
				{
					SQLiteConnection.CreateFile(file);
				}
				string sql = "create table if not exists munged (Id integer not null primary key autoincrement, Title varchar(1500), Subject varchar(1500), Abstract varchar(1500), Keywords varchar(1500), SessionId varchar(100))";
				_conn = new SQLiteConnection(string.Format(connectionString, file));
				_conn.Open();
				using (var command = new SQLiteCommand(sql, _conn))
				{
					command.ExecuteNonQuery();
				}
				_conn.Close();
			}
		}

		public void Munge(string str, string sessionId = null)
		{
			Items++;
			var raw = str.Replace("\n", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
			var index = Math.Min(raw.IndexOf('.'), raw.IndexOf('?'));
			var title = raw.Substring(0, index + 1).Trim();
			var subject = GetSubject(raw);
			var abs = GetAbstract(raw);
			var keywords = GetKeywords(raw);
			_rows.Add(new ArticleInfo { Title = title, Subject = subject, Abstract = abs, Keywords = keywords });
			if (_conn != null && sessionId != null)
			{
				_conn.Open();
				string sql = "insert into munged (Title, Keywords, Subject, Abstract, SessionId) values (@Title, @Keywords, @Subject, @Abstract, @SessionId)";
				using (var command = new SQLiteCommand(sql, _conn))
				{
					command.Parameters.Add(new SQLiteParameter("@Title", title));
					command.Parameters.Add(new SQLiteParameter("@Keywords", keywords));
					command.Parameters.Add(new SQLiteParameter("@Subject", subject));
					command.Parameters.Add(new SQLiteParameter("@Abstract", abs));
					command.Parameters.Add(new SQLiteParameter("@SessionId", sessionId));
					command.ExecuteScalar();
				}
				_conn.Close();
			}
		}

		private string GetSubject(string raw)
		{
			var begin = raw.IndexOf("Subjects:") + "Subjects:".Length;
			var end = raw.IndexOf("Classification:") - begin;
			return raw.Substring(begin, end).Trim();
		}

		private string GetAbstract(string raw)
		{
			var begin = raw.IndexOf("Abstract:") + "Abstract:".Length;
			var end = raw.IndexOf("Subjects:") - begin;
			return raw.Substring(begin, end).Trim();
		}

		private string GetKeywords(string raw)
		{
			var begin = raw.IndexOf("Keywords:") + "Keywords:".Length;
			var end = raw.IndexOf("Abstract:") - begin;
			return raw.Substring(begin, end).Trim();
		}

		private List<ArticleInfo> GetRows(string sessionId)
		{
			_conn.Open();
			string sql = "select * from munged where SessionId=@SessionId";
			var rows = new List<ArticleInfo>();
			using (var command = new SQLiteCommand(sql, _conn))
			{
				command.Parameters.Add(new SQLiteParameter("@SessionId", sessionId));
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					rows.Add(new ArticleInfo { Title = reader["Title"] as string, Abstract = reader["Abstract"] as string, Keywords = reader["Keywords"] as string, Subject = reader["Subject"] as string, SessionId = reader["SessionId"] as string });
				}
				_conn.Close();
			}
			return rows;
		}

		public override string ToString()
		{
			return GetCopy();
		}

		public string GetCopy(string sessionId = null)
		{
			var sb = new StringBuilder();
			int i = 0;
			List<ArticleInfo> rows = null;
			if (_conn != null)
			{
				rows = GetRows(sessionId);
			}
			else
			{
				rows = _rows;
			}
			foreach (var row in rows)
			{
				sb.Append(row.ToString());
				if (i++ < rows.Count - 1)
				{
					sb.Append("\n");
				}
			}
			return sb.ToString();
		}

		public void Clear(string sessionId = null)
		{
			_rows.Clear();
			DropMungedTable(sessionId);
			Items = 0;
		}

		private void DropMungedTable(string sessionId)
		{
			if (_conn != null)
			{
				_conn.Open();
				string sql = "drop table if exists munged";
				using (var command = new SQLiteCommand(sql, _conn))
				{
					command.ExecuteNonQuery();
				}
				_conn.Close();
			}
		}
	}
}
