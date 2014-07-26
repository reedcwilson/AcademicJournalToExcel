using ObjectModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
	public class HomeController : Controller
	{

		public ActionResult Index()
		{
			ViewBag.Message = "Paste the info page below.";

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		//
		// Post

		string file = "~/App_Data/Munger.db3";
		[HttpPost]
		public ActionResult Munge(string journalTxt)
		{
			ViewBag.Message = "Paste the info page below.";
			var munger = new Munger(Server.MapPath(file), ConfigurationManager.ConnectionStrings["SqliteConnectionStr"].ConnectionString);
			munger.Munge(journalTxt, Session.Contents.SessionID);
			return View("~/Views/Home/Index.cshtml");
		}

		public ActionResult Copy()
		{
			ViewBag.Message = "Go back to ~/Index to start over";
			var munger = new Munger(Server.MapPath(file), ConfigurationManager.ConnectionStrings["SqliteConnectionStr"].ConnectionString);
			var rows = munger.GetCopy(Session.Contents.SessionID);
			ViewBag.Rows = rows;
			munger.Clear(Session.Contents.SessionID);
			return View();
		}

		public ActionResult LineBreaks()
		{
			ViewBag.Message = "Copy the text below and press 'Remove' to cleanup your text";
			ViewBag.Text = "";
			return View();
		}

		public ActionResult RemoveLineBreaks(string text)
		{
			ViewBag.Message = "Feel free to try again.";
			var str = new LineRemover().Remove(text);
			ViewBag.Text = str;
			return View("~/Views/Home/LineBreaks.cshtml");
		}
	}
}
