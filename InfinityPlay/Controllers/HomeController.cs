using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfinityPlay.Models;
using static InfinityPlay.Models.HomeModels;

namespace InfinityPlay.Controllers
{
    public class HomeController : Controller
    {
        // ----- Index
        [Route("Partial/Index")]
        public ActionResult IndexPartial()
        {
            var model = GetPageModel();
            return PartialView("Home", model);
        }

        public ActionResult Index()
        {
            var model = GetPageModel();
            return View("Home", model);
        }

        // ----- Home
        [Route("Partial/Home")]
        public ActionResult HomePartial()
        {
            var model = GetPageModel();
            return PartialView("Home", model);
        }

        public ActionResult Home()
        {
            var model = GetPageModel();
            return this.View("Home", model);
        }

        // ----------------------------------------------------------------
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

        // ----- Albums List
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

        // --- Track Data
        public JsonResult GetNowPlayingTrackData(string trackGUID)
        {
             var row = DbHelper.Query(@"SELECT ALBUMS.ALBUM_ART, ARTISTS.ARTIST_NAME, TRACKS.TRACK_NAME
                                        FROM ALBUMS 
                                        JOIN TRACKS ON TRACKS.ALBUM_ID = ALBUMS.ALBUM_ID
                                        JOIN ARTISTS ON TRACKS.ARTIST_ID = ARTISTS.ARTIST_ID
                                        WHERE TRACKS.TRACK_FILE = '" + trackGUID + "';").First();

            var trackInfo = new TrackMetadataModel();
            trackInfo.AlbumArt = (string)row["ALBUM_ART"];
            trackInfo.ArtistName = (string)row["ARTIST_NAME"];
            trackInfo.TrackName = (string)row["TRACK_NAME"];

            return new JsonResult() { Data = trackInfo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // ---------- PRIVATE METHODS ------------//

        // Home -----------------------------------
        private HomePageModel GetPageModel()
        {
            var model = new HomePageModel();
            model.Artist = GetRandomArtist();
            model.Albums = GetTopAlbums1();
            return model;
        }

        private List<ALBUM> GetTopAlbums1()
        {
            // Sort all albums by date decending, and return the first two
            // Do it "the C# way"
            try
            {
                return AllAlbumList().OrderByDescending(a => a.RELEASE_YEAR).Take(5).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }

        private List<ALBUM> GetTopAlbums2()
        {
            // Sort all albums by date decending, and return the first two
            // Do it "the SQL way"
            List<ALBUM> organize = new List<ALBUM>();
            try
            {
                var rows = DbHelper.Query("SELECT TOP 2 RELEASE_YEAR, ALBUM_NAME, ALBUM_ART, BAND_NAME FROM ALBUMS ORDER BY RELEASE_YEAR DESC; ");

                foreach (DataRow row in rows)
                {
                    var album = new ALBUM();
                    album.RELEASE_YEAR = (int)row["RELEASE_YEAR"];
                    album.ALBUM_NAME = (string)row["ALBUM_NAME"];
                    album.ALBUM_ART = (string)row["ALBUM_ART"];
                    album.BAND_NAME = (string)row["BAND_NAME"];

                    organize.Add(album);
                }

                return organize;
            }
            catch (Exception)
            {
                return organize;
            }
        }

        private static Random rnd = new Random();

        private ARTIST GetRandomArtist()
        {
            var artist = new ARTIST();

            var allArtists = AllArtistList();

            int r = rnd.Next(allArtists.Count);

            artist = allArtists[r];
            return artist;
        }

        // Artist List ----------------------------------------
        private List<ARTIST> AllArtistList()
        {
            var list = new List<ARTIST>();
            try
            {
                var rows = DbHelper.Query("SELECT * FROM ARTISTS");

                foreach (DataRow row in rows)
                {
                    var artist = new ARTIST();
                    artist.ARTIST_NAME = (string)row["ARTIST_NAME"];
                    artist.ARTIST_ID = (int)row["ARTIST_ID"];
                    artist.ALBUM_ID = (int)row["ALBUM_ID"];
                    artist.ARTIST_IMG = (string)row["ARTIST_IMG"];

                    list.Add(artist);
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }

        // Albums List -----------------------------------------
        private List<ALBUM> AllAlbumList()
        {
            List<ALBUM> list = new List<ALBUM>();
            try
            {
                var rows = DbHelper.Query("SELECT * FROM ALBUMS");

                foreach (DataRow row in rows)
                {
                    var albumList = new ALBUM();
                    albumList.ALBUM_ID = (int)row["ALBUM_ID"];
                    albumList.ALBUM_NAME = (string)row["ALBUM_NAME"];
                    albumList.ALBUM_ART = (string)row["ALBUM_ART"];
                    albumList.BAND_NAME = (string)row["BAND_NAME"];
                    albumList.RELEASE_YEAR = (int)row["RELEASE_YEAR"];
                    albumList.RECORD_YEAR = (string)row["RECORD_LABEL"];
                    list.Add(albumList);
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }

        // MediaPlayer Album Info
        private HomePageModel MediaPlayerInfo()
        {
            var model = new HomePageModel();
            model.Artist = GetRandomArtist();
            // model.Albums = SelectedAlbum();
            return model;
        }

        // private List<ALBUM> SelectedAlbum()
        // {
        //    try
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        Debug.WriteLine(ex.StackTrace);
        //        return null;
        //    }
        // }

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