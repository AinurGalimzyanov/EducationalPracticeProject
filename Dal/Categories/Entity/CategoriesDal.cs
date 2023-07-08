using System.ComponentModel.DataAnnotations;
using Dal.Base.Entity;
using Dal.Operation.Entity;
using Dal.User.Entity;

namespace Dal.Categories.Entity;

public class CategoriesDal : BaseDal<Guid>
{
    [MaxLength(255)] 
    public string? Name { get; set; }

    public UserDal? UserDal { get; set; }
    
    public string? Img { get; set; }
    
    public  string? Type { get; set; }

    public List<OperationDal>? OperationList { get; set; }
}