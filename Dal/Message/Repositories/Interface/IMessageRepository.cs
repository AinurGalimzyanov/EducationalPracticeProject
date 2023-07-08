using Dal.Base.Repositories.Interface;
using Dal.Message.Entity;

namespace Dal.Message.Repositories.Interface;

public interface IMessageRepository : IBaseRepository<MessageDal, Guid>
{
    public Task<List<MessageDal>> GetMessagesAsync(string userId);
}