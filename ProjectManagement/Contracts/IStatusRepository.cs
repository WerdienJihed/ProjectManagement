using ProjectManagement.CustomHelpers;
using ProjectManagement.Models;

namespace ProjectManagement.Contracts
{
    public interface IStatusRepository
    {
        Task<ICollection<Status>> FindAll();
		Task<Status> FindById(string? id);
        Task<Status> GetStatusByEnum(StatusEnum status);
	}
}
