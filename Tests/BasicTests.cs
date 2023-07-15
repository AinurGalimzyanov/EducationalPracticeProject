using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api;
using Api.Managers.Messager.Interface;
using Dal.User.Entity;
using Logic.Managers.Categories.Interface;
using Logic.Managers.Operation.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests;

public class BasicTests
{
    private readonly ICategoriesManager _categoriesManager;
    private readonly IOperationManager _operationManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly IMessagerManager _messagerManager;
    private readonly JWTSettings _options;

    public BasicTests(ICategoriesManager categoriesManager, 
        IOperationManager operationManager,
        UserManager<UserDal> userManager,
        IMessagerManager messagerManager,
        IOptions<JWTSettings> options)
    {
        _categoriesManager = categoriesManager;
        _operationManager = operationManager;
        _userManager = userManager;
        _messagerManager = messagerManager;
        _options = options.Value;
    }

    private async Task<UserDal> CreateUser()
    {
        var user = new UserDal();
        user.Balance = 1000;
        user.Name = "Test";
        user.Id = "1";
        user.Email = "test@gmail.ru";
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name)
        };
        user.RefreshToken = GetToken(claims, 10);
        await _userManager.CreateAsync(user, "qweqrafddf");
        return user;
    }
    
    private string GetToken(IEnumerable<Claim> principal, int timeMin)
    {
        var claims = principal.ToList();
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));
        var token = new JwtSecurityToken
        (
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(timeMin),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return tokenHandler.WriteToken(token);
    }
    
    [Fact]
    public async void UserManager_UpdateUser()
    {
        var user = await CreateUser();
        user.Email = "test2@gmail.ru";
        await _userManager.UpdateAsync(user);
        var userEmail = await _userManager.GetEmailAsync(user);
        Assert.Equal("test2@gmail.ru", userEmail);
    } 
}