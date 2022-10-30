using System.ComponentModel.DataAnnotations;


namespace ProjectManagement.Models
{
	public class Ticket
	{
		[Key]
		public string Id { get; set; } = null!;
		[Required]
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		[Required]
		public DateTime ModifiedAt { get; set; }
		[Required]
		public Project Project { get; set; } = null!;
		[Required]
		public ApplicationUser AssignedTo { get; set; } = null!;
		[Required]
		public Status Status { get; set; } = null!;
		[Required]
		public Priority Priority { get; set; } = null!;

	}
}
