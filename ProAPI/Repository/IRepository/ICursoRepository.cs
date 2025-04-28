using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface ICursoRepository : IRepository<CursoEntity>
    {
        Task<ICollection<CursoEntity>> GetAllUnsubscribeAsync(string userId);
    }
}
