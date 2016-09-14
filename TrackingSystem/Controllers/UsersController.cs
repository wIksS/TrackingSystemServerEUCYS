using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrackingSystem.Models;
using TrackingSystem.Services.Contracts;
using TrackingSystem.ViewModels;
using Microsoft.AspNet.Identity;

namespace TrackingSystem.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService users;
        private readonly IGroupsService groups;

        public UsersController(IUsersService users, IGroupsService groupsService)
        {
            this.users = users;
            this.groups = groupsService;
        }

        /// <summary>
        /// Returns all students that arent in any group
        /// </summary>
        /// <returns>ICollection<ApplicationUserViewModel></returns>
        [HttpGet]
        public ICollection<ApplicationUserViewModel> GetGroupAvaiableUsers()
        {
            string userId = User.Identity.GetUserId();

            var availableUsers = users.GetGroupAvaiableUsers().Where(u => u.Id != userId).ToList();
            var usersViewModel = Mapper.Map<List<ApplicationUser>, List<ApplicationUserViewModel>>(availableUsers);

            return usersViewModel;
        }

        [HttpPost]
        public IHttpActionResult AddStudentToGroup(string id)
        {
            string leaderId = User.Identity.GetUserId();            
            ApplicationUser leader = users.Get(leaderId);
            ApplicationUser user = users.GetByUserName(id);
            if (leader == null || user == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState));
            }

            if (leader.Group == null || leader.Group.LeaderId == null)
            {
                var group = groups.CreateGroup(leader);
                leader.Group = group;
                leader.GroupId = group.Id;
                leader.IsInExcursion = true;
                leader.IsLeader = true;
            }
            else if(leader.Id != leader.Group.LeaderId)
            {
                return BadRequest("You are already part of a group!");
            }

            leader.Group.Users.Add(user);
            user.Group = leader.Group;
            user.GroupId = leader.GroupId;
            users.SaveChanges();
            return Ok();
        }
    }
}
