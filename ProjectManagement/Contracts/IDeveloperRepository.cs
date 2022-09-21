using ProjectManagement.Models;

namespace ProjectManagement.Contracts
{
    public interface IDeveloperRepository
    {
		Task<ICollection<ApplicationUser>> FindAll();
		Task<ApplicationUser> FindById(Guid id);

	}
}
