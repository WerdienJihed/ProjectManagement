using ProjectManagement.Models;

namespace ProjectManagement.Contracts
{
    public interface IPriorityRepository
    {
		Task<ICollection<Priority>> FindAll();
		Task<Priority> FindById(string? id);

	}
}
