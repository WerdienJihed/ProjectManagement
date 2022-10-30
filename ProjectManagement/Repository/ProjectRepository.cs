using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.Data;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Repository
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly ApplicationDbContext _context;

		public ProjectRepository(ApplicationDbContext context) 
		{
			_context = context;
		}

		public async Task<ICollection<Project>> FindAll()
		{
			return await _context.Projects.Include(t=>t.Status).ToListAsync();
		}

		public async Task<Project> FindById(string? id)
		{
			return await _context.Projects.Include(t => t.Status).Include(t => t.Tickets).FirstAsync(t=>t.Id == id);
		}

		public async Task<bool> Create(Project entity) 
		{
			await _context.Projects.AddAsync(entity);
			return await Save();
		}

		public async Task<bool> Update(Project entity)
		{
			_context.Projects.Update(entity);
			return await Save();
		}
		public async Task<bool> Delete(Project entity)
		{
			_context.Projects.Remove(entity);
			return await Save();
		}

		public async Task<bool> Exists(string id)
		{
			return await _context.Projects.AnyAsync(t => t.Id == id);
		}
		public async Task<bool> Save()
		{
			int changes = await _context.SaveChangesAsync();
			return changes > 0;
		}

	}
}
