using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Domain;

namespace gameAPI
{
    public class MatchContract
    {
        public Match Match { get; set; }
        public Match_User Match_User { get; set; }
    }
}
