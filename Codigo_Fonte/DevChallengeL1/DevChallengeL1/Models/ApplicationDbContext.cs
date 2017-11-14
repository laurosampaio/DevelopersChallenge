using System.Data.Entity;

namespace DevChallengeL1.Models
{
    public class ApplicationDbContext: DbContext
    {

        public DbSet<Team> Teams { get; set; }
        public DbSet<Score> Scores { get; set; }

        public ApplicationDbContext()
            : base("TournamentLocalDb")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}
