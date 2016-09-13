namespace TrackingSystem.Controllers
{
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using TrackingSystem.Models;
    using TrackingSystem.Services.Contracts;
    using TrackingSystem.ViewModels;

    [Authorize]
    [RoutePrefix("api/Group")]
    public class GroupController : BaseController
    {
        private readonly IGroupsService groups;
        private readonly IUsersService users;

        public GroupController(IGroupsService groupsService, IUsersService usersService)
        {
            this.groups = groupsService;
            this.users = usersService;
        }

        /// <summary>
        /// Changes the group distance. Users will be notified using the new distance
        /// </summary>
        /// <param name="newDistance"> The new distance to be notified to</param>
        /// <returns> The updated groupViewModel</returns>
        [HttpPost]
        public IHttpActionResult ChangeGroupDistance([FromUri]int newDistance)
        {
            var userId = User.Identity.GetUserId();

            //if (teachers.GetAll().FirstOrDefault(t => t.Id == userId) == null)
            //{
            //    return BadRequest("You can set the tracking distance only for a teacher");
            //}

            var group = groups.ChangeDistance(newDistance, userId);
            var groupViewModel = Mapper.Map<GroupViewModel>(group);

            return Ok(groupViewModel);
        }

        /// <summary>
        /// Returns all students in the group of the teacher
        /// </summary>
        /// <param name="id"> Optional specify which user's group you want. The default is the logged user</param>
        /// <returns> All students in the group</returns>
        [Route("GetUsersInGroup")]
        public ICollection<ApplicationUserViewModel> GetUsersInGroup(string id = null)
        {
            var userId = id;
            if (userId == null)
            {
                userId = User.Identity.GetUserId();
            }

            ApplicationUser user = users.Get(userId);
            if (user == null || user.Group == null)
            {
                return null;
            }

            var usersVM = user.Group.Users.Cast<ApplicationUser>().ToList();
            var usersViewModel = Mapper.Map<List<ApplicationUser>, List<ApplicationUserViewModel>>(usersVM);

            return usersViewModel;
        }

        /// <summary>
        /// Removes student from group
        /// </summary>
        /// <param name="id">The id of the user group</param>
        /// <returns>Ok if removed</returns>
        [Route("RemoveFromGroup")]
        public IHttpActionResult RemoveFromGroup([FromUri]string id)
        {
            string userName = id;
            ApplicationUser user = users.GetByUserName(id);          
            if (user == null)
            {
                return NotFound();
            }

            if (user.Group != null)
            {
                groups.RemoveFromGroup(user);
            }

            return Ok();
        }

        /// <summary>
        /// Returns the group of the current user
        /// </summary>
        /// <returns>GroupViewModel</returns>
        [Route("GetGroup")]
        public GroupViewModel GetGroup()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = users.Get(userId);

            //var group = user.Group;
            //if (group == null && isTeacher)
            //{
            //    group = groups.CreateGroup(user);
            //}

            //var groupViewModel = Mapper.Map<GroupViewModel>(group);

            return null;
        }
    }
}
