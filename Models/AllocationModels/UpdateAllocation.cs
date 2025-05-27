using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebService.API.Data.Entity;

namespace WebService.API.Models.AllocationModels
{
    public class UpdateAllocation
    {
        [Required]
        public DateTime EntryTime { get; set; }

        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public DateTime ExitTime { get; set; }

        public int AllocationCount { get; set; }
        public string AllocationMission { get; set; }
        public bool IsActive { get; set; }




    }
}
