using Microsoft.EntityFrameworkCore;
using WebService.API.Data;
using WebService.API.Data.Entity;
using WebService.API.Services.Contracts;
using WebService.API.Services.IServices;

namespace WebService.API.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private WebAPIDb context;
        public UnitOfWork(WebAPIDb context)
        {
            this.context = context;
            Todo = new TodoRepository(this.context);

            this.context = context;
            Person = new PersonRepository(this.context);

            this.context = context;
            Allocation = new AllocationRepository(this.context);
            
        }
        public ITodoRepository Todo
        {
            get;
            private set;
        }
        public IPersonRepository Person
        {
            get;
            private set;
        }
        public IAllocationRepository Allocation
        {
            get;
            private set;
        }



        public void Dispose()
        {
            context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
           
        }
    }
}
