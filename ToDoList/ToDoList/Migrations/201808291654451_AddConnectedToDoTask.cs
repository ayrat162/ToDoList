namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnectedToDoTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "ConnectedtoDoTaskId", c => c.Int());
            CreateIndex("dbo.ToDoTasks", "ConnectedtoDoTaskId");
            AddForeignKey("dbo.ToDoTasks", "ConnectedtoDoTaskId", "dbo.ToDoTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "ConnectedtoDoTaskId", "dbo.ToDoTasks");
            DropIndex("dbo.ToDoTasks", new[] { "ConnectedtoDoTaskId" });
            DropColumn("dbo.ToDoTasks", "ConnectedtoDoTaskId");
        }
    }
}
