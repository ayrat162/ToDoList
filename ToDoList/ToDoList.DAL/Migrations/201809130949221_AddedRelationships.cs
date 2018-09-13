namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BossId = c.String(maxLength: 128),
                        WorkerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BossId)
                .ForeignKey("dbo.AspNetUsers", t => t.WorkerId)
                .Index(t => t.BossId)
                .Index(t => t.WorkerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "WorkerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Relationships", "BossId", "dbo.AspNetUsers");
            DropIndex("dbo.Relationships", new[] { "WorkerId" });
            DropIndex("dbo.Relationships", new[] { "BossId" });
            DropTable("dbo.Relationships");
        }
    }
}
