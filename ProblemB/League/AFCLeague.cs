using System;
namespace ProblemB.League
{
    public class AFCLeague : ILeague
    {
        public string GetTeamMascot(string teamName)
        {
            if (teamName == "Oakland Raiders")
            {
                return "Raider Rusher";
            }

            if (teamName == "buffalo bills")
            {
                return "Buffalo";
            }

            if (teamName == "New England Patriots")
            {
                return "Pat Patriot";
            }

            if (teamName == "Baltimore Ravens")
            {
                return "Poe, Rise and Conquer";
            }

            throw new Exception("Unknown team name");
        }
    }
}
