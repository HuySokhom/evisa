namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        surname = c.String(),
                        middle_name = c.String(),
                        given_name = c.String(),
                        sex = c.String(),
                        dob = c.String(),
                        country = c.String(),
                        nationality = c.String(),
                        photo = c.String(),
                        passport_no = c.String(),
                        passport_issue = c.String(),
                        country_issue = c.String(),
                        passport_expire = c.String(),
                        contact_name = c.String(),
                        primary_email = c.String(),
                        secondary_email = c.String(),
                        phone_number = c.String(),
                        residential_address = c.String(),
                        heard_from = c.String(),
                        user_id = c.String(),
                        create_date = c.String(),
                        create_by = c.String(),
                        modified = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
