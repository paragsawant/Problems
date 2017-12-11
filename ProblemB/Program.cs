using ProblemB.League;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemB
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        public void Run()
        {

            LeagueFactory leagueFactory = new ConcreteLeagueFactory();
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine("Enter League Name");
                    string userInputLeague = Console.ReadLine();
                    Console.WriteLine("Enter Team Name");
                    string userInputTeam = Console.ReadLine();
                    Console.WriteLine(leagueFactory.GetLeagueName(userInputLeague).GetTeamMascot(userInputTeam));
                    userInputTeam = Console.ReadLine();
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        }
    }
}
