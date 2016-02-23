using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Aedes.Models
{
    [DataContract]
    public class Frequency
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Days { get; set; }
    }
}