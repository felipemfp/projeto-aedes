using System;

namespace Aedes.Aplicativo.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateRegister { get; set; }
    }
}