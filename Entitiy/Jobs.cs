namespace ToDos.Entity
{
    public class Jobs
    {
        public int id { get; set; }
        public string? jobName { get; set; }
        public string? jobTitle { get; set; }
        public string? jobDescription{ get; set; }
        public int priorityId { get; set; }
        public int statusId { get; set; }
        public DateTime createTime { get; set; }
        public DateTime finishTime { get; set; }
        public DateTime deleteTime { get; set; }
        public bool active { get; set; }
        public bool deleted { get; set; }
    }
    
}