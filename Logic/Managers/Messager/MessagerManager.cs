using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Managers.Messager.Interface;
using Dal.Base.Repositories.Interface;
using Dal.Message.Entity;
using Dal.Message.Repositories.Interface;
using Dal.User.Entity;
using Logic.Managers.Base;
using Microsoft.AspNetCore.Identity;

namespace Api.Managers.Messager;

public class MessagerManager : BaseManager<MessageDal, Guid>, IMessagerManager
{
    private readonly UserManager<UserDal> _userManager;
    private readonly IMessageRepository _messageRepository;
    
    public MessagerManager(IMessageRepository repository, UserManager<UserDal> userManager) : base(repository)
    {
        _userManager = userManager;
        _messageRepository = repository;
    }
    
    private async Task<UserDal> FindUser(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        if (jwt.ValidTo < DateTime.UtcNow) return null;
        var email = jwt.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        return await _userManager.FindByEmailAsync(email);
    }
    
    public async Task CreateMessage(string token, MessageDal messageDal)
    {
        var user = await FindUser(token);
        messageDal.UserDal = user;
        await InsertAsync(messageDal);
    }

    public async Task<List<MessageDal>> GetMessages(string token)
    {
        var user = await FindUser(token);
        return await _messageRepository.GetMessagesAsync(user.Id);
    }
}