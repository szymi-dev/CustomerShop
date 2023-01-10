namespace API.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        // public string Descripiton { get; set; }
        // public string PictureUrl { get; set; }
        // public ProductType ProductType { get; set; }
        // public int ProductTypeId { get; set; }
        // public ProductBrand ProductBrand { get; set; }
        // public int ProductBrandId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}