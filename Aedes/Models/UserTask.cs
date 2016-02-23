using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Aedes.Models
{
    [DataContract]
    public class UserTask
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }
        [DataMember]
        public bool IsReminder { get; set; }
        [DataMember]
        public TimeSpan TimeOfReminder { get; set; }

        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public virtual User User { get; set; }

        [DataMember]
        public int TaskId { get; set; }
        [DataMember]
        public virtual Task Task { get; set; }

        [IgnoreDataMember]
        public virtual List<Occurrence> Occurrences { get; set; }
    }
}