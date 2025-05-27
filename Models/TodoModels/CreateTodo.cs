using System.ComponentModel.DataAnnotations;

namespace WebService.API.Models.TodoModels
{
    public class CreateTodo
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
   
    }

}
