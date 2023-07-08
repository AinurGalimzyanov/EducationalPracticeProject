using Dal.Base.Repositories.Interface;
using Dal.User.Entity;

namespace Dal.User.Repositories.Interface;

public interface IUserRepository 
{
    Task<Guid> UpdateRefreshToken(UserDal user, string refreshToken);
    Task<UserDal> GetByUserEmail(string userEmail);
}