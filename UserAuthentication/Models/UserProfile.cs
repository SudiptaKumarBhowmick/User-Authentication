using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserAuthentication.Models
{
    public class UserProfile
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Address { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Profession { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Website { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Github { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Twitter { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Instagram { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Facebook { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string ProfilePicture { get; set; }
    }
}