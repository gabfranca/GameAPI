using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
     public class Session
    {
        
        
        public int? Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public bool IsActive { get; set; }

       

    }
}
