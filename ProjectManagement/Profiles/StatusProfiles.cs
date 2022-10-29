using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Profiles
{
	public class StatusProfiles: Profile
	{
		public StatusProfiles()
		{
			//Source -> Destination
			CreateMap<Status, StatusBaseVM>();
			//Destination -> Source 
			CreateMap<StatusBaseVM, Status>();
		}
	}
}
