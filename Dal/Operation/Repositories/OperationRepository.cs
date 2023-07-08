using Dal.Base.Repositories;
using Dal.Categories.Entity;
using Dal.Operation.Entity;
using Dal.Operation.Repositories.Interface;
using Dal.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dal.Operation.Repositories;

public class OperationRepository : BaseRepository<OperationDal, Guid>, IOperationRepository
{
    private readonly DataContext _context;
    public OperationRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<OperationDal>> GetAllOperationByCategoryAsync(string userId, Guid? categoryId, DateTime date)
    {
        var operations = await _context
            .Set<OperationDal>()
            .Where(x => x.UserDal.Id == userId && x.CategoriesDal.Id == categoryId)
            .Where(x => x.DateTime.Value.Year == date.Year &&
                        x.DateTime.Value.Month == date.Month)
            .OrderBy(x => x.DateTime)
            .ToListAsync();
        return operations;
    }

    public async Task<List<OperationDal>> GetAllOperationByTypeAsync(string userId, string type, DateTime date)
    {
        var operations = await _context
            .Set<OperationDal>()
            .Where(x => x.UserDal.Id == userId && x.CategoriesDal.Type == type )
            .Where(x => x.DateTime.Value.Year == date.Year &&
                        x.DateTime.Value.Month == date.Month)
            .OrderBy(x => x.DateTime)
            .ToListAsync();
        return operations;
    }
    
    public async Task<List<OperationDal>> GetOperationsByTypeDynamically(string userId, DateTime from, DateTime to, string type)
    {
        return await _context.Set<OperationDal>()
            .Where(x => x.UserDal.Id == userId && x.CategoriesDal.Type == type )
            .Where(y => from <= y.DateTime.Value && y.DateTime.Value <= to)
            .OrderBy(x => x.DateTime.Value)
            .ToListAsync();
    }

    public async Task<List<OperationDal>> GetAllOperationsAsync(string userId, DateTime date)
    {
        var operations = await _context
            .Set<OperationDal>()
            .Where(x => x.UserDal.Id == userId)
            .Where(x => x.DateTime.Value.Year == date.Year &&
                        x.DateTime.Value.Month == date.Month)
            .OrderBy(x => x.DateTime)
            .ToListAsync();
        return operations;
    }

    public async Task<string> GetNameCategoryAsync(Guid operationId)
    {
        var dal = await _context
            .Set<OperationDal>()
            .Where(x => x.Id == operationId)
            .Include(x => x.CategoriesDal)
            .Select(x => x.CategoriesDal)
            .FirstOrDefaultAsync();

        return dal.Name;
    }

    public async Task<CategoriesDal> GetOperationCategory(Guid operationId)
    {
        var category = await _context
            .Set<OperationDal>()
            .Where(x => x.Id == operationId)
            .Include(x => x.CategoriesDal)
            .Select(x => x.CategoriesDal)
            .FirstOrDefaultAsync();

        return category;
    }
    
    
}

