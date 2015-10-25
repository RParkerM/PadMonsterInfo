using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PadMonsterInfo
{
    public class Awaken
    {
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "desc")]
        public string Desc;
        [JsonProperty(PropertyName = "id")]
        public int Id;
    }
}
