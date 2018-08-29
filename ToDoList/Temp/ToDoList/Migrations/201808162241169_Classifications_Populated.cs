namespace ToDoList.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Classifications_Populated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoTasks", "Description", c => c.String(nullable: false, maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoTasks", "Description", c => c.String());
        }
    }
}
