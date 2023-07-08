using Api.Controllers.Public.Base;
using Api.Controllers.Public.Categories.Dto.Request;
using Api.Controllers.Public.Categories.Dto.Response;
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
    [HttpPost("getMessages")]
    public async Task<IActionResult> CreateCategory()
    {
        var list = await _messagerManager.GetAllAsync();
        var result = list.Select(x => _mapper.Map<GetMessage>(x)).ToList();
        return Ok(new GetMessages(result));
    }
}