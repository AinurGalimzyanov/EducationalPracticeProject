

namespace Logic.Managers.User.Interface;

public interface IUserManager
{
    public Task<string> RecoverPassword(string email);
}