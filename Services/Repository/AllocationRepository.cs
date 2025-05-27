using WebService.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebService.API.Models.PersonModels;
using WebService.API.Models;
using System.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebService.API.Helpers;
using WebService.API.Services.IServices;
using WebService.API.Data.Entity;

namespace WebService.API.Services.Repository
{
    public class AllocationRepository : Repository<Allocation>, IAllocationRepository
    {
        protected WebAPIDb _context;
    
        public AllocationRepository(WebAPIDb context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Allocation>>  GetByUserIdAsync(Guid? id)
        {
            return await _context.Allocation.Where(x => x.PersonId == id).ToListAsync();
        }
    }
}

