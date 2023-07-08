using Dal.Categories.Entity;
using Dal.Operation.Entity;
using Dal.User.Entity;
using Logic.Managers.Base.Interface;

namespace Logic.Managers.Categories.Interface;

public interface ICategoriesManager : IBaseManager<CategoriesDal, Guid>
{
     public Task<int?> CreateCategories(string token, CategoriesDal dal);
     
     public Task<(List<CategoriesDal>,List<CategoriesDal>)> GetAllCategoriesByType(string token);

     public Task<int?> GetSumCategory(Guid categoryId, string token);

     public Task<int?> GetSumCategoryFromTo(string token, Guid catId, DateTime from, DateTime to, string type);

     public Task AddStaticCategories(UserDal user);

     public Task DeleteCategory(Guid id, string token);

     public Task UpdateCategory(CategoriesDal dal, string token);

     public Task<int?> GetSumCurrentMonth(Guid categoryId, string token, DateTime date);
}