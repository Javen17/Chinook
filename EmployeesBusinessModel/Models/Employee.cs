using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chinook.BusinessModel.Models
{
    public class Employee : NamedKeyedEntity
    {
        public long EmployeeId { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string JobPosition { get; set; }

        public long? DirectBossEmployeeId { get; set; }

        [ForeignKey("DirectBossEmployeeId")]
        public Employee DirectBoss { get; set; }
        public string Email { get; set; }
        public override string Name { get ; set ; }

        
        [NotMapped]
        public override long Key { get { return this.EmployeeId; } set { this.EmployeeId = value; } }
    }
}
