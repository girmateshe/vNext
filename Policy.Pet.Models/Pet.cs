namespace Policy.Pets.Models
{
    public class Pet : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public int OwnerId { get; set; }
    }
}