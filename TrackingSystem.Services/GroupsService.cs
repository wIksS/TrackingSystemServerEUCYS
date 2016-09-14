namespace TrackingSystem.Services
{
    using TrackingSystem.Common;
    using TrackingSystem.Data;
    using TrackingSystem.Models;
    using TrackingSystem.Services.Contracts;

    public class GroupsService : IGroupsService
    {
        private readonly ITrackingSystemData data;

        public GroupsService(ITrackingSystemData data)
        {
            this.data = data;
        }

        public Group ChangeDistance(int newDistance, string userId)
        {
            ApplicationUser user = data.Users.Find(userId);
            var group = user.Group;

            group.MaxDistance = newDistance;
            this.data.Groups.SaveChanges();

            return group;
        }

        public void RemoveFromGroup(ApplicationUser user)
        {
            var group = user.Group;

            user.Group = null;
            user.GroupId = null;

            group.Users.Remove(user);
            data.Users.SaveChanges();

            if (group.Users.Count == 0)
            {
                ApplicationUser leader = group.Leader;
                leader.GroupId = null;
                leader.Group = null;
                data.Groups.Delete(group);
                data.Groups.SaveChanges();
            }
        }

        public Group CreateGroup(ApplicationUser user)
        {
            var group = new Group()
            {
                MaxDistance = AppConstants.MaxDistance,
                Leader = user,
                LeaderId = user.Id
            };

            this.data.Groups.Add(group);
            user.Group = group;
            this.data.Groups.SaveChanges();

            return group;
        }
    }
}
