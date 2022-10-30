using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string FirstName { get; set; } = null!;
		[Required]
		public string LastName { get; set; } = null!;
		public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

		public string GetFullName()
		{
			return FirstName + " " + LastName;
		}
	}
}
