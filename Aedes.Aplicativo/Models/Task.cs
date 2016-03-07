namespace Aedes.Aplicativo.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int FrequencyId { get; set; }
        public virtual Frequency Frequency { get; set; }
    }
}