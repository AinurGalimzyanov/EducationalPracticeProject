using Dal.Base.Repositories.Interface;
using Dal.Email.Entity;
using Dal.Operation.Entity;

namespace Dal.Email.Repositories.Interface;

public interface IEmailRepository : IBaseRepository<EmailDal, Guid>
{
    
}