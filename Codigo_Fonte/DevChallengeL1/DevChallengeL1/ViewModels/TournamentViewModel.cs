using System.Collections.Generic;

namespace DevChallengeL1.ViewModels
{
    //View model used to Serialize and Deserialize Tournament   
    public class TournamentViewModel
    {

        //Teams/Matchups
        public List<List<string>> teams { get; set; }

        //Results/Rounds/Score/Points
        public List<List<List<List<int>>>> results { get; set; }
    }

    
}