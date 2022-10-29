using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Profiles
{
	public class DeveloperProfiles : Profile
	{
		public DeveloperProfiles()
		{
			//Source -> Destination
			CreateMap<ApplicationUser, DeveloperBaseVM>()
				.ForMember(dest => dest.Id,
						   opt => opt.MapFrom(src=>Guid.Parse(src.Id))
						   )
				.ForMember(
						   dest => dest.FullName, 
						   opt => opt.MapFrom( src=> src.GetFullName())
						   );
		}
	}
}
