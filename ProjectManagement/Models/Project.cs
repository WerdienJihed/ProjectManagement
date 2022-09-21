namespace ProjectManagement.Models
{
	#nullable disable
	public class Project
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
		public List<Ticket> Tickets { get; set; } = new List<Ticket>();
		public Status Status { get; set; }

	}
}
