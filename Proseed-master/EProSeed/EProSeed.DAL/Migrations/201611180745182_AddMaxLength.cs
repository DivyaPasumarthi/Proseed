namespace EProSeed.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Property", "PassionForClientSuccessComment", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Property", "PassionForClientSuccessComment", c => c.String());
        }
    }
}
