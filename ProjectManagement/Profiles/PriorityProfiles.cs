using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Profiles
{
	public class PriorityProfiles : Profile
	{
		public PriorityProfiles()
		{
			//Source -> Destination
			CreateMap<Priority, PriorityBaseVM>();
			//Destination -> Source 
			CreateMap<PriorityBaseVM, Priority>();

		}
	}
}
