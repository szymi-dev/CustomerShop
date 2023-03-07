using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public User Reciepient { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
        public DateTime? MessageRead { get; set; }
        public bool SenderDeleted { get; set; }
        public bool ReciepientDeleted { get; set; }
    }
}