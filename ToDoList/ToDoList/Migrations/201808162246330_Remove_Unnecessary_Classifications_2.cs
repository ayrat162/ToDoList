namespace ToDoList.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Unnecessary_Classifications_2 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Classifications WHERE Id >=5");
        }

        public override void Down()
        {
        }
    }
}
