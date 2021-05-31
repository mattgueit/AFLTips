using System.Collections.Generic;
using Newtonsoft.Json;

namespace AFLTips.Shared.DataModels
{
    public class Fixture
    {
        [JsonProperty("games")]
        public List<Match> Matches { get; set; }
    }
}
