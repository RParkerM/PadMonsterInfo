using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace PadMonsterInfo.PadHerder_User
{
    class PadHerderUser
    {
        private string UserApiUrlPrefix;
        private string LocalFileUrl;
        private string Username;
        private string Password;
        public UserData UserData;

        private void ParseUserData()
        {
            UserData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(LocalFileUrl));
            using (StreamReader file = File.OpenText(LocalFileUrl))
            {
                JObject userData = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                JArray monsterInfo = (JArray)userData["monsters"];
                foreach(JToken monster in monsterInfo)
                {
                    //Console.WriteLine(monster["target_evolution"]);
                };
                //Console.WriteLine(userData.ToString());
            }
            //Console.WriteLine(UserData.Profile.DisplayName);

        }

        public Monster FindMonsterInstance(int monsterId)
        {
            return UserData.Monsters.Find(monst => monst.Id == monsterId);
        }

        public PadHerderUser(string username, string password = "", string localFileUrl = "user.json")
        {
            Username = username;
            Password = password;
            LocalFileUrl = localFileUrl;
            UserApiUrlPrefix = "https://www.padherder.com/user-api/";
            Methods.UpdateFileCached(UserApiUrlPrefix + "user/" + Username, LocalFileUrl, 1);
            ParseUserData();
        }
    }
}
