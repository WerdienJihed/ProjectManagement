using Microsoft.AspNetCore.Identity;
using ProjectManagement.Contracts;
using ProjectManagement.Models;

namespace ProjectManagement.Repository
{
    public class DeveloperRepository : IDeveloperRepository
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public DeveloperRepository(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<ICollection<ApplicationUser>> FindAll()
        {
			return await _userManager.GetUsersInRoleAsync("Developer");
        }

		public async Task<ApplicationUser> FindById(string id)
		{
			return await _userManager.FindByIdAsync(id.ToString());
		}


	}
}
