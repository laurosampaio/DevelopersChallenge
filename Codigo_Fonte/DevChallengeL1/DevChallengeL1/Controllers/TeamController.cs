using DevChallengeL1.Models;
using System.Linq;
using System.Web.Mvc;

namespace DevChallengeL1.Controllers
{
    public class TeamController : Controller
    {
        private ApplicationDbContext _context; 

        public TeamController()
        {
          
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            
            _context.Dispose();
        }

        // GET: /Team        
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }


        // GET: /Team/Teams
        [HttpGet]
        public JsonResult Teams()
        {
            return Json(_context.Teams.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: /Team/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("TeamForm", new Team());
        }

        // GET: /Team/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Return team base on id passed 
            var teamInDB = _context.Teams.SingleOrDefault(t => t.Id == id);

            if (teamInDB != null)
            {
                return View("TeamForm", teamInDB);
            }
            else
            {
                return HttpNotFound();
            }
            

        }

        // POST: Team/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Team team)
        {
            //Redirect to Form if model state it's ok
            if (!ModelState.IsValid)
            {
                return View("TeamForm", team);
            }

            // New team
            if (team.Id == 0) { 

                _context.Teams.Add(team);

                //Delete all scores to prevent inconsistences
                _context.Database.ExecuteSqlCommand("TRUNCATE TABLE SCORES");
            }
            else // Edit team
            {
                var teamInDb = _context.Teams.Single(t => t.Id == team.Id);
                teamInDb.Name = team.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Team");            
        }

        // DELETE /Api/TeamApi/{id}
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            bool resultDeleteOk = false;

            var teamInDB = _context.Teams.SingleOrDefault(T => T.Id == id);

            if (teamInDB != null)
            {
                _context.Teams.Remove(teamInDB);
                _context.SaveChanges();

                //Delete all scores to prevent inconsistences
                _context.Database.ExecuteSqlCommand("TRUNCATE TABLE SCORES");

                resultDeleteOk = true;
            }
            else { }

            return Json(new { success = resultDeleteOk }); 
        }

    }
}