using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
	public class Project
	{
		[Key]
		public string Id { get; set; } = null!;
		[Required]
		public string Name { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
		[Required]
		public Status Status { get; set; } = null!;
		public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

	}
}
