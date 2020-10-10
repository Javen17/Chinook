using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chinook.BusinessModel.Models
{
    public class InvoiceDetail : KeyedEntity
    {
        public long InvoiceDetailId { get; set; }

        public long InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public Song Song { get; set; }

        [NotMapped]
        public override long Key { get { return this.InvoiceDetailId; } set { this.InvoiceDetailId = value; } }
    }
}
