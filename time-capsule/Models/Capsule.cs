using Models;

namespace time_capsule.Models
{
    public class Capsule
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public string? ImagePath { get; set; } 
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Notifications> Notifications { get; set; } = new List<Notifications>();
    }
}