using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Contracts
{
	public interface ITicketRepository
	{
		Task<ICollection<Ticket>> FindAll();
		Task<ICollection<Ticket>> FindByUserId(string id);
		Task<Ticket> FindById(Guid? id);
		Task<bool> Create(Ticket entity);
		Task<bool> Update(Ticket entity);
		Task<bool> Delete(Models.Ticket entity);
		Task<bool> Exists(Guid id);
		Task<bool> Save();
	}
}
