namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ToDoTasks", "UserId");
            AddForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            DropColumn("dbo.ToDoTasks", "UserId");
        }
    }
}
