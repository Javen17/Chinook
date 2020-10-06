using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chinook.BusinessModel.Models
{
    class Song : NamedKeyedEntity
    {
        public long SongId { set; get; }
        public override string Name { get ; set ; }

        [NotMapped]
        public override long Key { get { return this.SongId; } set { this.SongId = value; } }
        public Album Album { get; set; }
        public Genre Genre { get; set; }
    }
}
