using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chinook.BusinessModel.Models
{
    class InvoiceDetail : KeyedEntity
    {
        public long InvoiceDetailId { get; set; }
        public int Quantity { get; set; }
        public long UnitPrice { get; set; }
        public Song Song { get; set; }

        [NotMapped]
        public override long Key { get { return this.InvoiceDetailId; } set { this.InvoiceDetailId = value; } }
    }
}
