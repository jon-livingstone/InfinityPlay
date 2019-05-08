using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static InfinityPlay.Models.HomeModels;

namespace InfinityPlay.Controllers
{
    public class HomeController : Controller
    {
        private static Random rnd = new Random();

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
            return View("Search");
        }

        // ----- Album Details
        [Route("Partial/Album/{albumId}")]
        public ActionResult AlbumDetailsPartial(int albumId)
        {
            var album = AlbumData(albumId);
            return PartialView("AlbumDetails", album);
        }

        [Route("Album/{albumId}")]
        public ActionResult AlbumDetails(int albumId)
        {
            var album = AlbumData(albumId);
            return View("AlbumDetails", album);
        }

        // ---------- PRIVATE METHODS ------------ //
        private ALBUM AlbumData(int albumId)
        {
            var album = new ALBUM();

            var rows = DbHelper.Query("SELECT * FROM ALBUMS JOIN TRACKS ON TRACKS.ALBUM_ID = ALBUMS.ALBUM_ID JOIN ARTISTS ON TRACKS.ARTIST_ID = ARTISTS.ARTIST_ID WHERE ALBUMS.ALBUM_ID =" + albumId);

            album.ALBUM_ART = (string)rows[0]["ALBUM_ART"];
            album.ALBUM_NAME = (string)rows[0]["ALBUM_NAME"];
            album.BAND_NAME = (string)rows[0]["BAND_NAME"];
            album.RELEASE_YEAR = (int)rows[0]["RELEASE_YEAR"];

            foreach (var row in rows)
            {
                var track = new TRACK
                {
                    TRACK_FILE = (string)row["TRACK_FILE"],
                    TRACK_NAME = (string)row["TRACK_NAME"],
                    DURATION = (int)row["DURATION"],
                    TRACK_NUMBER = (int)row["TRACK_NUMBER"],

                    Artist = new ARTIST()
                };

                track.Artist.ARTIST_IMG = (string)row["ARTIST_IMG"];
                track.Artist.ARTIST_NAME = (string)row["ARTIST_NAME"];
                track.Artist.Tracks.Add(track);

                track.Album = album;

                album.Tracks.Add(track);
            }

            return album;
        }

        // ---------- PRIVATE METHODS ------------//
        // Home -----------------------------------
        private HomePageModel GetPageModel()
        {
            var model = new HomePageModel
            {
                Artist = GetRandomArtist(),
                Albums = GetTopAlbums1()
            };
            return model;
        }

        private List<ALBUM> GetTopAlbums1()
        {
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

        // private List<ALBUM> GetTopAlbums2()
        // {
        //    List<ALBUM> organize = new List<ALBUM>();
        //    try
        //    {
        //        var rows = DbHelper.Query("SELECT TOP 2 RELEASE_YEAR, ALBUM_NAME, ALBUM_ART, BAND_NAME FROM ALBUMS ORDER BY RELEASE_YEAR DESC; ");

        // foreach (DataRow row in rows)
        //        {
        //            var album = new ALBUM
        //            {
        //                RELEASE_YEAR = (int)row["RELEASE_YEAR"],
        //                ALBUM_NAME = (string)row["ALBUM_NAME"],
        //                ALBUM_ART = (string)row["ALBUM_ART"],
        //                BAND_NAME = (string)row["BAND_NAME"]
        //            };

        // organize.Add(album);
        //        }

        // return organize;
        //    }
        //    catch (Exception)
        //    {
        //        return organize;
        //    }
        // }
        private ARTIST GetRandomArtist()
        {
            var artist = new ARTIST();

            var allArtists = AllArtistList();

            int r = rnd.Next(allArtists.Count);
            artist = allArtists[r];
            return artist;
        }

        // Artists ----------------------------------------
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

        // Albums -----------------------------------------
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
                    albumList.RECORD_LABEL = (string)row["RECORD_LABEL"];
                    list.Add(albumList);
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }

        // Track List -----------------------------------------
        private List<TRACK> AllTrackList()
        {
            List<TRACK> list = new List<TRACK>();
            try
            {
                var rows = DbHelper.Query("SELECT * FROM TRACK");

                foreach (DataRow row in rows)
                {
                    var trackList = new TRACK();
                    trackList.TRACK_NAME = (string)row["TRACK_NAME"];
                    trackList.DURATION = (int)row["DURATION"];
                    trackList.TRACK_NUMBER = (int)row["TRACK_NUMBER"];
                    trackList.TRACK_FILE = (string)row["TRACK_FILE"];
                    list.Add(trackList);
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }
    }
}