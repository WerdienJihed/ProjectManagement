using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Contracts
{
	public interface IProjectRepository
	{
		Task<ICollection<Project>> FindAll();
		Task<Project> FindById(string? id);
		Task<bool> Create(Project entity);
		Task<bool> Update(Project entity);
		Task<bool> Delete(Project entity);
		Task<bool> Exists(string id);
		Task<bool> Save();
	}
}
