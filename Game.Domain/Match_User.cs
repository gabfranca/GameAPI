using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public  class Match_User
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int Pontuation { get; set; }
        public int BoardPosition { get; set; }
        public string Token { get; set; }
        public virtual User User { get; set; }
        public virtual Match Match { get; set; }


    }
}
