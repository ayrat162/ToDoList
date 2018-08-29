namespace ToDoList.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Unnecessary_Classifications : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Classifications WHERE Id >=5");
        }
    }
}
