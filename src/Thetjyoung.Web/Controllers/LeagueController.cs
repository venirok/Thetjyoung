using System;
using System.Configuration;
using System.Threading.Tasks;
using PortableLeagueAPI;
using PortableLeagueApi.Interfaces.Enums;
using System.Web.Mvc;
using Thetjyoung.Web.Models;

namespace Thetjyoung.Web.Controllers
{
    public class LeagueController : Controller
    {
        #region Constructors
        #endregion Constructors

        #region Action Methods

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Summoner(SummonerLookupViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.SummonerId))
            {
                return View(viewModel);
            }
            var leagueApi = new LeagueApi(ConfigurationManager.AppSettings["LolApiKey"], RegionEnum.Na, true);
            viewModel.Summoner = await leagueApi.Summoner.GetSummonerByNameAsync(viewModel.SummonerId);

            return View(viewModel);
        }

        public ActionResult Stats()
        {
            return View();
        }

        public ActionResult Game()
        {
            return View();
        }

        #endregion ActionMethods

        #region Private Methods

        #endregion Private Methods
    }
}
