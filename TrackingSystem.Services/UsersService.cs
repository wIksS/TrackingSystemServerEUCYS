namespace TrackingSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TrackingSystem.Common;
    using TrackingSystem.Data;
    using TrackingSystem.Models;
    using TrackingSystem.Services.Contracts;

    public class UsersService : IUsersService
    {
        private readonly ITrackingSystemData data;
        //private readonly ITeachersService teachers;
       //7 private readonly IStudentsService students;
        private readonly IDistanceCalculator calculator;

        public UsersService(ITrackingSystemData data, IDistanceCalculator distanceCalculator)
        {
            this.data = data;
            this.calculator = distanceCalculator;
        }

        public ApplicationUser Get(string id)
        {
            return data.Users.Find(id);
        }

        // User can be both a student and a teacher
        // We have to look in both repositories to find him
        public ApplicationUser GetByUserName(string userName)
        {
            ApplicationUser user = data.Users.All().First(t => t.UserName == userName);

            return user;
        }

        public void SaveChanges()
        {
            this.data.Users.SaveChanges();
        }

        public IEnumerable<DistanceModel> CalculateDistance(ApplicationUser user)
        {
            if (user.Group.LeaderId == user.Id)
            {
                return this.CalculateDistanceLeader(user);
            }
            else
            {
                return this.CalculateDistanceUser(user);
            }

              return null;            
        }


        public IEnumerable<ApplicationUser> GetGroupAvaiableUsers()
        {
            var users = data.Users.All().
                           Where(u => u.GroupId == null).
                           Cast<ApplicationUser>().ToList();

            return users;
        }

         public IEnumerable<DistanceModel> CalculateDistanceLeader(ApplicationUser user)
        {
            var leaderLastCoordinate = user.Coordinates.LastOrDefault();
            if (leaderLastCoordinate != null)
            {
                foreach (var userInGroup in user.Group.Users)
                {
                    var userLastCoordinate = userInGroup.Coordinates.LastOrDefault();

                    if (userLastCoordinate != null && leaderLastCoordinate != null)
                    {
                        var distance = calculator.Calculate(userLastCoordinate.Latitude, leaderLastCoordinate.Latitude, userLastCoordinate.Longitude, leaderLastCoordinate.Longitude);

                        yield return new DistanceModel()
                        {
                            Distance = distance,
                            User = userInGroup,
                            Coordinate = leaderLastCoordinate
                        };
                    }
                }
            }
        }

         public IEnumerable<DistanceModel> CalculateDistanceUser(ApplicationUser user)
         {
             var userLastCoordinate = user.Coordinates.LastOrDefault();
             Coordinate leaderLastCoordinate = null;
             if (user.Group.Leader != null)
             {
                 leaderLastCoordinate = user.Group.Leader.Coordinates.LastOrDefault();
             }

             if (userLastCoordinate != null && leaderLastCoordinate != null)
             {
                 var distance = calculator.Calculate(userLastCoordinate.Latitude, leaderLastCoordinate.Latitude, userLastCoordinate.Longitude, leaderLastCoordinate.Longitude);

                 yield return new DistanceModel()
                 {
                     Distance = distance,
                     User = user.Group.Leader,
                     Coordinate = userLastCoordinate
                 };
             };
         }
    }
}
