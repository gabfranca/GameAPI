using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
   public class BonusQuestion
    {
        public int Id { get; set; }
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
        public string OptionThree { get; set; }

        public int SumResult { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int CreatedBy { get; set; }
    }
}
