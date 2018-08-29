namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileToTaskView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoTasks", "Image");
        }
    }
}
