using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebService.API.Data.Entity;

namespace WebService.API.Models.AllocationModels
{
    public class UpdateAndPostAllocation
    {
        [Required]
        public DateTime EntryTime { get; set; }

        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public DateTime? ExitTime { get; set; }

        public int AllocationCount { get; set; }

        public bool IsActive { get; set; }


        public DateTime EntryTime2 { get; set; }

        public Guid PersonId2 { get; set; }

        public DateTime? ExitTime2 { get; set; }

        public int AllocationCount2 { get; set; }
        public bool IsActive2 { get; set; }


    }
}
