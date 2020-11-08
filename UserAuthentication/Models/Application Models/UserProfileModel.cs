using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAuthentication.Models.Application_Models
{
    public class UserProfileModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public string Profession { get; set; }
        public string Website { get; set; }
        public string Github { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string ProfilePicture { get; set; }
    }
}