using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AFLTips.Shared.DataModels
{
    public class AFLFixture
    {
        [JsonProperty("games")]
        public List<Match> Matches { get; set; }
    }
}
