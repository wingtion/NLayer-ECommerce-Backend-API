namespace ECommerce.Core
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        // İLİŞKİ: Her ürünün bir kategorisi olmak ZORUNDA.
        public int CategoryId { get; set; } // Yabancı Anahtar (Foreign Key)

        // Bu özellik kod içinde rahatça kategoriye ulaşmak için (Navigation Property)
        public Category Category { get; set; }
    }
}