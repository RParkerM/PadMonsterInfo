using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace PadMonsterInfo
{
    

    public class AwakenList
    {
        public List<Awaken> Awakens;
        private string AwakensUrl;
        private string AwakensFile;

        public void parse_awakens()
        {
            Awakens = JsonConvert.DeserializeObject<List<Awaken>>(File.ReadAllText(AwakensFile));
        }
        public AwakenList()
        {
            Awakens = new List<Awaken>();
            AwakensFile = "awakens.json";
            AwakensUrl = "https://www.padherder.com/api/awakenings/";
            Methods.UpdateFileCached(AwakensUrl, AwakensFile, 24);
            parse_awakens();
        }
    }
}
