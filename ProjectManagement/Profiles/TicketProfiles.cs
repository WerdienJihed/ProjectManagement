using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Profiles
{
	public class TicketProfiles : Profile
	{
		public TicketProfiles()
		{
			//Source -> Destination
			CreateMap<Ticket, TicketBaseVM>();
			CreateMap<Ticket, TicketIndexVM>()
					.ForMember(
							dest => dest.Status,
							opt => opt.MapFrom(src => src.Status.Name)
						)				
					.ForMember(
							dest => dest.Priority,
							opt => opt.MapFrom(src => src.Priority.Name)
						)
					.ForMember(
							dest => dest.Project,
							opt => opt.MapFrom(src => src.Project.Name)
						)
					.ForMember(
							dest => dest.Developer,
							opt => opt.MapFrom(src => src.AssignedTo.GetFullName())
						);
			CreateMap<Ticket, TicketDetailsVM>()
					.ForMember(
							dest => dest.Status,
							opt => opt.MapFrom(src => src.Status.Name)
						)
					.ForMember(
							dest => dest.Priority,
							opt => opt.MapFrom(src => src.Priority.Name)
						)
					.ForMember(
							dest => dest.Project,
							opt => opt.MapFrom(src => src.Project)
						)
					.ForMember(
							dest => dest.Developer,
							opt => opt.MapFrom(src => src.AssignedTo.GetFullName())
						);
			CreateMap<Ticket, TicketCreateVM>();
			CreateMap<Ticket, TicketEditVM>()
					.ForMember(
							dest => dest.SelectedStatusId,
							opt => opt.MapFrom(src => src.Status.Id))
					.ForMember(
							dest => dest.SelectedPriorityId,
							opt => opt.MapFrom(src => src.Priority.Id))
					.ForMember(
							dest => dest.SelectedDeveloperId,
							opt => opt.MapFrom(src => src.AssignedTo.Id))
					.ForMember(
							dest => dest.SelectedProjectId,
							opt => opt.MapFrom(src => src.Project.Id));

			CreateMap<Ticket, TicketProjectVM>()
					.ForMember(
							dest => dest.Status,
							opt => opt.MapFrom(src => src.Status.Name)
						)
					.ForMember(
							dest => dest.Developer,
							opt => opt.MapFrom(src => src.AssignedTo)
						);
			//Destination -> Source
			CreateMap<TicketBaseVM, Ticket>();
			CreateMap<TicketIndexVM, Ticket>();
			CreateMap<TicketDetailsVM, Ticket>();
			CreateMap<TicketCreateVM, Ticket>()
				.ForMember(
						dest => dest.Id,
						opt => opt.MapFrom(src => Guid.NewGuid()))
				.ForMember(
						dest => dest.CreatedAt,
						opt => opt.MapFrom(src => DateTime.Now))
				.ForMember(
						dest => dest.ModifiedAt,
						opt => opt.MapFrom(src => DateTime.Now));
			CreateMap<TicketEditVM, Ticket>();
			CreateMap<TicketProjectVM, Ticket>();
				
		}
	}
}
