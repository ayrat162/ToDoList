namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeUserIdRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ToDoTasks", new[] { "ConnectedtoDoTaskId" });
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            AlterColumn("dbo.ToDoTasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ToDoTasks", "ConnectedToDoTaskId");
            CreateIndex("dbo.ToDoTasks", "UserId");
            AddForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            DropIndex("dbo.ToDoTasks", new[] { "ConnectedToDoTaskId" });
            AlterColumn("dbo.ToDoTasks", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ToDoTasks", "UserId");
            CreateIndex("dbo.ToDoTasks", "ConnectedtoDoTaskId");
            AddForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
