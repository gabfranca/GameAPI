using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class ChallengeQuestion
    {

        public int Id { get; set; }
        public string Question { get; set; }
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
        public string OptionThree { get; set; }
        public string OptionFour { get; set; }
        public int Asnwer { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public int CreatedBy { get; set; }

        public override string ToString()
        {
            return this.Question;
        }
    }
}
