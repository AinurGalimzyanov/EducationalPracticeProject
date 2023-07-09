using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Dal.Base.Repositories.Interface;
using Dal.Categories.Entity;
using Dal.Categories.Repositories.Interface;
using Dal.Operation.Entity;
using Dal.Operation.Repositories.Interface;
using Dal.User.Entity;
using Logic.Managers.Base;
using Logic.Managers.Categories.Interface;
using Logic.Managers.Operation.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Logic.Managers.Operation;

public class OperationManager : BaseManager<OperationDal, Guid>, IOperationManager
{
    private readonly UserManager<UserDal> _userManager;
    private readonly IOperationRepository _operationRepository;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public OperationManager(IOperationRepository repository,
        UserManager<UserDal> userManager, ICategoriesRepository categoriesRepository, IMapper mapper) : base(repository)
    {
        _userManager = userManager;
        _operationRepository = repository;
        _categoriesRepository = categoriesRepository;
        _mapper = mapper;
    }

    private async Task<UserDal> FindUser(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        if (jwt.ValidTo < DateTime.UtcNow) return null;
        var email = jwt.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        return await _userManager.FindByEmailAsync(email);
    }

   

    public async Task CreateOperation(string token, OperationDal operation, Guid categoryId)
    {
        var user = await FindUser(token);
        operation.CategoriesDal = await _categoriesRepository.GetAsync(categoryId);
        operation.UserDal = user;
        var currentPrice = operation.CategoriesDal.Type == "income" ? operation.Price : -operation.Price;
        var isSet = await SetBalanceAfterNewOperation(user, currentPrice);
        if (isSet)
        {
            await _operationRepository.InsertAsync(operation);
        }
        else
        {
            throw new ArgumentException("На балансе недостаточно срдеств");
        }
    }

    private async Task<bool> SetBalanceAfterNewOperation(UserDal user, decimal? currentPrice)
    {
        var balance = await GetBalanceAsync(user.RefreshToken);
        var newBalance = balance + currentPrice;

        if (newBalance >= 0)
        {
            user.Balance = newBalance;
            await _userManager.UpdateAsync(user);
            return true;
        }

        return false;
    }
    
    public async Task<List<OperationDal>> GetAllOperations(string token, DateTime dateTime)
    {
        var user = await FindUser(token);
        var operations = await _operationRepository.GetAllOperationsAsync(user.Id, dateTime);
        return operations;
    }

    public async Task<(List<OperationDal>, List<OperationDal>)> GetOperationsByTypeAsync(string token, DateTime dateTime)
    {
        var user = await FindUser(token);
        var operationsIncome = await _operationRepository.GetAllOperationByTypeAsync(user.Id, "income", dateTime);
        var operationsExpenses = await _operationRepository.GetAllOperationByTypeAsync(user.Id, "expenses", dateTime);
        return new(operationsIncome, operationsExpenses);
    }

    public async Task<List<OperationDal>> GetOperationsByCategoryAsync(string token, Guid categoryId, DateTime dateTime)
    {
        var user = await FindUser(token);
        var operations = await _operationRepository.GetAllOperationByCategoryAsync(user.Id, categoryId, dateTime);
        return operations;
    }

    public async Task<decimal?> GetBalanceAsync(string token)
    {
        var user = await FindUser(token);
        return user.Balance;
    }


    public async Task CreateBalanceAsync(string token, decimal newBalance)
    {
        var user = await FindUser(token);
        user.Balance = newBalance;
        await _userManager.UpdateAsync(user);
    }
    
    
    public async Task DeleteOperation(Guid operationId, string token)
    {
        var user = await FindUser(token);
        var operation = await GetAsync(operationId);
        var category = await _operationRepository.GetOperationCategory(operation.Id);
        var price = category.Type == "income" ? -operation.Price : operation.Price;
        user.Balance += price;
        await _userManager.UpdateAsync(user);
        await DeleteAsync(operationId);
    }

    public async Task UpdateOperation(string token, OperationDal operation, decimal oldPrice)
    {
        var user = await FindUser(token);
        await UpdateAsync(operation);
        var category = await _operationRepository.GetOperationCategory(operation.Id);
        var difference = 0;
        if (category.Type == "income")
        {
            difference = (int)(operation.Price - oldPrice);
        }

        if (category.Type == "expenses")
        {
            difference = (int)(oldPrice - operation.Price);
        }
        
        user.Balance += difference;
        await _userManager.UpdateAsync(user);
    }

    public async Task<OperationDal> GetOperation(Guid id)
    {
        var operation = await GetAsync(id);
        return operation;
    }

    public async Task<decimal?> GetSumByCategoryAsync(string token, Guid categoryId, DateTime dateTime)
    {
        var user = await FindUser(token);
        var operations =
            await _operationRepository.GetAllOperationByCategoryAsync(user.Id, categoryId, dateTime);
        var sum = operations.Select(x => x.Price).Sum();
        return sum;
    }
    
    public async Task<decimal?> GetSumByTypeAsync(string token, string type, DateTime dateTime)
    {
        var user = await FindUser(token);
        var operations = await _operationRepository.GetAllOperationByTypeAsync(user.Id, type, dateTime);
        var sum = operations.Select(x => x.Price).Sum();
        return sum;
    }

    public async Task<string> GetNameCategory(Guid operationId)
    {
        return await _operationRepository.GetNameCategoryAsync(operationId);
    }

    public async Task<List<OperationDal>> GetOperationsByTypeDynamically(string token, DateTime from, DateTime to, string type)
    {
        var user = await FindUser(token);
        return await _operationRepository.GetOperationsByTypeDynamically(user.Id, from, to, type);
    }
}