using System.ComponentModel.DataAnnotations;
using Dal.Base.Entity;
using Dal.Categories.Entity;
using Dal.User.Entity;

namespace Dal.Operation.Entity;

public class OperationDal : BaseDal<Guid>
{
    public int? Price { get; set; }
    
    public DateTime? DateTime { get; set; }

    public CategoriesDal? CategoriesDal { get; set; }
    
    public UserDal? UserDal { get; set; }
}