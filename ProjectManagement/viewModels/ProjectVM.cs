using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagement.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Windows.Markup;

namespace ProjectManagement.viewModels
{

	public class ProjectBaseVM
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
	}

	public class ProjectIndexVM : ProjectBaseVM
	{
		public string? Status { get; set; }
	}

	public class ProjectDetailsVM : ProjectBaseVM
	{
		public string? Status { get; set; }
		public List<TicketProjectVM>? Tickets { get; set; } =	new List<TicketProjectVM>();
		public List<DeveloperBaseVM> Developers { get; set; } = new List<DeveloperBaseVM>();
	}


	public class ProjectCreateVM : ProjectBaseVM
	{
	}

	public class ProjectEditVM : ProjectBaseVM
	{
		[DisplayName("Status")]
		public Guid SelectedStatusId { get; set; }
		public List<StatusBaseVM> Statuses { get; set; } = new List<StatusBaseVM>();
	}
}
