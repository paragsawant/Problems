Problem B
The following sample code, given an NFC football team, will generate the name of its team mascot:
    
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
    

Expected Deliverables
1.	A complete Visual Studio .NET solution with all projects, written in C#, which includes a console application that:
a.	Prompts for the user to enter a team name and displays the appropriate mascot.
b.	Does NOT provide the ability to add or remove any teams, nor change the returned mascot.
c.	Does NOT throw unhandled exceptions.
d.	To prepare for requirement 2 below, is data-driven and extensible (i.e., must not have any magic strings in it).
2.	Once the solution is implemented, you are going to modify it to add AFC teams.  
We expect that you will NOT have to modify your existing code to make this possible.  
This should mean that your code is extensible such that it can be modified to support another sport that has teams and mascots. 
Please supply the complete Visual Studio .NET solution with all projects, implemented in C#, with the modifications described, if necessary.
