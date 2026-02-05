using System.Collections.Generic;

namespace ECommerce.Core
{
    // BaseEntity'den miras alıyoruz (Id ve Tarih özellikleri otomatik geldi)
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        // İLİŞKİ: Bir kategorinin içinde birden fazla ürün olabilir (One-to-Many)
        // ICollection bir liste türüdür.
        public ICollection<Product> Products { get; set; }
    }
}