namespace DevChallengeL1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChangePointType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Scores", "PointsTeamA", c => c.Int(nullable: false));
            AlterColumn("dbo.Scores", "PointsTeamB", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scores", "PointsTeamB", c => c.String());
            AlterColumn("dbo.Scores", "PointsTeamA", c => c.String());
        }
    }
}
