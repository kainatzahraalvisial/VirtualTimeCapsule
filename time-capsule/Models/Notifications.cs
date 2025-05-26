using time_capsule.Models;

namespace Models
{
    public class Notifications
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int CapsuleId { get; set; }
        public Capsule? Capsule { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}