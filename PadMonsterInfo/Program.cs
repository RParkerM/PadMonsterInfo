using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadMonsterInfo.PadHerder_User;
using System.IO;


namespace PadMonsterInfo
{
    class Program
    {


        static void Main(string[] args)
        {
            MonsterDB db = new MonsterDB();
            MonsterList ml = db.MonsterList;

            PadHerderUser user = new PadHerderUser("korbah");

           /* foreach(PadHerder_User.Team team in user.UserData.Teams)
            {
                Console.WriteLine(team.Name + ":");
                Console.Write(db.MonsterList.FindMonsterCard(user.FindMonsterInstance(team.Leader)).Name);
            }*/
            //following is an example that retrieves a monster and it's awakenings
            
            {
                
                Console.WriteLine("What monster would you like to look at? (MonsterID)");
                string input = Console.ReadLine();
                while (input != "quit")
                {
                    int id = 0;
                    if (int.TryParse(input, out id))
                    {
                        MonsterCard monster = db.FindMonsterCard(id);
                        Console.WriteLine("monster name: " + monster.Name);
                       // foreach(Evolutions.Evolution evo in monster.Evolutions)
                        {
                            //Console.WriteLine(db.MonsterNameByID(evo.EvolvesTo));
                        }
                        List<Evolutions.Evolution> evolutions = db.getAvailableEvolutions(monster);
                        Console.WriteLine("Amount of Evos: " + evolutions.Count);
                        foreach (Evolutions.Evolution evo in evolutions)
                        {
                            Console.WriteLine(db.MonsterNameByID(evo.EvolvesTo));
                        }
                        /*
                        Console.WriteLine("Evolves into something else? " ); //need to figure this out still TODO
                        if (monster != null)
                        {
                            Console.WriteLine("How many awakenings do you have?");
                            int personalAwakenCount = 0;
                            if (int.TryParse(Console.ReadLine(), out personalAwakenCount))
                            {

                            }
                            Console.WriteLine(monster.Name);
                            foreach(Evolutions.Evolution evo in monster.Evolutions)
                            {
                                Console.WriteLine("Can evolve to " + db.MonsterNameByID(evo.EvolvesTo));
                                Console.Write("Materials: [ ");
                                foreach(Evolutions.EvoMaterial mat in evo.Materials)
                                {
                                    Console.Write(db.MonsterNameByID(mat.MonsterCardId) + " x " + mat.Count + " ");

                                }
                                Console.WriteLine("] ");
                            }
                            foreach (MonsterCard.Awaken awakenCount in monster.GetAwakens(personalAwakenCount))
                            {
                                Console.WriteLine(db.AwakensInfo.Awakens.Find(item => item.Id == awakenCount.Id).Name + " x " + awakenCount.Count);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Monster ID");
                        }
                        */
                    }
                    else
                    {
                        List<MonsterCard> monsters = db.FindMonstersByName(input);
                        if(monsters.Count() > 0)
                        {
                            Console.WriteLine(input.Contains("lu bu", StringComparison.OrdinalIgnoreCase));
                            foreach(MonsterCard monster in monsters)
                            {
                                Console.WriteLine(monster.Name);
                            }
                        }
                    }
                    Console.WriteLine("What monster would you like to look at? (MonsterID)");
                    input = Console.ReadLine();
                }
                
            }

        }
    }
}
