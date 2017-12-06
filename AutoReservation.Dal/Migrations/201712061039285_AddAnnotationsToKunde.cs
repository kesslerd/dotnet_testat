namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationsToKunde : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kundes", "Nachname", c => c.String(nullable: false));
            AlterColumn("dbo.Kundes", "Vorname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kundes", "Vorname", c => c.String());
            AlterColumn("dbo.Kundes", "Nachname", c => c.String());
        }
    }
}
