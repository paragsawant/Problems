using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemB.League
{
    public class ConcreteLeagueFactory : LeagueFactory
    {

        public override ILeague GetLeagueName(string teamName)
        {
            switch (teamName.ToUpper())
            {
                case "NFC":
                    return new NFCLeague();
                case "AFC":
                    return new AFCLeague();
                default:
                    throw new Exception("Unknown league name");
            }
        }
    }
}
