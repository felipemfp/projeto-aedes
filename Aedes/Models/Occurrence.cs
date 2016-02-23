using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Aedes.Models
{
    [DataContract]
    public class Occurrence
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public DateTime DateOccurrence { get; set; }

        [DataMember]
        public int UserTaskId { get; set; }
        [DataMember]
        public virtual UserTask UserTask { get; set; }
    }
}