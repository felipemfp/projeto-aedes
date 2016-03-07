using System;

namespace Aedes.Aplicativo.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsReminder { get; set; }
        public TimeSpan TimeOfReminder { get; set; }
        public string Username { get; set; }
        public virtual User User { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}