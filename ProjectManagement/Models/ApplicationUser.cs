using Microsoft.AspNetCore.Identity;

namespace ProjectManagement.Models
{
	#nullable disable
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
	}
}
