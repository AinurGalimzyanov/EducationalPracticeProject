using System.IdentityModel.Tokens.Jwt;
using Api.Controllers.Public.Base;
using Api.Controllers.Public.Categories.Dto.Request;
using Api.Controllers.Public.Categories.Dto.Response;
using Api.Controllers.Public.Message.Dto.Request;
using Api.Controllers.Public.Message.Dto.Response;
using Api.Managers.Messager.Interface;
using AutoMapper;
using Dal.Categories.Entity;
using Dal.Message.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Public.Message;

public class MessageController : BasePublicController
{
    private readonly IMessagerManager _messagerManager;
    private readonly IMapper _mapper;

    public MessageController(IMessagerManager messagerManager, IMapper mapper)
    {
        _messagerManager = messagerManager;
        _mapper = mapper;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("getMessages")]
    public async Task<IActionResult> CreateCategory([FromQuery] MessageRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var list = await _messagerManager.GetMessages(token);
        if (list.Count - model.Count * model.Page < -model.Count) return BadRequest();
        var skipValue = model.Count == 0 ? 0 : list.Count - model.Count * model.Page;
        var takeValue = model.Count == 0 ? list.Count : (skipValue > -model.Count && skipValue < 0) ? list.Count - model.Count * (model.Page - 1) : model.Count;
        var result = list.Skip(skipValue).Take(takeValue).Select(x => _mapper.Map<GetMessage>(x)).ToList();
        return Ok(new GetMessages(result));
    }
    
    private bool CheckNotValidAccess(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        return jwt.ValidTo < DateTime.UtcNow;
    }
}