namespace ToDoList.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Populate_Classification_Types : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Classifications (Name, Color) VALUES ('No Classification', 16776960)");
            Sql("INSERT INTO Classifications (Name, Color) VALUES ('Home', 65280)");
            Sql("INSERT INTO Classifications (Name, Color) VALUES ('Work', 255)");
            Sql("INSERT INTO Classifications (Name, Color) VALUES ('Other', 16776960)");
        }
        
        public override void Down()
        {
        }
    }
}
