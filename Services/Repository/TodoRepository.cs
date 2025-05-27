using WebService.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebService.API.Models.TodoModels;
using WebService.API.Models;
using System.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebService.API.Helpers;
using WebService.API.Services.IServices;
using WebService.API.Data.Entity;

namespace WebService.API.Services.Repository
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(WebAPIDb context) : base(context)
        {
        }
    }
}

