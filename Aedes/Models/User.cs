using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Aedes.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        [Key]
        [MaxLength(50)]
        public string Username { get; set; }
        [DataMember]
        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
        [DataMember]
        [Required]
        [MaxLength(64)]
        public string Key { get; set; }
        [DataMember]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [DataMember]
        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }
        [DataMember]
        [Required]
        public DateTime DateRegister { get; set; }

        [IgnoreDataMember]
        public virtual List<UserTask> UserTasks { get; set; }
    }
}