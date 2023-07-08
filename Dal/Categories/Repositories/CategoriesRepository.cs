using Dal.Base.Repositories;
using Dal.Categories.Entity;
using Dal.Categories.Repositories.Interface;
using Dal.Operation.Entity;
using Dal.User.Entity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X9;

namespace Dal.Categories.Repositories;

public class CategoriesRepository : BaseRepository<CategoriesDal, Guid>, ICategoriesRepository
{
    private readonly DataContext _context;
    
    public CategoriesRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<List<CategoriesDal>> GetAllUserCategory(string userId, string type)
    {
        var categories = await _context
            .Set<CategoriesDal>()
            .Where(x => (x.UserDal.Id == userId) && x.Type == type)
            .ToListAsync();
        return categories;
    }

    public async Task<int?> GetSumCurrentMonth(Guid catId, string userId, DateTime date)
    {
        return  await _context.Set<CategoriesDal>()
            .Where(x => x.Id == catId)
            .Include(x => x.OperationList)
            .SelectMany(x => x.OperationList)
            .Where(x => x.UserDal.Id == userId)
            .Where(x => x.DateTime.Value.Year == date.Year &&
                        x.DateTime.Value.Month == date.Month)
            .Select(x => x.Price)
            .SumAsync();
    }
    
    public async Task<int?> GetSumCategory(Guid catId, string userId)
    {
        return  await _context.Set<CategoriesDal>()
            .Where(x => x.Id == catId)
            .Include(x => x.OperationList)
            .SelectMany(x => x.OperationList)
            .Where(x => x.UserDal.Id == userId)
            .Select(x => x.Price)
            .SumAsync();
    }
    
    public async Task<int?> GetSumCategoryFromTo(string userId, Guid catId, DateTime from, DateTime to, string type)
    {
        return  await _context.Set<CategoriesDal>()
            .Where(x => x.Id == catId)
            .Include(x => x.OperationList)
            .SelectMany(x => x.OperationList)
            .Where(x => x.UserDal.Id == userId && from <= x.DateTime.Value && x.DateTime.Value <= to)
            .Select(x => x.Price)
            .SumAsync();
        
    }

    public async Task<List<OperationDal>> GetOperations(Guid id)
    {
        return await _context.Set<CategoriesDal>()
            .Include(x => x.OperationList)
            .SelectMany(x => x.OperationList)
            .Where(x => x.CategoriesDal.Id == id)
            .ToListAsync();
    }
}