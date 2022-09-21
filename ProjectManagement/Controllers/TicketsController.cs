using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.Data;
using ProjectManagement.Models;
using ProjectManagement.viewModels;
using System.Security.Claims;

namespace ProjectManagement.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITicketRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly IDeveloperRepository _developerRepository;

        public TicketsController(ApplicationDbContext context, ITicketRepository repository, IProjectRepository projectRepository, IDeveloperRepository developerRepository)
        {
            _context = context;
            _repository = repository;
            _projectRepository = projectRepository;
			_developerRepository = developerRepository;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
            }
            else
            {
                bool isAdmin = User.IsInRole("Administrator");
				ICollection<Ticket> tickes = new List<Ticket>();

				if (isAdmin) 
                {
					tickes = await _repository.FindAll();
                }
                else
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
					tickes = await _repository.FindByUserId(userId);

				}

                
				
				List<TicketIndexVM> TicketsVM = new List<TicketIndexVM>();
				foreach (var ticket in tickes)
				{
					TicketIndexVM ticketVM = new TicketIndexVM();
					ticketVM.Id = ticket.Id;
                    ticketVM.Name = ticket.Name;
					ticketVM.CreatedAt = ticket.CreatedAt;
					ticketVM.ModifiedAt = ticket.ModifiedAt;
					ticketVM.Status = ticket.Status?.Name;
                    ticketVM.Project = ticket.Project?.Name;
                    ticketVM.Developer = $"{ticket.AssignedTo?.FirstName} {ticket.AssignedTo?.LastName}"  ;

					TicketsVM.Add(ticketVM);
				}
                if (isAdmin)
                {
					return View(TicketsVM);
				}
                else
                {
                    return View("DeveloperIndex",TicketsVM);
                }
            }
        }

		// GET: Tickets/Details/5
		[Authorize(Roles=("Administrator"))]
		public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _repository.FindById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketDetailsVM ticketVM = new TicketDetailsVM();
			ticketVM.Id = ticket.Id;
			ticketVM.Name = ticket.Name;
			ticketVM.Description = ticket.Description;
			ticketVM.CreatedAt = ticket.CreatedAt;
			ticketVM.ModifiedAt = ticket.ModifiedAt;
			ticketVM.Status = ticket.Status?.Name;
            if (ticket.Project != null) 
            {
				ticketVM.Project = new ProjectBaseVM() {Id = ticket.Project.Id,Name=ticket.Project.Name } ;
			}
			ticketVM.Developer = ticketVM.Developer = $"{ticket.AssignedTo?.FirstName} {ticket.AssignedTo?.LastName}"; ;

			return View(ticketVM);
        }

		// GET: Tickets/Create
		[Authorize(Roles = ("Administrator"))]
		public async Task<IActionResult> Create()
        {
            TicketCreateVM ticketVM = new TicketCreateVM();
            var projects = await _projectRepository.FindAll();
            var developers = await _developerRepository.FindAll();
            projects.ToList().ForEach(project => ticketVM.Projects?.Add(new ProjectBaseVM() {Id=project.Id, Name=project.Name }));
			developers.
                ToList().
                ForEach(developer => ticketVM.Developers?.Add(
                            new DeveloperBaseVM() 
                                { 
                                    Id= Guid.Parse(developer.Id), 
                                    FullName= $"{developer.FirstName} {developer.LastName}" 
                                }));

            return View(ticketVM);
        }

		// POST: Tickets/Create
		[Authorize(Roles = ("Administrator"))]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCreateVM ticketVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser developer = await _developerRepository.FindById(ticketVM.SelectedDeveloperId);
                Project project = await _projectRepository.FindById(ticketVM.SelectedProjectId);
				Status status = _context.Statuses.First(s => s.Name == "Pending");
				Ticket ticket = new Ticket();
                ticket.Name = ticketVM.Name;
                ticket.Project = project;
                ticket.AssignedTo = developer;
                ticket.Status = status;
                ticket.Description = ticketVM.Description;
                await _repository.Create(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticketVM);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.Include("Project").Include("AssignedTo").Include("Status").FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketEditVM ticketVM = new TicketEditVM();
            ticketVM.Id = ticket.Id;
            ticketVM.Name = ticket.Name;
			ticketVM.Description = ticket.Description;
            var projects = await _projectRepository.FindAll();
            var developers = await _developerRepository.FindAll(); 
            var statuses = await _context.Statuses.ToListAsync();

			projects.ToList().ForEach(project => ticketVM.Projects?.Add(new ProjectBaseVM() { Id=project.Id,Name=project.Name }));
			developers.ToList().ForEach(user => ticketVM.Developers?.Add(new DeveloperBaseVM() { Id = Guid.Parse(user.Id), FullName= $"{user.FirstName} {user.LastName}" }));
            statuses.ForEach(status => ticketVM.Statuses?.Add(new StatusBaseVM() { Id = status.Id, Name = status.Name }));

			ticketVM.SelectedProjectId = ticket.Project.Id;
            ticketVM.SelectedDeveloperId = Guid.Parse(ticket.AssignedTo.Id);
            ticketVM.SelectedStatusId = ticket.Status.Id;

			bool isAdmin = User.IsInRole("Administrator");

			if (isAdmin)
            {
                return View(ticketVM);
            }
            else 
            {
                return View("DeveloperEdit", ticketVM);
            }
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TicketEditVM ticketVM)
        {
            if (id != ticketVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					bool isAdmin = User.IsInRole("Administrator");
                    Ticket ticket = await _repository.FindById(id); 
                    if (isAdmin) 
                    {
					    var project = await _projectRepository.FindById(ticketVM.SelectedProjectId);
                        var AssignedTo = await _developerRepository.FindById(ticketVM.SelectedDeveloperId);
                        ticket.Project = project;
                        ticket.AssignedTo = AssignedTo;
                        ticket.Name= ticketVM.Name;
                        ticket.Description= ticketVM.Description;
                    }
                    var Status = await _context.Statuses.FindAsync(ticketVM.SelectedStatusId);
                    if (Status != null) 
                    {
						ticket.Status = Status;
					}
                    await _repository.Update(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.Exists(ticketVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticketVM);
        }

		// GET: Tickets/Delete/5
		[Authorize(Roles = ("Administrator"))]
		public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _repository.FindById(id);
			TicketBaseVM ticketVM = new TicketBaseVM();
			ticketVM.Id = ticket.Id;
            ticketVM.Name = ticket.Name;
			if (ticket == null)
            {
                return NotFound();
            }

            return View(ticketVM);
        }

		// POST: Tickets/Delete/5
		[Authorize(Roles = ("Administrator"))]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
            }
            var ticket = await _repository.FindById(id);
            if (ticket != null)
            {
                await _repository.Delete(ticket);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
