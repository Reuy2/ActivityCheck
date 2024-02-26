

using ActivityCheck.Domain.ViewEntity.Activity;

namespace ActivityCheck.Domain.Entity
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int DurationInSec { get; set; }

        public long UserId { get; set; }

        public void From(ActivityViewModel activityViewEntity)
        {
            Id = activityViewEntity.id;
            Name = activityViewEntity.Name;
            Description = activityViewEntity.Description;
            Created = activityViewEntity.Created;
            DurationInSec = activityViewEntity.DurationInSec;

        }

        public void From(ActivityViewModel activityViewEntity, long UserId)
        {
            Id = activityViewEntity.id;
            Name = activityViewEntity.Name;
            Description = activityViewEntity.Description;
            Created = activityViewEntity.Created;
            DurationInSec = activityViewEntity.DurationInSec;
            this.UserId = UserId;

        }
    }
}
