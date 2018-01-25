using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class Match
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Token { get; set; }
        public int NumbOfPlayers { get; set; }
        public virtual Category Category { get; set; }
    }
}
