using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chinook.BusinessModel.Models
{
    public class Artist:NamedKeyedEntity
    {
        //some generic methods will expect this fields
        [NotMapped]
        public override long Key { get { return this.ArtistId; } set { this.ArtistId = value; } }
        public override string Name { get; set; }

        public long ArtistId { get; set; }

        public long AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }

    }
}
