using System.Collections.Generic;
using Newtonsoft.Json;

namespace AFLTips.Shared.DataModels
{
    public class Ladder
    {
        [JsonProperty("standings")]
        public List<LadderPosition> LadderPositions { get; set; }
    }
}
