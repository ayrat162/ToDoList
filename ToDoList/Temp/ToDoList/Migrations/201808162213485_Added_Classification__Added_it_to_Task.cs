namespace ToDoList.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Added_Classification__Added_it_to_Task : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Color = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ToDoTasks", "ClassificationId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToDoTasks", "ClassificationId");
            AddForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications");
            DropIndex("dbo.ToDoTasks", new[] { "ClassificationId" });
            DropColumn("dbo.ToDoTasks", "ClassificationId");
            DropTable("dbo.Classifications");
        }
    }
}
