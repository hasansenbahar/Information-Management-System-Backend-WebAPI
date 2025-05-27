using WebService.API.Services.IServices;
using WebService.API.Services.Repository;

namespace WebService.API.Services.Contracts
{
   public interface IUnitOfWork : IDisposable
    {
    
        IPersonRepository Person
        {
            get;
        }
        IAllocationRepository Allocation
        {
            get;
        }


        Task<int> SaveAsync();
    }
}
