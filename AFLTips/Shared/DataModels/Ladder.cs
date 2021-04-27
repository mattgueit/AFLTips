using Newtonsoft.Json;
using System.Collections.Generic;

namespace AFLTips.Shared.DataModels
{
    public class Ladder
    {
        [JsonProperty("standings")]
        public List<LadderPosition> LadderPositions { get; set; }
    }
}
