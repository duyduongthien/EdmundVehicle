namespace EdmundVehicle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IdMake = c.Int(nullable: false),
                        Make_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Makes", t => t.Make_Id)
                .Index(t => t.Make_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Models", "Make_Id", "dbo.Makes");
            DropIndex("dbo.Models", new[] { "Make_Id" });
            DropTable("dbo.Models");
            DropTable("dbo.Makes");
        }
    }
}
