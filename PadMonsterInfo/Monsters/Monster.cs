using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PadMonsterInfo.Evolutions;

namespace PadMonsterInfo
{
    public class MonsterCard
    {
        public class Awaken
        {
            public int Id;
            public int Count;
            public Awaken(int id)
            {
                Id = id;
                Count = 1;
            }
            public Awaken(int id, int count)
            {
                Id = id;
                Count = count;
            }
        }
        public struct ImageInfo
        {
            private string href;
            public int Size;
            public string HRef
            {
                get { return href; }
                set
                { 
                    href = value;
                    FileName = href.Split('/').Last();
                }
            }
            public string FileName;
        }
        public ImageInfo Image40;
        public ImageInfo Image60;
        public int Id;
        public MonsterType Type;
        public MonsterType Type2;
        public MonsterType Type3;
        public int Cost;
        public MonsterElement Element;
        public MonsterElement Element2;
        public string Name;
        public int LevelMax;
        public int Rarity;
        public int MaxHP;
        public int MinHP;
        public int HPScale;
        public int MaxATK;
        public int MinATK;
        public int ATKScale;
        public int MaxRCV;
        public int MinRCV;
        public int RCVScale;
        public int XPCurve;
        public int FeedXP;
        public string LeaderSkill;
        public string ActiveSkill;
        public int Version;
        public bool JpOnly;
        public string JpName;
        public int PdxId;
        public int UsId;
        private List<Awaken> awokenSkills;
        public List<Evolution> Evolutions;

        public void AddAwakening(MonsterCard.Awaken awaken)
        {
            awokenSkills.Add(awaken);
        }
        public List<Awaken> GetAwakens(int awakenedCount = -1)
        {
            if(awakenedCount == -1)
            {
                awakenedCount = awokenSkills.Count();
            }
            List<MonsterCard.Awaken> AwakenCount = new List<MonsterCard.Awaken>();
            awakenedCount = (awakenedCount < awokenSkills.Count()) ? awakenedCount : awokenSkills.Count();
            for (int i = 0; i < awakenedCount; i++)
            {
                MonsterCard.Awaken awaken = awokenSkills[i];
                var awakenIndex = AwakenCount.FindIndex(item => item.Id == awaken.Id);
                if (awakenIndex != -1)
                {
                    AwakenCount[awakenIndex] = new MonsterCard.Awaken(awaken.Id, AwakenCount[awakenIndex].Count + 1);
                }
                else
                {
                    MonsterCard.Awaken newAwaken = new MonsterCard.Awaken(awaken.Id);
                    AwakenCount.Add(newAwaken);
                }
            }
            return AwakenCount;
        }
        public MonsterCard()
        {
            awokenSkills = new List<Awaken>();
            Evolutions = new List<Evolution>();
        }
    }
}
