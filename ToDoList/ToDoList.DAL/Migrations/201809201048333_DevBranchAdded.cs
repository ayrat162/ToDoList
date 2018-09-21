namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DevBranchAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relationships", "BossId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Relationships", "WorkerId", "dbo.AspNetUsers");
            DropIndex("dbo.Relationships", new[] { "BossId" });
            DropIndex("dbo.Relationships", new[] { "WorkerId" });
            DropTable("dbo.Relationships");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BossId = c.String(maxLength: 128),
                        WorkerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Relationships", "WorkerId");
            CreateIndex("dbo.Relationships", "BossId");
            AddForeignKey("dbo.Relationships", "WorkerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Relationships", "BossId", "dbo.AspNetUsers", "Id");
        }
    }
}
