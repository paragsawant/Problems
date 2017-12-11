
namespace ProblemB.League
{
    public abstract class LeagueFactory
    {
        public abstract ILeague GetLeagueName(string teamName);
    }
}
