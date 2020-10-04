﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.BusinessModel.Models
{
    public class Album : NamedKeyedEntity
    {
        //some generic methods will expect this fields
        [NotMapped]
        public override long Key { get { return this.AlbumId; } set { this.AlbumId= value; }}
        [NotMapped]
        public override string Name { get { return this.Name; } set { this.Name = value; } }

        public long AlbumId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}