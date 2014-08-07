using System;
using Thetjyoung.Web.Models.Enums;

namespace Thetjyoung.Web.Models
{
    public class PlayerStatsSummary
    {
        public AggregateStats Stats { get; set; }
        public int Losses { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Wins { get; set; }
        public PlayerStatSummaryType SummaryType { get; set; }
    }
}