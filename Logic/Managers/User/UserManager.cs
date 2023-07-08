using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api;
using AutoMapper;
using Dal.User.Entity;
using Logic.Managers.User.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Logic.Managers.User;

public class UserManager
{
    private readonly SignInManager<UserDal> _signInManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly IMapper _mapper;
    private readonly JWTSettings _options;

    public UserManager(SignInManager<UserDal> signInManager, UserManager<UserDal> userManager, IMapper mapper, JWTSettings options)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapper = mapper;
        _options = options;
    }

    public async Task<string> RecoverPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var newPassword = Guid.NewGuid().ToString();
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, newPassword);
        return newPassword;
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
    
    private async Task<UserDal> FindUserByToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        if (jwt.ValidTo < DateTime.UtcNow) return null;
        var email = jwt.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        return await _userManager.FindByEmailAsync(email);;
    }

    /*public async Task<RegistModelResponse> RegisterUser(RegisterModelRequest model)
    {
        var user = _mapper.Map<UserDal>(model);
        await _userManager.CreateAsync(user, model.Password);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name)
        };
        await _userManager.AddClaimsAsync(user, claims);
        var accessToken = GetToken(claims, 15);
        var refreshToken = GetToken(claims, 43200);
        user.RefreshToken = refreshToken;
        await _userManager.UpdateAsync(user);
        return new RegistModelResponse("Bearer " + accessToken, user.Name, user.Email);
        
    }

    public async Task<SingInModelResponse> SignInWithAccess(string token)
    {
        var user = await FindUserByToken(token);
        var claims = await _userManager.GetClaimsAsync(user);
        var accessToken = GetToken(claims, 15);
        return new SingInModelResponse("Bearer " + accessToken, user.Name, user.Email, user.Balance);
    }

    public async Task<RefreshModelResponse> RefreshToken(string refreshToken)
    {
        var user = await FindUserByToken(refreshToken);

        var claims = await _userManager.GetClaimsAsync(user);
        var newAccessToken = GetToken(claims, 15);
        var newRefreshToken = GetToken(claims, 43200);
        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);
        return new RefreshModelResponse("Bearer " + newAccessToken);
        
    }*/
}