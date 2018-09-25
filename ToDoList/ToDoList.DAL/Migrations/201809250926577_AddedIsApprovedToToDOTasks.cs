namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsApprovedToToDOTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoTasks", "IsApproved");
        }
    }
}
