using Dal.Base.Entity;
using Dal.Base.Repositories;
using Dal.Message.Entity;
using Dal.Message.Repositories.Interface;
using Dal.User.Entity;

namespace Dal.Message.Repositories;

public class MessageRepository : BaseRepository<MessageDal, Guid>, IMessageRepository
{
    public MessageRepository(DataContext context) : base(context)
    {
    }
}