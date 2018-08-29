namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeClassificationNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications");
            DropIndex("dbo.ToDoTasks", new[] { "ClassificationId" });
            AlterColumn("dbo.ToDoTasks", "ClassificationId", c => c.Int());
            CreateIndex("dbo.ToDoTasks", "ClassificationId");
            AddForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications");
            DropIndex("dbo.ToDoTasks", new[] { "ClassificationId" });
            AlterColumn("dbo.ToDoTasks", "ClassificationId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToDoTasks", "ClassificationId");
            AddForeignKey("dbo.ToDoTasks", "ClassificationId", "dbo.Classifications", "Id", cascadeDelete: true);
        }
    }
}
