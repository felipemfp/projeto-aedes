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
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        [EmailAddress]
        public string Email { get; set; }
        [DataMember]
        public DateTime DateRegister { get; set; }

        [IgnoreDataMember]
        public virtual List<UserTask> UserTasks { get; set; }
    }
}