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

        public ActionResult Test()
        {
            string ret = "Hello...\r\n";

            List<DataRow> rows = DbHelper.Query("SELECT * FROM TRACKS");

            // Tracks is a collection of datarows, each of which is a collection of fields (column values)

            // Write how many rows there are into the console
            Debug.WriteLine("Number of rows: " + rows.Count());

            // Start on index 0
            int i = 0;

            // Do something with each row of all the rows...
            foreach (DataRow row in rows)
            {
                // Debug.WriteLine("Row at index: " + i);
                // Write how many column values there are in this row:
                // Debug.WriteLine("Number of items: " + row.ItemArray.Count());
                foreach (var r in row.ItemArray)
                {
                    string printdata = " | " + r.ToString();
                    Debug.Write(printdata);
                    ret += printdata;
                }

                Debug.WriteLine(" ");

                // Increment "i" by 1
                i = i + 1;
            }

            return Content(ret);
        }
    }
}