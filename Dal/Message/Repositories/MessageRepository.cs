using Dal.Base.Entity;
using Dal.Base.Repositories;
using Dal.Message.Entity;
using Dal.Message.Repositories.Interface;
using Dal.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dal.Message.Repositories;

public class MessageRepository : BaseRepository<MessageDal, Guid>, IMessageRepository
{
    private readonly DataContext _context;
    
    public MessageRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<List<MessageDal>> GetMessagesAsync(string userId)
    {
        return await _context
            .Set<MessageDal>()
            .Where(x => x.UserDal.Id == userId)
            .OrderByDescending(x => x.DateTime)
            .ToListAsync();
    }
}