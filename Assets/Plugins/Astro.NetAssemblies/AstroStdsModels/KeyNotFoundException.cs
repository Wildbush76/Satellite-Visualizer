using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models
{
   public class KeyNotFoundException : Exception
   {
      public long Key { get; }

      public KeyNotFoundException() { }

      public KeyNotFoundException(string message)
          : base(message) { }

      public KeyNotFoundException(string message, Exception inner)
          : base(message, inner) { }

      public KeyNotFoundException(string message, long key)
          : this(message)
      {
         Key = key;
      }
   }
}
