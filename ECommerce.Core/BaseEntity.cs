using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core
{
    // "abstract" yaptık ki bu sınıftan direkt nesne üretilmesin, sadece miras alınsın.
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Oluştuğu an şimdiki zamanı alır
        public DateTime? UpdatedDate { get; set; } // "?" işareti bu alanın boş (null) olabileceğini söyler
    }
}
