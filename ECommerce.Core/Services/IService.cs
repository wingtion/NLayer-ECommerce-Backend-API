using System.Linq.Expressions;

namespace ECommerce.Core.Services
{
    // IGenericRepository'ye çok benzer ama dönüş tipleri farklı olabilir.
    // Burası veritabanı sorgusu (IQueryable) değil, bitmiş veri (IEnumerable/List) döner.
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(); // Listeleme
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // Filtreleme
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); // Var mı yok mu kontrolü

        Task<T> AddAsync(T entity); // Ekleme
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities); // Çoklu Ekleme

        Task UpdateAsync(T entity); // Güncelleme
        Task RemoveAsync(T entity); // Silme
        Task RemoveRangeAsync(IEnumerable<T> entities); // Çoklu Silme
    }
}