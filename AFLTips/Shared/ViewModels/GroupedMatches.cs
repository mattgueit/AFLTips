using System;
using System.Collections.Generic;

namespace AFLTips.Shared.ViewModels
{
    public class GroupedMatches
    {
        public DateTime MatchDate { get; set; }
        public List<MatchViewModel> Matches { get; set; }
    }
}
