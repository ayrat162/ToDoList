namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClassificationConnectedToDoTaskPictureToTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ToDoTasks", "ClassificationId", c => c.Int());
            AddColumn("dbo.ToDoTasks", "ConnectedtoDoTaskId", c => c.Int());
            AddColumn("dbo.ToDoTasks", "PictureId", c => c.Int());
            AlterColumn("dbo.ToDoTasks", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ToDoTasks", "ClassificationId");
            CreateIndex("dbo.ToDoTasks", "ConnectedtoDoTaskId");
            CreateIndex("dbo.ToDoTasks", "PictureId");
            CreateIndex("dbo.ToDoTasks", "UserId");
            AddForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications", "Id");
            AddForeignKey("dbo.ToDoTasks", "ConnectedtoDoTaskId", "dbo.ToDoTasks", "Id");
            AddForeignKey("dbo.ToDoTasks", "PictureId", "dbo.Pictures", "Id");
            AddForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ToDoTasks", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.ToDoTasks", "ConnectedtoDoTaskId", "dbo.ToDoTasks");
            DropForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications");
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            DropIndex("dbo.ToDoTasks", new[] { "PictureId" });
            DropIndex("dbo.ToDoTasks", new[] { "ConnectedtoDoTaskId" });
            DropIndex("dbo.ToDoTasks", new[] { "ClassificationId" });
            AlterColumn("dbo.ToDoTasks", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.ToDoTasks", "PictureId");
            DropColumn("dbo.ToDoTasks", "ConnectedtoDoTaskId");
            DropColumn("dbo.ToDoTasks", "ClassificationId");
            DropTable("dbo.Pictures");
            CreateIndex("dbo.ToDoTasks", "UserId");
            AddForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
