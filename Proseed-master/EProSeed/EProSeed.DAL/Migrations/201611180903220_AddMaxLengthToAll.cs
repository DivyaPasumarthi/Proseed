namespace EProSeed.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxLengthToAll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Property", "FocusOnQualityComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "CommunicationComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "TransparencyComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "TeamPlayerComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "DisciplineComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "EnergyComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "CommitmentComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "OwnerShipComment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Property", "TechnicalCompetencyComment", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Property", "TechnicalCompetencyComment", c => c.String());
            AlterColumn("dbo.Property", "OwnerShipComment", c => c.String());
            AlterColumn("dbo.Property", "CommitmentComment", c => c.String());
            AlterColumn("dbo.Property", "EnergyComment", c => c.String());
            AlterColumn("dbo.Property", "DisciplineComment", c => c.String());
            AlterColumn("dbo.Property", "TeamPlayerComment", c => c.String());
            AlterColumn("dbo.Property", "TransparencyComment", c => c.String());
            AlterColumn("dbo.Property", "CommunicationComment", c => c.String());
            AlterColumn("dbo.Property", "FocusOnQualityComment", c => c.String());
        }
    }
}
