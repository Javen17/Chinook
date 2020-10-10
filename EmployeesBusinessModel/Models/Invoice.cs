using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chinook.BusinessModel.Models
{
    public class Invoice : NamedKeyedEntity
    {
        public long InvoiceId { get; set; }
        public Client Client { get; set; }
        public DateTime InvoiceDate { get; set; }
        public float Total { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        [NotMapped]
        public override string Name { get ; set ; }
        [NotMapped]
        public override long Key { get { return this.InvoiceId; } set { this.InvoiceId = value; } }
    }
}
