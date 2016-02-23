using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aedes.Models
{
    public class Occurrence
    {
        public int Id { get; set; }
        public DateTime DateOccurrence { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}