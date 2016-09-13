namespace TrackingSystem.Services
{
    using TrackingSystem.Data;
    using TrackingSystem.Models;
    using TrackingSystem.Services.Contracts;

    public class EventsService : IEventsService
    {
        private readonly ITrackingSystemData data;

        public EventsService(ITrackingSystemData data)
        {
            this.data = data;
        }

        public void Add(string leaderId, Event eventModel)
        {
            ApplicationUser leader = data.Users.Find(leaderId);

            data.Events.Add(eventModel);
            data.Events.SaveChanges();

            var group = leader.Group;
            foreach (var user in group.Users)
            {
                user.Events.Add(eventModel);
            }

            data.Users.SaveChanges();
        }
    }
}
