using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Profiles
{
	public class ProjectProfiles :Profile
	{
		public ProjectProfiles()
		{
			//Source -> Destination
			CreateMap<Project,ProjectBaseVM>();
			CreateMap<Project, ProjectIndexVM>()
				.ForMember(
						dest => dest.Status,
						opt => opt.MapFrom( src => src.Status.Name));

			CreateMap<Project, ProjectDetailsVM>()
				.ForMember(
						dest => dest.Status,
						opt => opt.MapFrom(src => src.Status.Name));

			CreateMap<Project, ProjectCreateVM>();
			CreateMap<Project, ProjectEditVM>()
				.ForMember(
					dest => dest.SelectedStatusId,
					opt=> opt.MapFrom( src => src.Status.Id)
				);
			
			//Destination -> Source 
			CreateMap<ProjectBaseVM, Project>();
			CreateMap<ProjectIndexVM, Project>();
			CreateMap<ProjectDetailsVM, Project>();
			CreateMap<ProjectCreateVM, Project>()
				.ForMember(
						dest => dest.Id,
						opt => opt.MapFrom(src => Guid.NewGuid()))
				.ForMember(
						dest => dest.CreatedAt,
						opt => opt.MapFrom(src => DateTime.Now))
				.ForMember(
						dest => dest.ModifiedAt,
						opt => opt.MapFrom(src => DateTime.Now));
			CreateMap<ProjectEditVM, Project>();
				
		}
	}
}
