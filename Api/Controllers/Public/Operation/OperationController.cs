using System.IdentityModel.Tokens.Jwt;
using Api.Controllers.Public.Base;
using Api.Controllers.Public.Operation.Dto.request;
using Api.Controllers.Public.Operation.Dto.Response;
using Api.Managers.Messager.Interface;
using AutoMapper;
using Dal.Message.Entity;
using Dal.Operation.Entity;
using Logic.Managers.Operation.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Public.Operation;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OperationController : BasePublicController
{
    private readonly IOperationManager _operationManager;
    private readonly IMessagerManager _messagerManager;
    private readonly IMapper _mapper;

    public OperationController(IOperationManager operationManager, IMessagerManager messagerManager, IMapper mapper)
    {
        _operationManager = operationManager;
        _mapper = mapper;
        _messagerManager = messagerManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOperation([FromBody] CreateOperationModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var operation = _mapper.Map<OperationDal>(model);
        await _operationManager.CreateOperation(token,  operation, model.CategoryId);
        var categoryName = await _operationManager.GetNameCategory(operation.Id);
        await _messagerManager.InsertAsync(new MessageDal($"Добавлена операция: {categoryName} {model.Price} руб."));
        return Ok(new OperationResponse(operation.Id, operation.Price, operation.DateTime, categoryName));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateOperation([FromBody] UpdateOperationModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var operation = _mapper.Map<OperationDal>(model);
        await _operationManager.UpdateOperation(token, operation, model.OldPrice);
        var categoryName = await _operationManager.GetNameCategory(operation.Id);
        await _messagerManager.InsertAsync(new MessageDal($"Изменена операция: {categoryName} с {model.OldPrice} на {model.Price} руб."));
        return Ok(new OperationResponse(operation.Id, operation.Price, operation.DateTime, categoryName));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteOperation([FromRoute] Guid id)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var categoryName = await _operationManager.GetNameCategory(id);
        var operation = await _operationManager.GetAsync(id);
        await _operationManager.DeleteOperation(id, token);
        await _messagerManager.InsertAsync(new MessageDal($"Удалена операция: {categoryName} {operation.Price} руб."));
        return Ok();
    }
    
    [HttpGet("getOperation/{id}")]
    public async Task<IActionResult> GetOperation([FromRoute] Guid id)
    {
        var operation = await _operationManager.GetOperation(id);
        var categoryName = await _operationManager.GetNameCategory(operation.Id);
        return operation != null ? Ok(new OperationResponse(operation.Id, operation.Price, operation.DateTime,categoryName)) : BadRequest();
    }
    
    [HttpGet("getAllOperation")]
    public async Task<IActionResult> GetAllOperations([FromQuery] AllOperationModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var operations =  await _operationManager.GetAllOperations(token, model.DateTime);
        if (operations.Count - model.Count * model.Page < -model.Count) return BadRequest();
        var result = new List<OperationResponse>();
        var skipValue = model.Count == 0 ? 0 : operations.Count - model.Count * model.Page;
        var takeValue = model.Count == 0 ? operations.Count : (skipValue > -model.Count && skipValue < 0) ? operations.Count - model.Count * (model.Page - 1) : model.Count;
        foreach (var operation in operations.Skip(skipValue).Take(takeValue))
        {
            result.Add(new OperationResponse(operation.Id, operation.Price, operation.DateTime, 
                await  _operationManager.GetNameCategory(operation.Id)));
        }
        return result != null ? Ok(new AllOperationResponse(result)) : BadRequest();
    }
    
    [HttpGet("getOperationsByTypeDynamically")]
    public async Task<IActionResult> GetOperationsByTypeDynamically([FromQuery] OperationsByTypeDynamically model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var operations =  await _operationManager.GetOperationsByTypeDynamically(token, model.DateTimeFrom, model.DateTimeTo, model.Type);
        if (operations.Count - model.Count * model.Page < -model.Count) return BadRequest();
        var result = new List<OperationResponse>();
        var skipValue = model.Count == 0 ? 0 : operations.Count - model.Count * model.Page;
        var takeValue = model.Count == 0 ? operations.Count : (skipValue > -model.Count && skipValue < 0) ? operations.Count - model.Count * (model.Page - 1) : model.Count;
        foreach (var operation in operations.Skip(skipValue).Take(takeValue))
        {
            result.Add(new OperationResponse(operation.Id, operation.Price, operation.DateTime, 
                await  _operationManager.GetNameCategory(operation.Id)));
        }
        return result != null ? Ok(new AllOperationResponse(result)) : BadRequest();
    }
    
    [HttpGet("getOperationByAllCategory")]
    public async Task<IActionResult> GetOperationByAllCategory([FromQuery] OperationsByCategoryModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var operations = await _operationManager.GetOperationsByCategoryAsync(token, model.CategoryId, model.DateTime);
        var result = new List<OperationResponse>();
        foreach (var operation in operations.Skip(model.Quantity == 0 ? 0 : operations.Count - model.Quantity))
        {
            result.Add(new OperationResponse(operation.Id, operation.Price, operation.DateTime, 
                await  _operationManager.GetNameCategory(operation.Id)));
        }
        return result != null ? Ok(new AllOperationResponse(result)) : BadRequest();
    }
    
    [HttpGet("getOperationByCategory")]
    public async Task<IActionResult> GetOperationByCategory([FromQuery] OperationsByCategoryModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var operations = await _operationManager.GetOperationsByCategoryAsync(token, model.CategoryId, model.DateTime);
        var result = new List<OperationResponse>();
        foreach (var operation in operations.Skip(model.Quantity == 0 ? 0 : operations.Count - model.Quantity))
        {
            result.Add(new OperationResponse(operation.Id, operation.Price, operation.DateTime, 
                await  _operationManager.GetNameCategory(operation.Id)));
        }
        return result != null ? Ok(new AllOperationResponse(result)) : BadRequest();
    }
    
    [HttpGet("getOperationsByType")]
    public async Task<IActionResult> GetOperationsByType([FromQuery] AllOperationModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var operations = await _operationManager.GetOperationsByTypeAsync(token, model.DateTime);
        
        var responsesIncome = new List<OperationResponse>();
        foreach (var operation in operations.Item1.Skip(model.Count == 0 ? 0 : operations.Item1.Count - model.Count))
        {
            responsesIncome.Add(new OperationResponse(operation.Id, operation.Price, operation.DateTime, 
                await  _operationManager.GetNameCategory(operation.Id)));
        }
        
        var responsesExpenses = new List<OperationResponse>();
        foreach (var operation in operations.Item2.Skip(model.Count == 0 ? 0 : operations.Item2.Count - model.Count))
        {
            responsesExpenses.Add(new OperationResponse(operation.Id, operation.Price, operation.DateTime, 
                await  _operationManager.GetNameCategory(operation.Id)));
        }
        
        return Ok(new OperationByTypeResponse(responsesIncome, responsesExpenses));
    }

    [HttpGet("getSumByCategory")]
    public async Task<IActionResult> GetSumByCategory([FromQuery] SumByCategoryModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var sum = await _operationManager.GetSumByCategoryAsync(token, model.CategoryId, model.DateTime);
        return sum != null ? Ok(new SumResponse(sum)) : BadRequest();
    }
    
    [HttpGet("getSumByType")]
    public async Task<IActionResult> GetSumByType([FromQuery] SumByTypeModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var sum = await _operationManager.GetSumByTypeAsync(token, model.Type, model.DateTime);
        return sum != null ? Ok(new SumResponse(sum)) : BadRequest();
    }
    
    [HttpGet("getBalance")]
    public async Task<IActionResult> GetBalance()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        var balance = await _operationManager.GetBalanceAsync(token);
        return balance != null ? Ok(new BalanceResponse(balance)) : BadRequest();
    }

    [HttpPatch("createBalance")]
    public async Task<IActionResult> CreateBalance([FromBody] CreateBalanceModelRequest model)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (CheckNotValidAccess(token)) return StatusCode(403);
        await _operationManager.CreateBalanceAsync(token, model.NewBalance);
        return Ok();
    }
    
    private bool CheckNotValidAccess(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        return jwt.ValidTo < DateTime.UtcNow;
    }
}