namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "dob", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "create_date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "modified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "modified", c => c.String());
            AlterColumn("dbo.Users", "create_date", c => c.String());
            AlterColumn("dbo.Users", "dob", c => c.String());
        }
    }
}
