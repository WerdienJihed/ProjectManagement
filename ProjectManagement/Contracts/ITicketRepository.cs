using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Contracts
{
	public interface ITicketRepository
	{
		Task<ICollection<Ticket>> FindAll();
		Task<ICollection<Ticket>> FindByUserId(string id);
		Task<ICollection<Ticket>> FindByProjectId(string id);
		Task<Ticket> FindById(string? id);
		Task<bool> Create(Ticket entity);
		Task<bool> Update(Ticket entity);
		Task<bool> Delete(Ticket entity);
		Task<bool> DeleteByProjectId(string id);
		Task<bool> Exists(string id);
		Task<bool> Save();
	}
}
