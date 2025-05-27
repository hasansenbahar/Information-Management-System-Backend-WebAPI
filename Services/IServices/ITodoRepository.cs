using Microsoft.AspNetCore.Identity;
using WebService.API.Data.Entity;
using WebService.API.Models;
using WebService.API.Models.TodoModels;

namespace WebService.API.Services.IServices
{
    public interface ITodoRepository : IRepository<Todo>
    {

    }
}
