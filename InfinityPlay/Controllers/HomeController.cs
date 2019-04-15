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

        public PartialViewResult Home()
        {
            return PartialView("Home");
        }

        public ActionResult Artists()
        {
            return PartialView("Artists");
        }

        public ActionResult Albums()
        {
            return PartialView("Albums");
        }

        public ActionResult Search()
        {
            return PartialView("Search");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View("~/Shared/Error.cshtml");
        }

        /* ---------------------------------Get Album Rating----------------------------------
        public ActionResult GetAlbumRating(int albumId)
        {
            List<DataRow> rows = DbHelper.Query($"SELECT STAR_RATINGS FROM RATINGS WHERE ALBUM_ID = {albumId}");

            if (rows == null || rows.Count() == 0)
            {
                throw new Exception("No Ratings For This Item Yet");
            }

            List<int> ratings = new List<int>();

            foreach (DataRow row in rows)
            {
                int rating = (int)row.ItemArray[0];
                ratings.Add(rating);
            }

            double rAverage = ratings.Average();
            return Content(rAverage.ToString());
        }

        // ---------------------------------Get Artist Data----------------------------------
        // public Actionresult ArtistData(int artistid)
        // {
        //    List<DataRow> rows = DbHelper.Query($"select star_ratings from ratings where track_id = {artistid}");

        //    if (rows == null || rows.Count() == 0)
        //    {
        //        throw new Exception("no ratings for this item yet");
        //    }

        //    List<int> ratings = new List<int>();

        //    foreach (DataRow row in rows)
        //    {
        //        int rating = rows.Count();
        //        ratings.Add(rating);
        //    }

        //    double rAverage = ratings.Average();
        //    return Content(rAverage.ToString());
        //}

        // ---------------------------------Get Track Data----------------------------------
        public ActionResult GetTrackData()
        {
            string trackinfo = string.Empty;

            List<string> stringList = new List<string>();

            List<DataRow> rows = DbHelper.Query("SELECT * FROM TRACKS");

            Debug.WriteLine("Number of rows: " + rows.Count());
            int i = 0;

            foreach (DataRow row in rows)
            {
                foreach (var item in row.ItemArray)
                {
                    string getinfo = item.ToString();
                    Debug.WriteLine(getinfo);
                    stringList.Add(getinfo);
                }

                Debug.WriteLine(" | ");
                i = i + 1;
            }

            trackinfo = string.Join(" | ", stringList);
            return Content(trackinfo);
        }*/
    }
}