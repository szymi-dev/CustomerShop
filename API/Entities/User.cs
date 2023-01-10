using System.Collections.Generic;

namespace API.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Product> Products { get; set; }
    }
}