using System;

namespace ProblemB.League
{
    public class NFCLeague : ILeague
    {
        public string GetTeamMascot(string teamName)
        {
            if (teamName == "Arizona Cardinals")
            {
                return "Big Red";
            }

            if (teamName == "Atlanta Falcons")
            {
                return "Freddie Falcon";
            }

            if (teamName == "Carolina Panthers")
            {
                return "Sir Purr";
            }

            if (teamName == "Chicago Bears")
            {
                return "Staley Da Bear";
            }

            if (teamName == "Dallas Cowboys")
            {
                return "Rowdy";
            }

            if (teamName == "Detroit Lions")
            {
                return "Roary";
            }

            if (teamName == "Green Bay Packers")
            {
                return "None";
            }

            if (teamName == "Minnesota Vikings")
            {
                return "Ragnar, Viktor";
            }

            if (teamName == "New Orleans Saints")
            {
                return "Gumbo, Sir Saint";
            }

            if (teamName == "New York Giants")
            {
                return "None";
            }

            if (teamName == "Philadelphia Eagles")
            {
                return "Swoop, Air Swoop";
            }

            if (teamName == "St. Louis Rams")
            {
                return "Rampage";
            }

            if (teamName == "San Francisco 49ers")
            {
                return "Sourdough Sam";
            }

            if (teamName == "Seattle Seahawks")
            {
                return "Blitz; Boom; Taima";
            }

            if (teamName == "Tampa Bay Buccaneers")
            {
                return "Captain Fear";
            }

            if (teamName == "Washington Redskins")
            {
                return "None";
            }

            throw new Exception("Unknown team name");
        }

    }
}
