using System.Linq.Expressions;

namespace ECommerce.Core.Repositories
{
    // <T> demek: Bana hangi tabloyu verirsen onun için çalışırım demek.
    public interface IGenericRepository<T> where T : class
    {
        // Id'ye göre veri getirme (Asenkron)
        Task<T> GetByIdAsync(int id);

        // Tüm verileri getirme
        IQueryable<T> GetAll();

        // Şarta göre veri getirme 
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        // Veri veritabanına eklenir ama kaydedilmez (Memory'de tutulur)
        Task AddAsync(T entity);

        void Remove(T entity);
        void Update(T entity);
    }
}