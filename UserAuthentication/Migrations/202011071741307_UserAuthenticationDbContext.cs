namespace UserAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAuthenticationDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Mobile = c.String(nullable: false, maxLength: 15),
                        Profession = c.String(nullable: false, maxLength: 100),
                        Website = c.String(nullable: false, maxLength: 100),
                        Github = c.String(nullable: false, maxLength: 100),
                        Twitter = c.String(nullable: false, maxLength: 100),
                        Instagram = c.String(nullable: false, maxLength: 100),
                        Facebook = c.String(nullable: false, maxLength: 100),
                        ProfilePicture = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        idUser = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idUser);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserProfile");
        }
    }
}
