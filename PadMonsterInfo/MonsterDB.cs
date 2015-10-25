using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadMonsterInfo.Evolutions;
using System.IO;

namespace PadMonsterInfo
{
    class MonsterDB
    {
        public AwakenList AwakensInfo;
        public MonsterList MonsterList;

        public MonsterCard FindMonsterCard(PadHerder_User.Monster monster)
        {
            MonsterCard card = MonsterList.MonsterCards.Find(monsterCard => monsterCard.Id == monster.MonsterCardId);
            return card;
        }

        public MonsterCard FindMonsterCard(int monsterCardId)
        {
            MonsterCard card = MonsterList.MonsterCards.Find(monsterCard => monsterCard.Id == monsterCardId);
            return card;
        }

        public string MonsterNameByID(int monsterCardId)
        {
            string name = FindMonsterCard(monsterCardId).Name;
            return name;
        }

        public async Task<System.Drawing.Image> getMonsterImage60(MonsterCard monster)
        {
            string phUrl = "https://www.padherder.com/";
            System.Diagnostics.Debug.Write("getting an image... for " + monster.Name);
            await Methods.UpdateFileCachedAsync(phUrl + monster.Image60.HRef, monster.Image60.FileName);
            System.Drawing.Image img = null;
            if (File.Exists(monster.Image60.FileName))
            {
                img = System.Drawing.Image.FromFile(monster.Image60.FileName);
            }
            return img;
        }

        private void addEvosToList(List<Evolution> evoList, Evolution baseEvo, int baseMonsterId)
        {
            MonsterCard card = FindMonsterCard(baseEvo.EvolvesTo);
            foreach(Evolution evo in card.Evolutions)
            {
                if(!evoList.Any(e => e.EvolvesTo == evo.EvolvesTo) && baseMonsterId != evo.EvolvesTo)
                {
                    Evolution newEvo = new Evolution();
                    newEvo.EvolvesTo = evo.EvolvesTo;
                    newEvo.Materials.AddRange(baseEvo.Materials);
                    foreach(EvoMaterial mat in evo.Materials)
                    {
                        if(newEvo.Materials.Any(m => m.MonsterCardId == mat.MonsterCardId))
                        {
                            newEvo.Materials.First(m => m.MonsterCardId == mat.MonsterCardId).Count += mat.Count;
                        }
                        else
                        {
                            EvoMaterial newMat = new EvoMaterial();
                            newMat.MonsterCardId = mat.MonsterCardId;
                            newMat.Count = mat.Count;
                            newEvo.Materials.Add(newMat);
                        }
                    }
                    evoList.Add(newEvo);
                    Console.WriteLine("added evolution" + MonsterNameByID(newEvo.EvolvesTo));
                    addEvosToList(evoList, newEvo, baseMonsterId);
                }
            }
        }

        public List<Evolution> getAvailableEvolutions(MonsterCard card)
        {
            List<Evolution> evoList = new List<Evolution>();
            foreach(Evolution evo in card.Evolutions)
            {
                evoList.Add(evo);
                
            }
            foreach(Evolution evo in card.Evolutions)
            {
                addEvosToList(evoList, evo, card.Id);
            }
            return evoList;
        }

        public List<MonsterCard> FindMonstersByName(string searchQuery)
        {
            List<MonsterCard> monsters = MonsterList.MonsterCards.Where(monst => monst.Name.Contains(searchQuery,StringComparison.OrdinalIgnoreCase)).ToList();
            return monsters;
        }

        public List<MonsterCard> FindMonsters(string searchQuery)
        {
            int id = 0;
            List<MonsterCard> monsters;
            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                if (int.TryParse(searchQuery, out id))
                {
                    monsters = new List<MonsterCard>();
                    MonsterCard monster = FindMonsterCard(id);
                    if(monster != null) monsters.Add(monster);
                }
                else
                {
                    monsters = MonsterList.MonsterCards.Where(monst => monst.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }
            else
            {
                monsters = new List<MonsterCard>();
            }
            return monsters;
        }


    public MonsterDB()
        {
            AwakensInfo = new AwakenList();
            MonsterList = new MonsterList(AwakensInfo);
            MonsterList.DownloadImages();
        }
    }
}
