using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface ICursoRepository : IRepository<CursoEntity>
    {
        Task<ICollection<CursoEntity>> GetAllUnsubscribeAsync(string userId);
        Task<bool> DeleteParticipanteAsync(int cursoid, string userId);
        Task<ICollection<CursoEntity>> GetAllTimes();

    }
}
