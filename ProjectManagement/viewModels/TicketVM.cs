using System.ComponentModel;


namespace ProjectManagement.viewModels
{
	public class TicketBaseVM 
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
	}


	public class TicketIndexVM : TicketBaseVM 
	{
		public string? Status { get; set; }
		public string? Developer { get; set; }
		public string? Project { get; set; }
	}

	public class TicketDetailsVM : TicketBaseVM
	{
		public string? Status { get; set; }
		public string? Developer { get; set; }
		public ProjectBaseVM? Project { get; set; }
	}

	public class TicketCreateVM : TicketBaseVM
	{
		[DisplayName("Project")]
		public Guid SelectedProjectId { get; set; }
		public List<ProjectBaseVM>? Projects { get; set; } = new List<ProjectBaseVM>();
		[DisplayName("Developer")]
		public Guid SelectedDeveloperId { get; set; }
		public List<DeveloperBaseVM>? Developers { get; set; } = new List<DeveloperBaseVM>();
	}

	public class TicketEditVM : TicketCreateVM
	{
		[DisplayName("Status")]
		public Guid SelectedStatusId { get; set; }
		public List<StatusBaseVM>? Statuses { get; set; } = new List<StatusBaseVM>();
	}
	public class TicketProjectVM : TicketBaseVM
	{
		public string? Status { get; set; }
		public DeveloperBaseVM? Developer { get; set; }

	}

}
