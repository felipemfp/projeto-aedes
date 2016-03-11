namespace Aedes.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AedesDBContext : DbContext
    {
        public AedesDBContext() : base("name=AedesDB")
        {
            Database.SetInitializer(new AedesDBInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<Occurrence> Occurrences { get; set; }
    }

    public class AedesDBInitializer : DropCreateDatabaseIfModelChanges<AedesDBContext>
    {
        protected override void Seed(AedesDBContext context)
        {
            // Frequencies
            context.Frequencies.Add(new Frequency("Diariamente", 1));
            context.Frequencies.Add(new Frequency("Alternadamente", 2));
            context.Frequencies.Add(new Frequency("Semanalmente", 7));
            context.Frequencies.Add(new Frequency("Quinzenalmente", 15));
            context.Frequencies.Add(new Frequency("Mensalmente", 30));
            context.Frequencies.Add(new Frequency("Bimestralmente", 30 * 2));
            context.Frequencies.Add(new Frequency("Trimestralmente", 30 * 3));
            context.Frequencies.Add(new Frequency("Semestralmente", 30 * 6));
            context.Frequencies.Add(new Frequency("Anualmente", 365));
            context.Frequencies.Add(new Frequency("Bienalmente", 365 * 2));

            // Tasks
            context.Tasks.Add(new Task("Verifique se a caixa d'água está bem tampada", 2));
	        context.Tasks.Add(new Task("Limpe as calhas", 2));
	        context.Tasks.Add(new Task("Tampe os ralos", 1));
	        context.Tasks.Add(new Task("Abaixe as tampas dos vasos sanitários", 1));
	        context.Tasks.Add(new Task("Limpe a bandeja externa da geladeira", 1));
	        context.Tasks.Add(new Task("Recolha e acondicione o lixo do quintal", 1));
	        context.Tasks.Add(new Task("Verifique se as lixeiras estão bem tampadas", 1));
	        context.Tasks.Add(new Task("Cubra a piscina", 3));
	        context.Tasks.Add(new Task("Limpe e guarde as vasilhas dos bichos de estimação", 1));
	        context.Tasks.Add(new Task("Coloque areia nos pratos de plantas", 1));
            context.Tasks.Add(new Task("Limpe a bandeja coletora de água do ar condicionado", 1));

            base.Seed(context);
            context.SaveChanges();
        }
    }
}