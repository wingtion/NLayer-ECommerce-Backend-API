namespace ECommerce.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        // Değişiklikleri veritabanına asenkron olarak kaydeder
        Task CommitAsync();

        // Değişiklikleri senkron olarak kaydeder (Bazen gerekir)
        void Commit();
    }
}