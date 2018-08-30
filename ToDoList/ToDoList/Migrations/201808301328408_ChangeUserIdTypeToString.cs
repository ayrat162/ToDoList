namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIdTypeToString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ToDoTasks", new[] { "User_Id" });
            DropColumn("dbo.ToDoTasks", "UserId");
            RenameColumn(table: "dbo.ToDoTasks", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.ToDoTasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ToDoTasks", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            AlterColumn("dbo.ToDoTasks", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ToDoTasks", name: "UserId", newName: "User_Id");
            AddColumn("dbo.ToDoTasks", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToDoTasks", "User_Id");
        }
    }
}
