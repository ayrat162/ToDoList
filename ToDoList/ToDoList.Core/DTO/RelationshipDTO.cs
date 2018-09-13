namespace ToDoList.Core.DTO
{
    public class RelationshipDTO
    {
        public int Id { get; set; }
        public string BossUserName { get; set; }
        public string BossId { get; set; }
        public string WorkerUserName { get; set; }
        public string WorkerId { get; set; }
    }
}