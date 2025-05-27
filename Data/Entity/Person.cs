using Microsoft.Extensions.Hosting;

namespace WebService.API.Data.Entity
{
    public class Person : BaseEntity
    {
        public string? Name { get; set; }

        public string? RegistrationId { get; set; }

        public string? Mission { get; set; }


        //public  ICollection<Allocation> Allocations { get; } =  new List<Allocation>();


    }
}
