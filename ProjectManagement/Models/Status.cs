namespace ProjectManagement.Models
{
    #nullable disable
	public class Status
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Project> Projects{ get; set; } = new List<Project>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
