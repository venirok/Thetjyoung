using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thetjyoung.Web.Models.Enums;

namespace Thetjyoung.Web.Models
{
    public class Game
    {
        public int ChampionId { get; set; }
        public DateTime CreateDate { get; set; }
        public Player[] Players { get; set; }
        public GameMode GameMode { get; set; }
        public GameType GameType { get; set; }
        public bool Invalid { get; set; }
        public int IpEarned { get; set; }
        public int Level { get; set; }
        public int MapId { get; set; }
        public int Spell1 { get; set; }
        public int Spell2 { get; set; }
        public GameRawStats GameStats { get; set; }
        public SubGameType GameSubType { get; set; }
        public int TeamId { get; set; }
    }
}