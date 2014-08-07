using System;

namespace Thetjyoung.Web.Models
{
    public class Summoner
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ProfileIconId { get; set; }
        public DateTime RevisionDate { get; set; }
        public long Level { get; set; }
        public MasteryPage[] MasteryPages { get; set; }
        public RunePage[] RunePages { get; set; }
        public ChampionStats[] RankedChampionStats { get; set; }
        public PlayerStatsSummary[] PlayerStatsSummaries { get; set; }
        public Game[] RecentGames { get; set; }
    }
}