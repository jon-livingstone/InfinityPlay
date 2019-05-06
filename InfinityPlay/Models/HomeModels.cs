﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfinityPlay.Models
{
    public class HomeModels
    {
        public class HomePageModel
        {
            public ARTIST Artist { get; set; }

            public List<ALBUM> Albums { get; set; }
        }

        public class TrackMetadataModel
        {
            public string AlbumArt { get; set; }

            public string ArtistName { get; set; }

            public string TrackName { get; set; }
        }

        public class ARTIST
        {
            [Key]
            public int ARTIST_ID { get; set; }

            public string ARTIST_NAME { get; set; }

            public int ALBUM_ID { get; set; }

            public string ARTIST_IMG { get; set; }
        }

        public class ALBUM
        {
            [Key]
            public int ALBUM_ID { get; set; }

            public string ALBUM_NAME { get; set; }

            public string BAND_NAME { get; set; }

            public string ALBUM_ART { get; set; }

            public int RELEASE_YEAR { get; set; }

            public string RECORD_YEAR { get; set; }
        }

        public class TRACK
        {
            [Key]
            public int TRACK_ID { get; set; }

            public char TRACK_NAME { get; set; }

            public int TRACK_NUMBER { get; set; }

            public int DURATION { get; set; }

            private string TRACK_FILE { get; set; }
        }

        public class COMMENT
        {
            [Key]
            public int COMMENT_ID { get; set; }

            public char USERNAME { get; set; }

            public char EMAIL { get; set; }

            public string COMMENT_TEXT { get; set; }

            public DateTime CREATED_AT { get; set; }
        }

        public class RATING
        {
            [Key]
            public int RATING_ID { get; set; }

            public int STAR_RATING { get; set; }
        }
    }
}