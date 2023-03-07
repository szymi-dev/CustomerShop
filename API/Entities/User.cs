using System.Collections.Generic;

namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> LikedProducts { get; set; } = new List<Product>();
        public List<Message> MessagesSent { get; set; }
        public List<Message> MessagesRecieved { get; set; }
    }
}