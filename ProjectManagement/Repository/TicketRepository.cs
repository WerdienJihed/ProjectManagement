using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ProjectManagement.Contracts;
using ProjectManagement.Data;
using ProjectManagement.Models;

namespace ProjectManagement.Repository
{
	public class TicketRepository : ITicketRepository
	{
		private readonly ApplicationDbContext _context;

		public TicketRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<ICollection<Ticket>> FindAll()
		{
			return await _context.Tickets.Include("Project").Include("AssignedTo").Include("Status").ToListAsync();
		}
		public async Task<ICollection<Ticket>> FindByUserId(string id)
		{
			return await _context.Tickets.Include("Project").Include("AssignedTo").Include("Status").Where(t=>t.AssignedTo.Id == id).ToListAsync();
		}
		public async Task<Ticket> FindById(Guid? id)
		{
			return await _context.Tickets.Include("Project").Include("AssignedTo").Include("Status").FirstAsync(t => t.Id == id);
		}
		public async Task<bool> Create(Ticket entity)
		{
			await _context.Tickets.AddAsync(entity);
			return await Save();
		}
		public async Task<bool> Update(Ticket entity)
		{
			entity.ModifiedAt = DateTime.Now;
			_context.Tickets.Update(entity);
			return await Save();
		}
		public async Task<bool> Delete(Ticket entity)
		{
			_context.Tickets.Remove(entity);
			return await Save();
		}
		public async Task<bool> Exists(Guid id)
		{
			return await _context.Tickets.AnyAsync(t => t.Id == id);
		}
		public async Task<bool> Save()
		{
			int changes = await _context.SaveChangesAsync();
			return changes > 0;
		}
	}
}
