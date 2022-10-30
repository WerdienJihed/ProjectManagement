using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
	public class Status
	{
		[Key]
		public string Id { get; set; } = null!;
		[Required]
		public string Name { get; set; } = null!;
		public ICollection<Project> Projects { get; set; } = new List<Project>();
		public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

	}
}
