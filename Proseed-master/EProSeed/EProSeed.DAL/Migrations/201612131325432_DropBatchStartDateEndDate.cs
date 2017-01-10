namespace EProSeed.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropBatchStartDateEndDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("Batch", "StartDate");
            DropColumn("Batch", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("Batch", "StartDate", c => c.DateTime(false));
            AddColumn("Batch", "EndDate", c => c.DateTime(false));
        }
    }
}
