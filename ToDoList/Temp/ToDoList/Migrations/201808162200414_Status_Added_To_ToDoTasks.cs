namespace ToDoList.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Status_Added_To_ToDoTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoTasks", "Status");
        }
    }
}
