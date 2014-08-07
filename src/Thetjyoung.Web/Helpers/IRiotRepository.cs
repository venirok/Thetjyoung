using Thetjyoung.Web.Models;

namespace Thetjyoung.Web.Helpers
{
    public interface IRiotRepository
    {
        Summoner GetSummonerByName(string name);
    }
}
