using DevChallengeL1.Models;
using DevChallengeL1.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DevChallengeL1.Controllers
{

    public class HomeController : Controller
    {


    public ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: /Home/
        [HttpGet]
        public ActionResult Index() 
        {
           return View(_context.Teams.ToList());
        }

        // POST: /Home/Save
        [HttpPost]
        public ActionResult Save(TournamentViewModel TourVM)
        {

            bool boSaveOk = SaveTournamentInDB(TourVM);

            return Json(new { success = boSaveOk });
        }



        // GET: /Home/Contact
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }


        // GET: /Home/GetTournament
        [HttpGet]
        public ContentResult GetTournament()
        {
            string stringJson = ReturnTournamentJsonFromDb();

            return Content(stringJson, "application/json");
        }


        // Mounts entire Tournament view model, based on database saved data, and return a serialized Json
        private string ReturnTournamentJsonFromDb()
        {

            string strJson = "";

            //Create Team List

            //Teams/Matchups
            List<List<string>> teamsList = new List<List<string>>();

            //Get teams from data base
            List<Team> teamsDB = _context.Teams.ToList();

            bool boAddMatch = false;
            string tmpTeamAName = "";
            string tmpTeamBName = "";

            foreach (var tmpTeamsDB in teamsDB)
            {

                if (!boAddMatch)
                {
                    tmpTeamAName = tmpTeamsDB.Name;
                    boAddMatch = true; // Next loop will add Match
                }
                else //Add New Match to List
                {
                    tmpTeamBName = tmpTeamsDB.Name; // load tmpTeamBName

                    List<string> tmpMatch = new List<string>();
                    tmpMatch.Add(tmpTeamAName);
                    tmpMatch.Add(tmpTeamBName);

                    teamsList.Add(tmpMatch);

                    boAddMatch = false; // Next loop will load tmpTeamAName
                }

            }

            //Total of rounds = teams total divided by 4 + 1 (final Match)
            int intRoundsCount = teamsDB.Count / 4 + 1;

            //Create Result List
            //Results/Rounds/Score/Points
            List<List<List<List<int>>>> resultsList = new List<List<List<List<int>>>>();

            //Get total of scores from round in data base
            int intScoresInDB = _context.Scores.Count();

            //Loads only if it has values
            if (intScoresInDB > 0)
            {
                //Create Round List
                List<List<List<int>>> roundsList = new List<List<List<int>>>();

                for (int rI = 1; rI <= intRoundsCount; rI++)
                {

                    //Get scores from round in data base
                    List<Score> scoresDB = _context.Scores.Where(s => s.Round == rI).ToList();

                    //Create List of scores 
                    List<List<int>> scoreList = new List<List<int>>();

                    //Add points to score
                    foreach (var tmpScoreDB in scoresDB)
                    {
                        List<int> tmpPoints = new List<int>();
                        tmpPoints.Add(tmpScoreDB.PointsTeamA);
                        tmpPoints.Add(tmpScoreDB.PointsTeamB);
                        scoreList.Add(tmpPoints);
                    }

                    roundsList.Add(scoreList);

                }
                resultsList.Add(roundsList);
            }



            //Create View Model
            var TourVM = new TournamentViewModel
            {
                teams = teamsList,
                results = resultsList
            };

            //Serialize View Model to Json
            strJson = JsonConvert.SerializeObject(TourVM);
                        

            return strJson;
        }


        // Save entire Tournament view model on database, based on deserialized Json posted
        private bool SaveTournamentInDB(TournamentViewModel TourVM)
        {
            bool boReturn = false;

            //Inicialize Transaction
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int intRoundIndex = 0;

                    //Delete all scores
                    _context.Database.ExecuteSqlCommand("TRUNCATE TABLE SCORES");


                    //Results/Rounds/Score/Points
                    //List<List<List<List<string>>>> results 
                    foreach (var results in TourVM.results)
                    {
                        foreach (var rounds in results) //Rounds
                        {
                            intRoundIndex++;

                            foreach (var score in rounds) //score
                            {
                                int intPointA = 0;
                                int intPointB = 0;

                                int intPointIndex = 0;

                                foreach (var points in score)
                                {
                                    if (intPointIndex == 0)
                                    {
                                        intPointA = points; //Get points A on first pass
                                    }
                                    else intPointB = points; //Get points A on second pass

                                    intPointIndex++;
                                }

                                //Create new score to persist based on round and points 
                                var tempDbScore = new Score
                                {
                                    Round = intRoundIndex,
                                    PointsTeamA = intPointA,
                                    PointsTeamB = intPointB,
                                };

                                _context.Scores.Add(tempDbScore);
                                _context.SaveChanges();

                            }
                        }
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("SaveTournamentInDB => Exception " + e.Message);
                    dbContextTransaction.Rollback();
                }

            }

            return boReturn;
        }

    }
}