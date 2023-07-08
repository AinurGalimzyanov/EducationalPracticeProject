using Dal.Base.Repositories.Interface;
using Dal.Message.Entity;
using Dal.Message.Repositories.Interface;
using Logic.Managers.Base;

namespace Api.Managers.Messager;

public class MessagerManager : BaseManager<MessageDal, Guid>, IMessageRepository
{
    public MessagerManager(IMessageRepository repository) : base(repository)
    {
    }
}