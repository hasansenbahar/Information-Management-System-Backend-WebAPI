using System.ComponentModel.DataAnnotations;

namespace WebService.API.Models.AllocationModels
{
    public class CreateAllocation
    {
        [Required]

        public DateTime EntryTime { get; set; }

        public Guid PersonId { get; set; }

        public DateTime? ExitTime { get; set; }

        public int AllocationCount { get; set; }

        public bool IsActive { get; set; }


    }

}
