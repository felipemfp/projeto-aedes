using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Aedes.Models
{
    [DataContract]
    public class Task
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public int FrequencyId { get; set; }
        [DataMember]
        public virtual Frequency Frequency { get; set; }

        public Task(string description, int frequencyId)
        {
            this.Description = description;
            this.FrequencyId = frequencyId;
        }
    }
}