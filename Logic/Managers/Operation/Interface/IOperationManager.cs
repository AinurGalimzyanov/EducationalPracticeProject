using Dal.Categories.Entity;
using Dal.Operation.Entity;
using Logic.Managers.Base.Interface;

namespace Logic.Managers.Operation.Interface;

public interface IOperationManager : IBaseManager<OperationDal, Guid>
{
    public Task CreateOperation(string token, OperationDal operation, Guid categoryId);

    public Task<List<OperationDal>> GetAllOperations(string token, DateTime dateTime);
    
    public Task<(List<OperationDal>, List<OperationDal>)> GetOperationsByTypeAsync(string token, DateTime dateTime);

    public Task<List<OperationDal>> GetOperationsByCategoryAsync(string token, Guid categoryId, DateTime dateTime);

    public Task<int?> GetBalanceAsync(string token);

    public Task CreateBalanceAsync(string token, int newBalance);

    public Task DeleteOperation(Guid operationId, string token);

    public Task UpdateOperation(string token, OperationDal operation, int oldPrice);

    public Task<OperationDal> GetOperation(Guid id);

    public Task<int?> GetSumByCategoryAsync(string token, Guid categoryId, DateTime dateTime);

    public Task<int?> GetSumByTypeAsync(string token, string type, DateTime dateTime);

    public Task<string> GetNameCategory(Guid operationId);

    public  Task<List<OperationDal>> GetOperationsByTypeDynamically(string token, DateTime from, DateTime to,
        string type);
}