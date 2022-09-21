using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ProjectManagement.Models
{
	#nullable disable
	public class Ticket
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
		public Project Project { get; set; }
		public ApplicationUser AssignedTo { get; set; }
		public Status Status { get; set; }

	}
}
