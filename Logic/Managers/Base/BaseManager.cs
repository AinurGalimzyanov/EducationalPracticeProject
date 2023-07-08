using Dal.Base.Entity;
using Dal.Base.Repositories.Interface;
using Logic.Managers.Base.Interface;

namespace Logic.Managers.Base;

public class BaseManager<T, TI> : IBaseManager<T, TI> where T : BaseDal<TI>
{
    protected readonly IBaseRepository<T, TI> Repository;

    public BaseManager(IBaseRepository<T, TI> repository)
    {
        Repository = repository;
    }

    public async Task<TI> InsertAsync(T dal)
    {
        return await Repository.InsertAsync(dal);
    }

    public async Task DeleteAsync(TI id)
    { 
        await Repository.DeleteAsync(id);
    }

    public async Task<T?> GetAsync(TI id)
    {
        return await Repository.GetAsync(id);
    }

    public async Task<List<T?>> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public async Task<TI> UpdateAsync(T dal)
    {
        return await Repository.UpdateAsync(dal);
    }
}