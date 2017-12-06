namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationsToAuto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Autoes", "Marke", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Autoes", "Marke", c => c.String());
        }
    }
}
