namespace TrackingSystem.Controllers
{
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using TrackingSystem.Infrastructure;
    using TrackingSystem.Models;
    using TrackingSystem.Services.Contracts;
    using TrackingSystem.ViewModels;
    using System.Linq;

    [System.Web.Http.Authorize]
    public class EventsController : BaseController
    {
        private readonly IEventsService events;
        private readonly IUsersService users;

        public EventsController(IEventsService eventsService, IUsersService usersService)
        {
            this.events = eventsService;
            this.users = usersService;
        }

        [HttpGet]
        public EventViewModel GetEvent(string id)
        {
            EventViewModel eventVM = null;
            ApplicationUser user = users.GetByUserName(id);
            if (user != null)
            {
                var eventDB = user.Events.FirstOrDefault();
                if (eventDB != null)
                {
                    eventVM = Mapper.Map<EventViewModel>(eventDB);

                }
            }

            return eventVM;
        }

        /// <summary>
        /// Creates an event and notifies all users in the group
        /// </summary>
        /// <param name="eventViewModel">the viewModel of the event</param>
        /// <returns>200 Ok if event is created</returns>
        [HttpPost]
        public IHttpActionResult AddEvent(EventViewModel eventViewModel)
        {
            if (eventViewModel == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState));
            }

            var userId = User.Identity.GetUserId();
            var dbEvent = Mapper.Map<Event>(eventViewModel);
            events.Add(userId, dbEvent);
            ApplicationUser user = users.Get(userId);
            user.Events.Add(dbEvent);
            users.SaveChanges();
            //SendEvent(eventViewModel, teacher.GroupId.ToString(), teacher.UserName);

            return Ok();
        }

        private void SendEvent(EventViewModel eventViewModel, string groupName, string clientUserName)
        {
            GlobalHost
           .ConnectionManager
           .GetHubContext<EventHub>().Clients.Group(groupName, UsersConnections.GetUserConnection(clientUserName)).receiveEvent(eventViewModel);
        }
    }
}