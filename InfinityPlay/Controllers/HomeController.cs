using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfinityPlay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message =
                "Your application description page.";
            if (true)
            {
                Console.Write("!");
                return this.View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View("~/Shared/Error.cshtml");
        }

        public string Test()
        {
            string ret = string.Empty;
            List<DataRow> tracks = DbHelper.Query("SELECT * FROM TRACKS");

            foreach (DataRow track in tracks)
            {
                Debug.WriteLine(track["TRACK_NAME"]);
            }

            ret = "Hello World!";

            return ret;
        }
    }
}