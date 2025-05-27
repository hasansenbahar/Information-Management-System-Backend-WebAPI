using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebService.API.Data.Entity;
using WebService.API.Models;
using WebService.API.Models.PersonModels;
using WebService.API.Models.AllocationModels;
using WebService.API.Models.UserModels;

namespace WebService.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentityUser, ApiResponse>();
            CreateMap<RegisterUser, ApiResponse>();
            CreateMap<UpdateUser, ApiResponse>();
            CreateMap<CreatePerson, Person>().ReverseMap();
            CreateMap<UpdatePerson, Person>().ReverseMap();
            CreateMap<CreateAllocation, Allocation>().ReverseMap();
            CreateMap<UpdateAllocation, Allocation>().ReverseMap();
        }
        
    }
}
