using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebService.API.Data.Entity;

namespace WebService.API.Models.TodoModels
{
    public class UpdateTodo
    {
        [Required]
        public string Title { get; set; }
        public Guid Id { get; set; }
    }
}
