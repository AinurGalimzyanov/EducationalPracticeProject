using Dal.Base.Entity;
using Dal.User.Entity;

namespace Dal.Message.Entity;

public class MessageDal : BaseDal<Guid>
{
    public string? Message { get; set; }
    public DateTime? DateTime { get; set; }
    public UserDal? UserDal { get; set; }

    public MessageDal(string message,DateTime dateTime)
    {
        Message = message;
        DateTime = dateTime;
    }
}