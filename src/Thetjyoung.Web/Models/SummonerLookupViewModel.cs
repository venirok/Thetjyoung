using PortableLeagueApi.Interfaces.Summoner;

namespace Thetjyoung.Web.Models
{
    public class SummonerLookupViewModel
    {
        public string SummonerId { get; set; }
        public ISummoner Summoner { get; set; }
    }
}