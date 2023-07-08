using Dal.Base.Entity;

namespace Dal.Email.Entity;

public class EmailDal : BaseDal<Guid>
{
    public string Email { get; set; }
}