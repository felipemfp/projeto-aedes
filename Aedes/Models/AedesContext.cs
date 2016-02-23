namespace Aedes.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AedesContext : DbContext
    {
        // Your context has been configured to use a 'AedesModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Aedes.Models.AedesModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AedesModel' 
        // connection string in the application configuration file.
        public AedesContext()
            : base("name=AedesContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Occurrence> Occurrences { get; set; }
    }
}