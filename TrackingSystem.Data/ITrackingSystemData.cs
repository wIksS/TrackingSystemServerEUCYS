namespace TrackingSystem.Data
{
    using TrackingSystem.Data.Repositories;
    using TrackingSystem.Models;

    public interface ITrackingSystemData
    {
        IRepository<ApplicationUser> Users
        {
            get;
        }

        IRepository<Group> Groups
        {
            get;
        }

        IRepository<Coordinate> Coordinates
        {
            get;
        }

        IRepository<Event> Events
        {
            get;
        }

    }
}
