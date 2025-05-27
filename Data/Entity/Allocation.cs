namespace WebService.API.Data.Entity
{
    public class Allocation : BaseEntity
    {
  
        //public Person Person { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public Guid PersonId { get; set; }

        public int AllocationCount { get; set; }

        public string AllocationMission { get; set; }
        public bool IsActive { get; set; }

    }
}
