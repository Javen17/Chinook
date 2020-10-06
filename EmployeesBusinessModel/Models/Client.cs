﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chinook.BusinessModel.Models
{
    class Client : NamedKeyedEntity
    {
        public long ClientId { set; get; }
        public string LastName { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public override string Name { get; set; }
        [NotMapped]
        public override long Key { get { return this.ClientId; } set { this.ClientId = value; } }
    }
}
