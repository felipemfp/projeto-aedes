using System;

namespace Aedes.Aplicativo.Models
{
    public class Occurrence
    {
        public int Id { get; set; }
        public DateTime DateOccurrence { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTask UserTask { get; set; }
    }
}