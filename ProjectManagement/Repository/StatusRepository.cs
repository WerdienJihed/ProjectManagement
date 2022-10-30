using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.CustomHelpers;
using ProjectManagement.Data;
using ProjectManagement.Models;

namespace ProjectManagement.Repository
{
    public class StatusRepository : IStatusRepository
    {
		private readonly ApplicationDbContext _context;

		public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Status>> FindAll()
        {
			return await _context.Statuses.ToListAsync();
		}

		public async Task<Status> FindById(string? id)
		{
			return await _context.Statuses.FirstAsync(t => t.Id == id);
		}

		public async Task<Status> GetStatusByEnum(StatusEnum status)
		{
			switch (status)
			{
				case StatusEnum.Unassigned:
					return await _context.Statuses.FirstAsync(t => t.Id == "06351907-ea60-4f42-bcb4-8b8ce7ce87eb");
				case StatusEnum.Started:
					return await _context.Statuses.FirstAsync(t => t.Id == "1aba0280-ae62-4e88-9f3b-efc6a14e76ec");
				case StatusEnum.Completed:
					return await _context.Statuses.FirstAsync(t => t.Id == "1ce7d834-b757-4f42-9a0f-f55ee31f1e29");
				case StatusEnum.Pending:
					return await _context.Statuses.FirstAsync(t => t.Id == "4b67a21a-73a6-4310-a045-e9de6d075f8e");
				case StatusEnum.Blocked:
					return await _context.Statuses.FirstAsync(t => t.Id == "6acfa4c4-f146-4b4f-8709-f50710494b7b");
				default:
					throw new ArgumentOutOfRangeException(nameof(status));
			}
		}
	}
}
