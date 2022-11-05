using ProjectManagement.CustomHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.viewModels
{

	public class ProjectBaseVM
	{
		public string? Id { get; set; }
		public string Name { get; set; } = null!;
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
		public List<TicketProjectVM> Tickets { get; set; } =	new List<TicketProjectVM>();
		public List<DeveloperBaseVM> Developers { get; set; } = new List<DeveloperBaseVM>();
	}


	public class ProjectCreateVM : ProjectBaseVM
	{
	}

	public class ProjectEditVM : ProjectBaseVM
	{
		[DisplayName("Status")]
		[DropDownValidator]
		public string SelectedStatusId { get; set; } = null!;
		public List<StatusBaseVM> Statuses { get; set; } = new List<StatusBaseVM>();
	}
}
