using System;
using System.Collections.Generic;
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
            var albums = DbHelper.Query("SELECT * FROM ALBUMS");

            foreach (var album in albums)
            {
                Debug.WriteLine(album["BAND_NAME"]);
            }

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

            return this.View();
        }
    }
}