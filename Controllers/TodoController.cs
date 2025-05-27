using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WebService.API.Authorization;
using WebService.API.Constants;
using WebService.API.Data.Entity;
using WebService.API.Models;
using WebService.API.Models.TodoModels;
using WebService.API.Services.Contracts;
using WebService.API.Services.IServices;

namespace WebService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TodoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoController> _logger;
        private ApiResponse _response;

        public TodoController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TodoController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new ApiResponse();
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Permissions.Todo_View)]
        public async Task<IActionResult> GetTodo() 
        {
            //throw new Exception("Failed to retrieve data"); //for trying purpose
            var AllTodo = await _unitOfWork.Todo.GetAllAsync();
        
            if (AllTodo is null)
            {
                return NotFound(TextResources.NotFound);
            }

            _response.Result = AllTodo;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        [Authorize(Permissions.Todo_ViewById)]
        public async Task<IActionResult> GetTodobyId(string id)
        {
            var TodoById = await _unitOfWork.Todo.GetByIdAsync(new Guid(id));

            if (TodoById is null)
            {
                return NotFound("$`{id}` " + TextResources.NotFound);
            }
            _response.Result = TodoById;
            return Ok(_response);
        }

        [Authorize(Permissions.Todo_Edit)]
        [HttpPut()]
        public async Task<IActionResult> PutTodo(UpdateTodo Todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TextResources.BadRequestNotValidParams);
            }
            var put = _mapper.Map<Todo>(Todo);

            var updatedTodo = await _unitOfWork.Todo.UpdateAsync(put);
            await _unitOfWork.SaveAsync();

            if (updatedTodo is null)
            {
                _response.IsSuccess = false;

            }
            _response.Result = updatedTodo;
            return Ok(_response);

        }


        [Authorize(Permissions.Todo_Create)]
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] CreateTodo Todo)
        {
            _logger.LogDebug("Inside PostTodo endpoint"); //logging
            if (!ModelState.IsValid)
            {
                return BadRequest(TextResources.BadRequestNotValidParams);
            }

            var post = _mapper.Map<Todo>(Todo);

            var addedTodo = await _unitOfWork.Todo.AddAsync(post);
            var saveResult = await _unitOfWork.SaveAsync();

            if (saveResult == 0)
            {
                _response.IsSuccess = false;
                _response.Result = null;
            }
            _response.Result = addedTodo;
            _logger.LogDebug($"The response for the PostTodo {JsonConvert.SerializeObject(_response)}");
            return Ok(_response);
        }

        [Authorize(Permissions.Todo_Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(string id)
        {
            await _unitOfWork.Todo.DeleteAsync(new Guid(id));
            await _unitOfWork.SaveAsync();
            return Ok(_response.Result = "");
        }
        [Authorize(Permissions.Todo_Exists)]
        private async Task<bool> IsTodoExists(string id)
        {
            return await _unitOfWork.Todo.Exists(new Guid(id));
        }
    }
}
