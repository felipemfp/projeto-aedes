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
        [MaxLength(20)]
        [Required]
        public string Description { get; set; }
        [DataMember]
        [Required]
        public int Days { get; set; }

        public Frequency(string description, int days)
        {
            this.Description = description;
            this.Days = days;
        }
    }
}