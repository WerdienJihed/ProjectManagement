using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
	public class Priority
	{
		[Key]
		public string Id { get; set; } = null!;
		[Required]
		public string Name { get; set; } = null!;
		public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
	}
}
