using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Repository
{
    public class CursoRepository : ICursoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string CursoEntityCacheKey = "CursoEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        public CursoRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() >= 0;
            if (result)
            {
                ClearCache();
            }
            return result;
        }

        public void ClearCache()
        {
            _cache.Remove(CursoEntityCacheKey);
        }

        public async Task<ICollection<CursoEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(CursoEntityCacheKey, out ICollection<CursoEntity> CursosCached))
                return CursosCached;

            var CursosFromDb = await _context.Cursos.Include(p=>p.Profesor).OrderBy(c => c.FechaInicio).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(CursoEntityCacheKey, CursosFromDb, cacheEntryOptions);
            return CursosFromDb;
        }

        public async Task<ICollection<CursoEntity>> GetAllUnsubscribeAsync(string userId)
        {
            

            var CursosFromDb = await _context.Cursos.Include(p => p.Profesor).Where(curso=>!curso.Participantes.
            Any(participante=>participante.Id.Equals(userId))).OrderBy(c => c.FechaInicio).ToListAsync();

            return CursosFromDb;
        }

        

        public async Task<CursoEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(CursoEntityCacheKey, out ICollection<CursoEntity> CursosCached))
            {
                var CursoEntity = CursosCached.FirstOrDefault(c => c.Id == id);
                if (CursoEntity != null)
                    return CursoEntity;
            }

            return await _context.Cursos.Include(curso=>curso.Profesor).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Cursos.AnyAsync(c => c.Id == id);
        }

        

        public async Task<bool> CreateAsync(CursoEntity CursoEntity)
        {
            CursoEntity.CreatedDate = DateTime.Now;
            _context.Cursos.Add(CursoEntity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(CursoEntity CursoEntity)
        {
            CursoEntity.CreatedDate = DateTime.Now;
            _context.Update(CursoEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var CursoEntity = await GetAsync(id);
            if (CursoEntity == null)
                return false;

            _context.Cursos.Remove(CursoEntity);
            return await Save();
        }

        public async Task<bool> DeleteParticipanteAsync(int cursoid,string userId)
        {
            var curso= await _context.Cursos.Include(curso => curso.Participantes).FirstOrDefaultAsync(c => c.Id == cursoid);
            var user= await _context.AppUsers.FirstOrDefaultAsync(c => c.Id == userId);
            if (curso == null)
                return false;

            curso.Participantes.Remove(user);
            
            return await Save();
        }
    }
}
