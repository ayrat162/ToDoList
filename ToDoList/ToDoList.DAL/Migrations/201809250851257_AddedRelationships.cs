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
                        ChildId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BossId)
                .ForeignKey("dbo.AspNetUsers", t => t.ChildId)
                .Index(t => t.BossId)
                .Index(t => t.ChildId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "ChildId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Relationships", "BossId", "dbo.AspNetUsers");
            DropIndex("dbo.Relationships", new[] { "ChildId" });
            DropIndex("dbo.Relationships", new[] { "BossId" });
            DropTable("dbo.Relationships");
        }
    }
}
