using Dal.Message.Entity;
using Logic.Managers.Base.Interface;

namespace Api.Managers.Messager.Interface;

public interface IMessagerManager : IBaseManager<MessageDal, Guid>
{
    
}