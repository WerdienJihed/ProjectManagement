using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Contracts
{
	public interface IProjectRepository
	{
		Task<ICollection<Project>> FindAll();
		Task<Project> FindById(Guid? id);
		Task<bool> Create(Project entity);
		Task<bool> Update(Project entity);
		Task<bool> Delete(Project entity);
		Task<bool> Exists(Guid id);
		Task<bool> Save();
	}
}
