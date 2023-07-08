using Dal.Categories.Entity;
using Dal.User.Entity;
using Dal.User.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dal.User.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    
    public UserRepository(DataContext context)
    {
        _context = context;
    }


    public List<UserDal> GetAll() =>  _context.Set<UserDal>().ToList();
    
    public async Task<UserDal?> GetByIdAsync(Guid id) => await _context.Set<UserDal>().FindAsync(id);

    public async Task<Guid> InsertAsync(UserDal user)
    {
         var a = await _context.Set<UserDal>().AddAsync(user);
         await _context.SaveChangesAsync();
         return Guid.Parse(a.Entity.Id);
    }

    
    
    public Task<Guid> UpdateRefreshToken(UserDal user, string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserDal> GetByUserEmail(string userEmail)
    {
        throw new NotImplementedException();
    }
}