using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PadMonsterInfo.Evolutions;

namespace PadMonsterInfo
{
    public enum MonsterType { EvoMaterial=0, Balanced=1, Physical=2, Healer=3, Dragon=4, God=5, Attacker=6, Devil=7, AwokenSkillMaterial=12, Protected=13, EnhanceMaterial=14};
    public enum MonsterElement { Fire = 0, Water = 1, Wood = 2, Light = 3, Dark = 4 };

    public class MonsterList
    {
        private string MonsterListUrl;
        private string MonsterListFile;
        private string EvolutionUrl;
        private string EvolutionFile;
        public AwakenList AwakensInfo;
        public List<MonsterCard> MonsterCards;

        public void ParseMonsters()
        {
            using (StreamReader file = File.OpenText(MonsterListFile))
            {
                JArray monstersJS = (JArray)JToken.ReadFrom(new JsonTextReader(file));
                foreach (JObject monsterJS in monstersJS.Children())
                {
                    MonsterCard monster = new MonsterCard();
                    IList<string> keys = monsterJS.Properties().Select(p => p.Name).ToList();
                    foreach (string key in keys)
                    {
                        switch (key)
                        {
                            case "id":
                                monster.Id = (int)monsterJS.Property("id").Value;
                                break;
                            case "type":
                                monster.Type = (MonsterType)((int)monsterJS.Property("type").Value);
                                break;
                            case "type2":
                                var type2 = monsterJS["type2"];
                                if(!JsonExtensions.IsNullOrEmpty(type2))
                                {
                                    monster.Type2 = (MonsterType)((int)type2);
                                }
                                break;
                            case "type3":
                                var type3 = monsterJS["type3"];
                                if (!JsonExtensions.IsNullOrEmpty(type3))
                                {
                                    monster.Type3 = (MonsterType)((int)type3);
                                }
                                break;
                            case "team_cost":
                                monster.Cost = (int)monsterJS.Property("team_cost").Value;
                                break;
                            case "element":
                                monster.Element = (MonsterElement)((int)monsterJS.Property("element").Value);
                                break;
                            case "element2":
                                JToken element2 = monsterJS["element2"];
                                if (!JsonExtensions.IsNullOrEmpty(element2))
                                {
                                    monster.Element2 = (MonsterElement)((int)element2);
                                }
                                break;
                            case "image40_size":
                                monster.Image40.Size = (int)monsterJS.Property("image40_size").Value;
                                break;
                            case "image40_href":
                                monster.Image40.HRef = (string)monsterJS.Property("image40_href").Value;
                                break;
                            case "image60_size":
                                monster.Image60.Size = (int)monsterJS.Property("image60_size").Value;
                                break;
                            case "image60_href":
                                monster.Image60.HRef = (string)monsterJS.Property("image60_href").Value;
                                break;
                            case "name":
                                monster.Name = (string)monsterJS.Property("name").Value;
                                break;
                            case "max_level": 
                                monster.LevelMax = (int)monsterJS.Property("max_level").Value;
                                break;
                            case "rarity":
                                monster.Rarity = (int)monsterJS.Property("rarity").Value;
                                break;
                            case "hp_max":
                                monster.MaxHP = (int)monsterJS.Property("hp_max").Value;
                                break;
                            case "hp_min":
                                monster.MinHP = (int)monsterJS.Property("hp_min").Value;
                                break;
                            case "hp_scale":
                                monster.HPScale = (int)monsterJS["hp_scale"];
                                break;
                            case "atk_max":
                                monster.MaxATK = (int)monsterJS["atk_max"];
                                break;
                            case "atk_min":
                                monster.MinATK = (int)monsterJS["atk_min"];
                                break;
                            case "atk_scale":
                                monster.ATKScale = (int)monsterJS["atk_scale"];
                                break;
                            case "rcv_max":
                                monster.MaxRCV = (int)monsterJS["rcv_max"];
                                break;
                            case "rcv_min":
                                monster.MinRCV = (int)monsterJS["rcv_min"];
                                break;
                            case "rcv_scale":
                                monster.RCVScale = (int)monsterJS["rcv_scale"];
                                break;
                            case "xp_curve":
                                monster.XPCurve = (int)monsterJS["xp_curve"];
                                break;
                            case "feed_xp":
                                monster.FeedXP = (int)monsterJS["feed_xp"];
                                break;
                            case "leader_skill":
                                monster.LeaderSkill = (string)monsterJS["leader_skill"];
                                break;
                            case "active_skill":
                                monster.ActiveSkill = (string)monsterJS["active_skill"];
                                break;
                            case "version":
                                monster.Version = (int)monsterJS["version"];
                                break;
                            case "jp_only":
                                if (monsterJS["jp_only"].ToString() == "true") { monster.JpOnly = true; }
                                else monster.JpOnly = false;
                                break;
                            case "name_jp":
                                monster.JpName = monsterJS["name_jp"].ToString();
                                break;
                            case "pdx_id":
                                var pdxID = monsterJS["pdx_id"];
                                if(!JsonExtensions.IsNullOrEmpty(pdxID))
                                {
                                    pdxID = (int)pdxID;
                                }
                                break;
                            case "us_id":
                                var usID = monsterJS["us_id"];
                                if (!JsonExtensions.IsNullOrEmpty(usID))
                                {
                                    pdxID = (int)usID;
                                }
                                break;
                            case "awoken_skills":
                                string[] numbers = System.Text.RegularExpressions.Regex.Split(monsterJS.Property("awoken_skills").Value.ToString(), @"\D+");
                                if (numbers.Length > 0)
                                {
                                    foreach (string value in numbers)
                                    {
                                        if (!string.IsNullOrEmpty(value))
                                        {
                                            int awakenId = int.Parse(value);
                                            MonsterCard.Awaken aw = new MonsterCard.Awaken(awakenId);
                                            monster.AddAwakening(aw);                          
                                        }
                                    }
                                }
                                break;
                            default:
                                Debug.WriteLine(key + " " + monsterJS.Property(key).Value.ToString());
                                break;
                        }

                    }
                    MonsterCards.Add(monster);
                }
            }
        }

        public void ParseEvolutions()
        {
            using (StreamReader file = File.OpenText(EvolutionFile))
            {
                
                JObject evolutionsJS = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                IList<string> keys = evolutionsJS.Properties().Select(p => p.Name).ToList();
                foreach(string key in keys)
                {
                    int id = 0;                    
                    if(int.TryParse(key,out id))
                    {
                        MonsterCard monster = FindMonsterCard(id);                        
                        foreach(JObject evolutionJS in evolutionsJS[key])
                        {
                            Evolution evolution = new Evolution();
                            evolution.EvolvesTo = (int)evolutionJS["evolves_to"];
                            List<Evolution> dupes = monster.Evolutions.FindAll(monst => monst.EvolvesTo == evolution.EvolvesTo);
                            if (dupes.Count == 0)
                            {
                                evolution.IsUltimate = bool.Parse(evolutionJS["is_ultimate"].ToString());
                                foreach (JArray material in evolutionJS["materials"])
                                {
                                    EvoMaterial mat = new EvoMaterial();
                                    mat.MonsterCardId = (int)material[0];
                                    mat.Count = (int)material[1];
                                    evolution.Materials.Add(mat);
                                }
                                monster.Evolutions.Add(evolution);
                            }
                            //Console.WriteLine(evolutionJS);
                        }
                        //evolutions.Add(evolutionEntry);                     
                        
                    }
                    //Console.WriteLine(key);
                }                
            }
        }

        private MonsterCard FindMonsterCard(int monsterCardId)
        {
            MonsterCard card = MonsterCards.Find(monsterCard => monsterCard.Id == monsterCardId);
            return card;
        }

        public async void DownloadImages()
        {
            const string phUrl = "https://www.padherder.com/";
            const string localImageDirectory = "Images\\";
            Directory.CreateDirectory("Images\\");
            foreach (MonsterCard monster in MonsterCards)
            {
                
                //System.Diagnostics.Debug.Write("getting an image... for " + monster.Name);
                await Methods.UpdateFileCachedAsync(phUrl + monster.Image60.HRef, localImageDirectory + monster.Image60.FileName);
            }
        }

        public MonsterList(AwakenList awakensInfo)
        {
            MonsterListUrl = "https://www.padherder.com/api/monsters/";
            EvolutionUrl = "https://www.padherder.com/api/evolutions";
            MonsterListFile = "monsters.json";            
            EvolutionFile = "evolutions.json";
            AwakensInfo = awakensInfo;
            MonsterCards = new List<MonsterCard>();
            Methods.UpdateFileCached(MonsterListUrl, MonsterListFile, 24);            
            Methods.UpdateFileCached(EvolutionUrl, EvolutionFile, 24);
            ParseMonsters();
            ParseEvolutions();
        }
    }
}