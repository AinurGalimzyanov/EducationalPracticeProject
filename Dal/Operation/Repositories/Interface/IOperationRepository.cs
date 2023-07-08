using Dal.Base.Repositories.Interface;
using Dal.Categories.Entity;
using Dal.Operation.Entity;

namespace Dal.Operation.Repositories.Interface;

public interface IOperationRepository : IBaseRepository<OperationDal, Guid>
{
    public Task<List<OperationDal>> GetAllOperationByCategoryAsync(string userId, Guid? categoryId, DateTime date);
    public Task<List<OperationDal>> GetAllOperationByTypeAsync(string userId, string type, DateTime date);
    public Task<List<OperationDal>> GetAllOperationsAsync(string userId, DateTime date);
    public Task<string> GetNameCategoryAsync(Guid operationId);
    public Task<CategoriesDal> GetOperationCategory(Guid operationId);
    public Task<List<OperationDal>> GetOperationsByTypeDynamically(string userId, DateTime from, DateTime to,
        string type);
}