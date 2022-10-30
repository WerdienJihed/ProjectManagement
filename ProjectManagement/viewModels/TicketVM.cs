using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectManagement.CustomHelpers;
using System.ComponentModel;


namespace ProjectManagement.viewModels
{
	public class TicketBaseVM 
	{
		public string? Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
	}


	public class TicketIndexVM : TicketBaseVM 
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public StatusEnum Status { get; set; }
		public string? Developer { get; set; }
		public string? Project { get; set; }
		public string? Priority { get; set; }
	}

	public class TicketDetailsVM : TicketBaseVM
	{
		public string? Status { get; set; }
		public string? Developer { get; set; }
		public string? Priority { get; set; }
		public ProjectBaseVM? Project { get; set; }
	}

	public class TicketCreateVM : TicketBaseVM
	{
		[DropDownValidator]
		[DisplayName("Project")]
		public string SelectedProjectId { get; set; } = null!;
		public List<ProjectBaseVM>? Projects { get; set; } = new List<ProjectBaseVM>();
		[DropDownValidator]
		[DisplayName("Developer")]
		public string SelectedDeveloperId { get; set; } = null!;
		public List<DeveloperBaseVM>? Developers { get; set; } = new List<DeveloperBaseVM>();
		[DropDownValidator]
		[DisplayName("Priority")]
		public string SelectedPriorityId { get; set; } = null!;
		public List<PriorityBaseVM>? Priorities { get; set; } = new List<PriorityBaseVM>();
	}

	public class TicketEditVM : TicketCreateVM
	{
		[DropDownValidator]
		[DisplayName("Status")]
		public string? SelectedStatusId { get; set; }
		public List<StatusBaseVM>? Statuses { get; set; } = new List<StatusBaseVM>();
	}
	public class TicketProjectVM : TicketBaseVM
	{
		public string Status { get; set; } = null!;
		public DeveloperBaseVM Developer { get; set; } = null!;

	}

}
