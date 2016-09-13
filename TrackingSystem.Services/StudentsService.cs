//namespace TrackingSystem.Services
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using TrackingSystem.Common;
//    using TrackingSystem.Data;
//    using TrackingSystem.Models;
//    using TrackingSystem.Services.Contracts;

//    public class StudentsService : IStudentsService
//    {
//        private readonly ITrackingSystemData data;
//        private readonly IDistanceCalculator calculator;

//        public StudentsService(ITrackingSystemData data, IDistanceCalculator distanceCalculator)
//        {
//            this.data = data;
//            this.calculator = distanceCalculator;
//        }

//        public void AddStudentToGroup(ApplicationUser leader, ApplicationUser user)
//        {
//            if (leader.Group == null)
//            {
//                var group = new Group();
//                group.Leader = leader;

//                data.Groups.Add(group);
//                data.Groups.SaveChanges();
//                leader.Group = group;
//            }

//            leader.Group.Users.Add(user);
//            user.Group = leader.Group;
//            data.Groups.SaveChanges();
//        }

//        public ICollection<ApplicationUser> GetGroupAvaiableStudents()
//        {
//            var students = data.Students.All().
//                           Where(s => s.GroupId == null).
//                           Cast<ApplicationUser>().ToList();

//            return students;
//        }

//        public Student Get(string id)
//        {
//            return data.Students.Find(id);
//        }


//        public IEnumerable<Student> GetAll()
//        {
//            return data.Students.All();
//        }

//        public IEnumerable<DistanceModel> CalculateDistance(ApplicationUser student)
//        {
//            var studentLastCoordinate = student.Coordinates.LastOrDefault();
//            Coordinate teacherLastCoordinate = null;
//            if (student.Group.Teacher != null)
//            {
//                teacherLastCoordinate = student.Group.Teacher.Coordinates.LastOrDefault();
//            }

//            if (studentLastCoordinate != null && teacherLastCoordinate != null)
//            {
//                var distance = calculator.Calculate(studentLastCoordinate.Latitude, teacherLastCoordinate.Latitude, studentLastCoordinate.Longitude, teacherLastCoordinate.Longitude);

//                yield return new DistanceModel()
//                {
//                    Distance = distance,
//                    User = student.Group.Teacher,
//                    Coordinate = studentLastCoordinate
//                };
//            };
//        }
//    }
//}
