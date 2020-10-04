using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BusinessModel.Models
{
   public abstract class NamedKeyedEntity:KeyedEntity
    {
      public abstract string Name { get; set; }  
    }
}
