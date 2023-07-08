using Dal.Base.Entity;
using Dal.Base.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dal.Base.Repositories;

public class BaseRepository<T, TI> : IBaseRepository<T, TI> where T : BaseDal<TI>
{
    private readonly DataContext _context;
    private readonly DbSet<T> _dbSet;
    
    public BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task<TI> InsertAsync(T dal)
    {
        var entity = await _dbSet.AddAsync(dal);
        await _context.SaveChangesAsync();
        return entity.Entity.Id;
    }

    public async Task DeleteAsync(TI id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<T?> GetAsync(TI id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }
    
    public async Task<List<T?>> GetAllAsync()
    {
        var entitys = await _dbSet.ToListAsync();
        return entitys;
    }

    public async Task<TI> UpdateAsync(T dal)
    {
        _context.Entry(dal).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return dal.Id;
    }
}