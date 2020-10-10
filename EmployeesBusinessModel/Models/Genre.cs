
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.BusinessModel.Models
{
    public class Genre : NamedKeyedEntity
    {
        public long GenreId { get; set; }
        public override string Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        [NotMapped]
        public override long Key { get { return this.GenreId; } set  { this.GenreId = value; } }
    }
}
