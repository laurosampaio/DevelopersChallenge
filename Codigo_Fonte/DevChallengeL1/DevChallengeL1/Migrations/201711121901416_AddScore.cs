namespace DevChallengeL1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        TeamAId = c.Int(nullable: false),
                        TeamBId = c.Int(nullable: false),
                        PointsTeamA = c.Int(nullable: false),
                        PointsTeamB = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Scores");
        }
    }
}
