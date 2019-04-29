using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfinityPlay.Models
{
    public class HomeModels
    {
        public class ARTISTS
        {
            [Key]
            public int ARTIST_ID { get; set; }

            public string ARTIST_NAME { get; set; }

            public int ALBUM_ID { get; set; }

            public string ARTIST_IMG { get; set; }

            public virtual COMMENTS COMMENTS { get; set; }

            public virtual TRACKS TRACKS { get; set; }
        }

        public class ALBUMS
        {
            [Key]
            public int ALBUM_ID { get; set; }

            public string ALBUM_NAME { get; set; }

            public string BAND_NAME { get; set; }

            public string ALBUM_ART { get; set; }

            public int RELEASE_YEAR { get; set; }

            public string RECORD_LABEL { get; set; }

            public virtual ARTISTS ARTISTS { get; set; }

            public virtual TRACKS TRACKS { get; set; }

            public virtual RATINGS RATINGS { get; set; }

            public virtual COMMENTS COMMENTS { get; set; }
        }

        public class TRACKS
        {
            [Key]
            public int TRACK_ID { get; set; }

            public char TRACK_NAME { get; set; }

            public int TRACK_NUMBER { get; set; }

            public int DURATION { get; set; }

            public virtual COMMENTS COMMENTS { get; set; }

            public virtual ICollection<ALBUMS> ALBUMs { get; set; }

            public virtual ICollection<ARTISTS> ARTISTs { get; set; }
        }

        public class COMMENTS
        {
            [Key]
            public int COMMENT_ID { get; set; }

            public char USERNAME { get; set; }

            public char EMAIL { get; set; }

            public char COMMENT { get; set; }

            public DateTime CREATED_AT { get; set; }

            public virtual ICollection<ALBUMS> ALBUMs { get; set; }

            public virtual ICollection<TRACKS> TRACKs { get; set; }

            public virtual ICollection<ARTISTS> ARTISTs { get; set; }
        }

        public class RATINGS
        {
            [Key]
            public int RATINGS_ID { get; set; }

            public int STAR_RATINGS { get; set; }

            public virtual ICollection<ALBUMS> ALBUMs { get; set; }
        }
    }
}