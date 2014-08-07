using System.Linq;
using PortableLeagueApi.Interfaces.Enums;
using PortableLeagueApi.Interfaces.Game;
using PortableLeagueApi.Interfaces.Stats;
using PortableLeagueApi.Interfaces.Summoner;
using PortableLeagueAPI;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Thetjyoung.Web.Models;
using Thetjyoung.Web.Models.Enums;

namespace Thetjyoung.Web.Helpers
{
    public class RiotRepository : IRiotRepository
    {
        #region Member Variables

        private readonly LeagueApi _leagueApi;

        #endregion Member Variables

        #region Constructors

        public RiotRepository()
        {
            _leagueApi = new LeagueApi(ConfigurationManager.AppSettings["LolApiKey"], RegionEnum.Na, true);
        }

        #endregion Constructors

        #region IRiotRepository Members

        public Summoner GetSummonerByName(string name)
        {
            return this.GetSummoner(name).Result;
        }

        #endregion IRiotRepository Members

        #region Private Methods

        private async Task<Summoner> GetSummoner(string name)
        {
            var summoner = await _leagueApi.Summoner.GetSummonerByNameAsync(name);
            var masteries = await _leagueApi.Summoner.GetMasteryPagesBySummonerIdAsync(summoner.SummonerId);
            var statSummaries = await _leagueApi.Stats.GetPlayerStatsSummariesBySummonerIdAsync(summoner.SummonerId);
            var rankedStats = await _leagueApi.Stats.GetRankedStatsSummariesBySummonerIdAsync(summoner.SummonerId);
            var recentGames = await _leagueApi.Game.GetRecentGamesBySummonerIdAsync(summoner.SummonerId);
            var runes = await _leagueApi.Summoner.GetRunePagesBySummonerIdAsync(summoner.SummonerId);
            return this.ConvertApiToModel(summoner, masteries, statSummaries, rankedStats, recentGames, runes);
        }

        private Summoner ConvertApiToModel(ISummoner summoner, IEnumerable<IMasteryPage> masteries, IEnumerable<IPlayerStatsSummary> statSummaries,
            IRankedStats rankedStats, IEnumerable<IGame> recentGames, IEnumerable<IRunePage> runes)
        {
            var model = new Summoner();

            model.Id = summoner.SummonerId;
            model.Level = summoner.SummonerLevel;
            model.MasteryPages = this.ConvertMasteriesToModels(masteries);
            model.Name = summoner.Name;
            model.PlayerStatsSummaries = this.ConvertStatSummariesToModels(statSummaries);
            model.ProfileIconId = summoner.ProfileIconId;
            model.RankedChampionStats = this.ConvertRankedStatsToModels(rankedStats);
            model.RecentGames = this.ConvertRecentGamesToModels(recentGames);
            model.RevisionDate = summoner.RevisionDate;
            model.RunePages = this.ConvertRunesToModels(runes);

            return model;
        }

        private MasteryPage[] ConvertMasteriesToModels(IEnumerable<IMasteryPage> masteries)
        {
            var pages = new List<MasteryPage>();
            foreach (var masteryPage in masteries)
            {
                var pageModel = new MasteryPage();
                pageModel.Id = masteryPage.Id;
                pageModel.IsCurrent = masteryPage.Current;
                pageModel.Name = masteryPage.Name;
                pageModel.Masteries = masteryPage.Masteries.Select(x => new Mastery { Id = x.Id, Rank = x.Rank }).ToArray();
                pages.Add(pageModel);
            }

            return pages.ToArray();
        }

        private PlayerStatsSummary[] ConvertStatSummariesToModels(IEnumerable<IPlayerStatsSummary> statSummaries)
        {
            var summaries = new List<PlayerStatsSummary>();
            foreach (var summary in statSummaries)
            {
                var summaryModel = new PlayerStatsSummary();
                summaryModel.Losses = summary.Losses;
                summaryModel.ModifyDate = summary.ModifyDate;
                summaryModel.Stats = this.ConvertAggregateStatsToModel(summary.AggregatedStats);
                summaryModel.SummaryType = (PlayerStatSummaryType)summary.PlayerStatSummaryType;
                summaryModel.Wins = summary.Wins;
                summaries.Add(summaryModel);
            }

            return summaries.ToArray();
        }

        private ChampionStats[] ConvertRankedStatsToModels(IRankedStats rankedStats)
        {
            var summaries = new List<ChampionStats>();
            foreach (var summary in rankedStats.Champions)
            {
                var summaryModel = new ChampionStats();
                summaryModel.Id = summary.ChampionId;
                summaryModel.Stats = this.ConvertAggregateStatsToModel(summary.Stats);
                summaries.Add(summaryModel);
            }

            return summaries.ToArray();
        }

        private AggregateStats ConvertAggregateStatsToModel(IAggregatedStats aggregatedStats)
        {
            var stats = new AggregateStats();

            stats.AverageAssists = aggregatedStats.AverageAssists;
            stats.AverageChampionsKilled = aggregatedStats.AverageChampionsKilled;
            //TODO: finish this

            return stats;
        }

        private Game[] ConvertRecentGamesToModels(IEnumerable<IGame> recentGames)
        {
            var games = new List<Game>();
            foreach (var recentGame in recentGames)
            {
                var game = new Game();
                game.ChampionId = recentGame.ChampionId;
                game.CreateDate = recentGame.CreateDate;
                game.GameMode = (GameMode)recentGame.GameMode;
                //game.GameStats =;
                game.GameSubType = (SubGameType)recentGame.GameSubType;
                game.GameType = (GameType)recentGame.GameType;
                game.Invalid = recentGame.Invalid;
                game.IpEarned = recentGame.IpEarned;
                game.Level = recentGame.Level;
                game.MapId = (int)recentGame.Map;
                //game.Players =;
                //game.Spell1 =;
                //game.Spell2 =;
                game.TeamId = recentGame.TeamId;

                games.Add(game);
            }

            return games.ToArray();
        }

        private RunePage[] ConvertRunesToModels(IEnumerable<IRunePage> runes)
        {
            throw new System.NotImplementedException();
        }

        #endregion Private Methods
    }
}