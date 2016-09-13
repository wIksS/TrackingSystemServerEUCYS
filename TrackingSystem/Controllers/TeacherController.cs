//namespace TrackingSystem.Controllers
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Web.Mvc;
//    using TrackingSystem.Services.Contracts;
//    using TrackingSystem.Services.Web;
//    using TrackingSystem.ViewModels;
//    using TrackingSystem.Common.Mapping;
//    using TrackingSystem.Data;

//    public class TeacherController : BaseController
//    {
//        private readonly ITeachersService teachers;
//        private readonly ICacheService cache;
//        private readonly IUsersService userService;
//        public TeacherController(ITeachersService teachersService, ICacheService cacheService, IUsersService userService)
//        {
//            this.userService = userService;
//            this.teachers = teachersService;
//            this.cache = cacheService;
//        }

//        /// <summary>
//        /// Returns all teachers
//        /// </summary>
//        /// <returns>ICollection<TeacherViewModel></returns>
//        //[HttpGet]
//        //public ICollection<TeacherViewModel> GetAllTeachers()
//        //{
//        //    var teachers =this.cache.Get("teachers", () => this.teachers.GetAll().AsQueryable().To<TeacherViewModel>().ToList(), 30 * 60);
//        //    var pesho = this.teachers.GetAll();
//        //    var db = new TrackingSystemData();
//        //    db.Users.All();
//        //    return teachers;
//        //}

//        [HttpPost]
//        public ICollection<TeacherViewModel> TestTeacherPost(string pesho)
//        {
//            var teachers = this.cache.Get("teachers", () => this.teachers.GetAll().AsQueryable().To<TeacherViewModel>().ToList(), 30 * 60);

//            return teachers;
//        }
//    }
//}