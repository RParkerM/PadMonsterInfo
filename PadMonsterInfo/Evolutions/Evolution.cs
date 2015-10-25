using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PadMonsterInfo.Evolutions
{
    public class EvoMaterial
    {
        public int MonsterCardId;
        public int Count;
    }
    public class Evolution
    {
        [JsonProperty("is_ultimate")]
        public bool IsUltimate { get; set; }
        [JsonProperty("materials")]
        public List<EvoMaterial> Materials { get; set; }
        [JsonProperty("evolves_to")]
        public int EvolvesTo { get; set; }

        public Evolution()
        {
            Materials = new List<EvoMaterial>();
        }
    }
}
