namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.ToDoTasks", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ToDoTasks", "User_Id");
            AddForeignKey("dbo.ToDoTasks", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ToDoTasks", new[] { "User_Id" });
            DropColumn("dbo.ToDoTasks", "User_Id");
            DropColumn("dbo.ToDoTasks", "UserId");
        }
    }
}
