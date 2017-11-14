namespace DevChallengeL1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoreUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Scores", "PointsTeamA", c => c.String());
            AlterColumn("dbo.Scores", "PointsTeamB", c => c.String());
            DropColumn("dbo.Scores", "TeamAId");
            DropColumn("dbo.Scores", "TeamBId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Scores", "TeamBId", c => c.Int(nullable: false));
            AddColumn("dbo.Scores", "TeamAId", c => c.Int(nullable: false));
            AlterColumn("dbo.Scores", "PointsTeamB", c => c.Int(nullable: false));
            AlterColumn("dbo.Scores", "PointsTeamA", c => c.Int(nullable: false));
        }
    }
}
