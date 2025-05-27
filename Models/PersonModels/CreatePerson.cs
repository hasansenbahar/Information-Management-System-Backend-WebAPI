using System.ComponentModel.DataAnnotations;

namespace WebService.API.Models.PersonModels
{
    public class CreatePerson
    {
        [Required]
        public string? Name { get; set; }

        public string? RegistrationId { get; set; }

        public string? Mission { get; set; }

        public bool IsReign { get; set; }
    }

}
