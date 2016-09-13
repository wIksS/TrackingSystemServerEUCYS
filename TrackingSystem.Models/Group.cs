namespace TrackingSystem.Models
{
    using System.Collections.Generic;

    public class Group
    {
        private ICollection<ApplicationUser> users;

        public Group()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public int Id
        {
            get;
            set;
        }

        public virtual ICollection<ApplicationUser> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
            }
        }

        public virtual ApplicationUser Leader
        {
            get;
            set;
        }

        public string LeaderId
        {
            get;
            set;
        }

        public int MaxDistance
        {
            get;
            set;
        }
    }
}
