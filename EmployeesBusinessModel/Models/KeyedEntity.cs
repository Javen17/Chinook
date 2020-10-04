using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BusinessModel.Models
{
   public abstract class KeyedEntity
    {
        public abstract long Key { get; set; }
    }
}
