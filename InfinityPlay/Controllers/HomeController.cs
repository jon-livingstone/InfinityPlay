using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfinityPlay.Models;

namespace InfinityPlay.Controllers
{
    public class HomeController : Controller
    {
        // private static Random rnd = new Random();

        // ----- Index
        [Route("Partial/Index")]
        public ActionResult IndexPartial()
        {
            return PartialView("Index");
        }

        public ActionResult Index()
        {
            return this.View();
        }

        // ----- Home
        [Route("Partial/Home")]
        public ActionResult HomePartial()
        {
            return PartialView("Home");
        }

        public ActionResult Home()
        {
            return this.View();
        }

        // ----- Artists
        public ActionResult Artists()
        {
            var list = AllArtistList();
            return View("Artists", list);
        }

        [Route("Partial/Artists")]
        public ActionResult ArtistsPartial()
        {
            var list = AllArtistList();
            return PartialView("Artists", list);
        }

        // ----- Albums
        [Route("Partial/Albums")]
        public ActionResult AlbumsPartial()
        {
            var list = AllAlbumList();
            return PartialView("Albums", list);
        }

        public ActionResult Albums()
        {
            var list = AllAlbumList();
            return View("Albums", list);
        }

        // ----- Search
        [Route("Partial/Search")]
        public ActionResult SearchPartial()
        {
            return PartialView("Search");
        }

        public ActionResult Search()
        {
            return PartialView("Search");
        }

        // ---------- PRIVATE METHODS ------------//
        // Artists
        private List<HomeModels.ARTISTS> AllArtistList()
        {
            List<HomeModels.ARTISTS> list = new List<HomeModels.ARTISTS>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string sql = "SELECT * FROM ARTISTS";
                    con.ConnectionString = @"Server=LIVINGSTONEDT\SQLEXPRESS;Database=InfinityPlay;Trusted_Connection=True;MultipleActiveResultSets=true";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var artistList = new Models.HomeModels.ARTISTS();
                        artistList.ARTIST_NAME = (string)row["ARTIST_NAME"];
                        artistList.ARTIST_ID = (int)row["ARTIST_ID"];
                        artistList.ALBUM_ID = (int)row["ALBUM_ID"];
                        artistList.ARTIST_IMG = (string)row["ARTIST_IMG"];

                        list.Add(artistList);
                    }
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }

        // private HomeModels.ALBUMS GetRandomAlbum()
        // {
        //    HomeModels.ALBUMS album = new HomeModels.ALBUMS();

        // List<HomeModels.ALBUMS> allAlbums = AllAlbumList();

        // int r = rnd.Next(allAlbums.Count);

        // album = allAlbums[r];
        //    return album;
        // }

        // public string test()
        // {
        //    var album = GetRandomAlbum();

        // return album.ALBUM_NAME;
        // }

        // Albums
        private List<HomeModels.ALBUMS> AllAlbumList()
        {
            List<HomeModels.ALBUMS> list = new List<HomeModels.ALBUMS>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string sql = "SELECT * FROM ALBUMS";
                    con.ConnectionString = @"Server=LIVINGSTONEDT\SQLEXPRESS;Database=InfinityPlay;Trusted_Connection=True;MultipleActiveResultSets=true";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var albumList = new Models.HomeModels.ALBUMS();
                        albumList.ALBUM_ID = (int)row["ALBUM_ID"];
                        albumList.ALBUM_NAME = (string)row["ALBUM_NAME"];
                        albumList.ALBUM_ART = (string)row["ALBUM_ART"];
                        albumList.BAND_NAME = (string)row["BAND_NAME"];
                        albumList.RELEASE_YEAR = (int)row["RELEASE_YEAR"];
                        albumList.RECORD_LABEL = (string)row["RECORD_LABEL"];
                        list.Add(albumList);
                    }
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }

        private List<HomeModels.ALBUMS> AllALBUList()
        {
            List<HomeModels.ALBUMS> list = new List<HomeModels.ALBUMS>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string sql = "SELECT * FROM ALBUM";
                    con.ConnectionString = @"Server=LIVINGSTONEDT\SQLEXPRESS;Database=InfinityPlay;Trusted_Connection=True;MultipleActiveResultSets=true";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var albumList = new Models.HomeModels.ALBUMS();
                        albumList.ALBUM_ID = (int)row["ALBUM_ID"];
                        albumList.ALBUM_NAME = (string)row["ALBUM_NAME"];
                        albumList.ALBUM_ART = (string)row["ALBUM_ART"];
                        albumList.BAND_NAME = (string)row["BAND_NAME"];
                        albumList.RELEASE_YEAR = (int)row["RELEASE_YEAR"];
                        albumList.RECORD_LABEL = (string)row["RECORD_LABEL"];

                        list.Add(albumList);
                    }
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
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
        // public Actionresult ArtistData(int ArtistID)
        // {
        //    List<DataRow> rows = DbHelper.Query($"select star_ratings from ratings where track_id = {ArtistID}");

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