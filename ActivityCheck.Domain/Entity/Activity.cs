

namespace ActivityCheck.Domain.Entity
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int DurationInSec { get; set; }
    }
}
