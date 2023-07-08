using Dal.Email.Entity;
using Dal.Email.Repositories.Interface;

namespace Dal.Email.Repositories;

public class EmailRepository : IEmailRepository
{
    public Task<Guid> InsertAsync(EmailDal dal)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<EmailDal?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmailDal?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Guid> UpdateAsync(EmailDal dal)
    {
        throw new NotImplementedException();
    }
}