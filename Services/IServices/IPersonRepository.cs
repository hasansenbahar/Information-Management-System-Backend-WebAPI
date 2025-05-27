using Microsoft.AspNetCore.Identity;
using WebService.API.Data.Entity;
using WebService.API.Models;
using WebService.API.Models.PersonModels;

namespace WebService.API.Services.IServices
{
    public interface IPersonRepository : IRepository<Person>
    {
    }
}
