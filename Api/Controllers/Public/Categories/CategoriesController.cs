
using System.IdentityModel.Tokens.Jwt;
using Api.Controllers.Public.Base;
using Api.Controllers.Public.Categories.Dto.Request;
using Api.Controllers.Public.Categories.Dto.Response;
using Api.Managers.Messager.Interface;
using AutoMapper;
using Dal.Categories.Entity;
using Dal.Message.Entity;
using Logic.Managers.Categories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Public.Categories;



public class CategoriesController : BasePublicController
{
    private readonly ICategoriesManager _categoriesManager; 
    private readonly IMessagerManager _messagerManager;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoriesManager categoriesManager, IMessagerManager messagerManager, IMapper mapper)
    {
        _categoriesManager = categoriesManager;
        _messagerManager = messagerManager;
        _mapper = mapper;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoriesModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var newCategory = _mapper.Map<CategoriesDal>(model);
        var sum = await _categoriesManager.CreateCategories(token, newCategory);
        var message = new MessageDal();
        message.Message = $"Добавлена категория: {model.Name}";
        message.DateTime = DateTime.Now;
        await _messagerManager.CreateMessage(token, message);
        return Ok(new CategoryResponse(newCategory.Name, newCategory.Id, newCategory.Type, sum, newCategory.Img));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        var newCategory = _mapper.Map<CategoriesDal>(model);
        await _categoriesManager.UpdateCategory(newCategory, token);
        var message = new MessageDal();
        message.Message = $"Отредактирована категория: {model.Name}";
        message.DateTime = DateTime.Now;
        await _messagerManager.CreateMessage(token, message);
        return Ok();
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    { 
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        var category = await _categoriesManager.GetAsync(id);
        await _categoriesManager.DeleteCategory(id, token);
        var message = new MessageDal();
        message.Message = $"Удалена категория: {category.Name}";
        message.DateTime = DateTime.Now;
        await _messagerManager.CreateMessage(token, message);
        return Ok();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("allCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var categories = await _categoriesManager.GetAllCategoriesByType(token);
        var responsesIncome = new List<CategoryResponse>();
        var responsesExpenses = new List<CategoryResponse>();
        foreach (var dal in categories.Item1)
        {
            responsesIncome.Add(new CategoryResponse(dal.Name, dal.Id, dal.Type, await _categoriesManager.GetSumCurrentMonth(dal.Id, token, DateTime.Now), dal.Img));
        }
        foreach (var dal in categories.Item2)
        {
            responsesExpenses.Add(new CategoryResponse(dal.Name, dal.Id, dal.Type, await _categoriesManager.GetSumCurrentMonth(dal.Id, token, DateTime.Now), dal.Img));
        }
        return Ok(new AllCategoryByTypeResponse(responsesIncome, responsesExpenses));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("getAllCategoryFromTo")]
    public async Task<IActionResult> GetAllCategoryFromTo([FromQuery] DateTimeLimitRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var categories = await _categoriesManager.GetAllCategoriesByType(token);
        var result = model.Type == "income" ? categories.Item1 : categories.Item2;
        var responses= new List<CategoryResponse>();
        foreach (var category in  result)
        {
            responses.Add(new CategoryResponse(category.Name, category.Id, category.Type,
                await _categoriesManager.GetSumCategoryFromTo(token, category.Id, model.FromDateTime, model.ToDateTime,
                    model.Type)));
        }
        return Ok(new AllCategory(responses));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("getCategory/{id}")]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        var sum = await _categoriesManager.GetSumCategory(id, token);
        var category = await _categoriesManager.GetAsync(id);
        return Ok(new CategoryResponse(category.Name, category.Id, category.Type, sum, category.Img));
    }
    
    private bool CheckNotValidAccess(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        return jwt.ValidTo < DateTime.UtcNow;
    }
    
    [HttpGet("getPictureForCategories/{img}")]
    public async Task<IActionResult> GetPictureForCategories([FromRoute] string img)
    {
        string path = "wwwroot/_content/Dal/PictureForCategories/" + img;
        var fileType="application/octet-stream";
        var fileStream = new FileStream(path, FileMode.Open);
        return Ok(fileStream);
    }
    
    [HttpGet("getUriPicturesForCategories")]
    public async Task<IActionResult> GetPicturesForCategories()
    {
        string path = "wwwroot/_content/Dal/PictureForCategories";
        var responses = Directory
            .GetFiles(path)
            .Select(x => new PictureModelResponse(new Uri($"https://smartbudget.stk8s.66bit.ru/api/v1/public/Categories/getPictureForCategories/{x.Split("/").LastOrDefault()}")))
            .ToList();
        return Ok(new PicturesModelResponse(responses));
    }
}