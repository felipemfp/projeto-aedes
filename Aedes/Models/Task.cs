using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aedes.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsReminder { get; set; }

        public int FrequencyId { get; set; }
        public virtual Frequency Frequency { get; set; }

        public virtual List<Occurrence> Occurrences { get; set; }
    }
}