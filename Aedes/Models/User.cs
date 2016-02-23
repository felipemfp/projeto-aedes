using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aedes.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateRegister { get; set; }

        public virtual List<Occurrence> Occurrences { get; set; }
    }
}