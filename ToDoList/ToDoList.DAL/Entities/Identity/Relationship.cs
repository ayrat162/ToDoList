namespace ToDoList.DAL.Entities
{
    public class Relationship
    {
        public int Id { get; set; }
        public ApplicationUser Boss { get; set; }
        public string BossId { get; set; }
        public ApplicationUser Worker { get; set; }
        public string WorkerId { get; set; }
    }
}