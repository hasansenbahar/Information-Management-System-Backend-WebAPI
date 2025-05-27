using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebService.API.Data.Entity;

namespace WebService.API.Models.PersonModels
{
    public class UpdatePerson
    {
        [Required]
        public string? Name { get; set; }

        public string? RegistrationId { get; set; }

        public string? Mission { get; set; }
        public DateTime MissionChangeDate { get; set; }

        public bool IsReign { get; set; }

        public Guid Id { get; set; }
    }
}
