using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.Data;
using ProjectManagement.Models;

namespace ProjectManagement.Repository
{
    public class PriorityRepository : IPriorityRepository
    {
		private readonly ApplicationDbContext _context;

        public PriorityRepository(ApplicationDbContext context)
        {
			_context = context;

		}

        public async Task<ICollection<Priority>> FindAll()
        {
			return await _context.Priorities.ToListAsync();
		}

		public async Task<Priority> FindById(string? id)
		{
			return await _context.Priorities.FirstAsync(t => t.Id == id);
		}
	}
}
