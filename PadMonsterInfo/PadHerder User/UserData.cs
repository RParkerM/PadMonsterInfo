using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PadMonsterInfo.PadHerder_User
{
    public class Profile
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "public")]
        public bool Public { get; set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName = "profile_text")]
        public string ProfileText { get; set; }
        [JsonProperty(PropertyName = "account_id")]
        public int AccountId { get; set; }
        [JsonProperty(PropertyName = "country")]
        public int Country { get; set; }
        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }
        [JsonProperty(PropertyName = "starter_colour")]
        public int StarterColour { get; set; }
        [JsonProperty(PropertyName = "max_team_cost")]
        public int MaxTeamCost { get; set; }
        [JsonProperty(PropertyName = "team_group_1")]
        public string TeamGroup1 { get; set; }
        [JsonProperty(PropertyName = "team_group_2")]
        public string TeamGroup2 { get; set; }
        [JsonProperty(PropertyName = "team_group_3")]
        public string TeamGroup3 { get; set; }
        [JsonProperty(PropertyName = "team_group_4")]
        public string TeamGroup4 { get; set; }
        [JsonProperty(PropertyName = "team_group_5")]
        public string TeamGroup5 { get; set; }
        [JsonProperty(PropertyName = "team_group_6")]
        public string TeamGroup6 { get; set; }
        [JsonProperty(PropertyName = "team_group_7")]
        public string TeamGroup7 { get; set; }
        [JsonProperty(PropertyName = "team_group_8")]
        public string TeamGroup8 { get; set; }
        [JsonProperty(PropertyName = "team_group_9")]
        public string TeamGroup9 { get; set; }
        [JsonProperty(PropertyName = "team_group_10")]
        public string TeamGroup10 { get; set; }
    }
    public class Food
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "monster")]
        public int Monster { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }

    public class Material
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "monster")]
        public int Monster { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }

    public class Monster
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "monster")]
        public int MonsterCardId { get; set; }
        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }
        [JsonProperty(PropertyName = "current_xp")]
        public int CurrentXp { get; set; }
        [JsonProperty(PropertyName = "current_skill")]
        public int CurrentSkill { get; set; }
        [JsonProperty(PropertyName = "current_awakening")]
        public int CurrentAwakening { get; set; }
        [JsonProperty(PropertyName = "target_level")]
        public int TargetLevel { get; set; }
        [JsonProperty(PropertyName = "target_evolution")]
        public int? TargetEvolution { get; set; }
        [JsonProperty(PropertyName = "plus_hp")]
        public int PlusHp { get; set; }
        [JsonProperty(PropertyName = "plus_atk")]
        public int PlusAtk { get; set; }
        [JsonProperty(PropertyName = "plus_rcv")]
        public int PlusRcv { get; set; }
    }

    public class Team
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "favourite")]
        public bool Favourite { get; set; }
        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }
        [JsonProperty(PropertyName = "team_group")]
        public int TeamGroup { get; set; }
        [JsonProperty(PropertyName = "leader")]
        public int Leader { get; set; }
        [JsonProperty(PropertyName = "sub1")]
        public int Sub1 { get; set; }
        [JsonProperty(PropertyName = "sub2")]
        public int Sub2 { get; set; }
        [JsonProperty(PropertyName = "sub3")]
        public int Sub3 { get; set; }
        [JsonProperty(PropertyName = "sub4")]
        public int Sub4 { get; set; }
        [JsonProperty(PropertyName = "friend_leader")]
        public int FriendLeader { get; set; }
        [JsonProperty(PropertyName = "friend_level")]
        public int FriendLevel { get; set; }
        [JsonProperty(PropertyName = "friend_hp")]
        public int FriendHp { get; set; }
        [JsonProperty(PropertyName = "friend_atk")]
        public int FriendAtk { get; set; }
        [JsonProperty(PropertyName = "friend_rcv")]
        public int FriendRcv { get; set; }
        [JsonProperty(PropertyName = "friend_skill")]
        public int FriendSkill { get; set; }
        [JsonProperty(PropertyName = "friend_awakening")]
        public int FriendAwakening { get; set; }
    }

    class UserData
    {
        [JsonProperty(PropertyName = "profile")]
        public Profile Profile { get; set; }
        [JsonProperty(PropertyName = "food")]
        public List<Food> Food { get; set; }
        [JsonProperty(PropertyName = "materials")]
        public List<Material> Materials { get; set; }
        [JsonProperty(PropertyName = "monsters")]
        public List<Monster> Monsters { get; set; }
        [JsonProperty(PropertyName = "teams")]
        public List<Team> Teams { get; set; }
    }
}
