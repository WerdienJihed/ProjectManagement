using AutoMapper;
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
		private readonly IMapper _mapper;

		public TicketsController(
            ApplicationDbContext context, 
            ITicketRepository repository, 
            IProjectRepository projectRepository, 
            IDeveloperRepository developerRepository,
            IMapper mapper
            )
        {
            _context = context;
            _repository = repository;
            _projectRepository = projectRepository;
			_developerRepository = developerRepository;
            _mapper = mapper;
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

                List<TicketIndexVM> ticketsVM = _mapper.Map<List<TicketIndexVM>>(tickes);

                if (isAdmin)
                {
					return View(ticketsVM);
				}
                else
                {
                    return View("DeveloperIndex", ticketsVM);
                }
            }
        }

		// GET: Tickets/Details/5
		[Authorize(Roles=("Administrator"))]
		public async Task<IActionResult> Details(string? id)
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

			TicketDetailsVM ticketVM = _mapper.Map<TicketDetailsVM>(ticket);
			return View(ticketVM);
        }

		// GET: Tickets/Create
		[Authorize(Roles = ("Administrator"))]
		public async Task<IActionResult> Create()
        {
			ICollection<Project> projects = await _projectRepository.FindAll();
			ICollection<ApplicationUser> developers = await _developerRepository.FindAll();
            ICollection<Priority> priorities = await _context.Priorities.ToListAsync();

			TicketCreateVM ticketVM = new TicketCreateVM();
            ticketVM.Projects = _mapper.Map<ICollection<Project>,List<ProjectBaseVM>>(projects);
            ticketVM.Developers = _mapper.Map<ICollection<ApplicationUser>, List<DeveloperBaseVM>>(developers);
            ticketVM.Priorities = _mapper.Map<ICollection<Priority>,List<PriorityBaseVM>>(priorities);
			return View(ticketVM);
        }

		// POST: Tickets/Create
		[Authorize(Roles = ("Administrator"))]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCreateVM ticketVM)
        {
			ICollection<Project> projects = await _projectRepository.FindAll();
			ICollection<ApplicationUser> developers = await _developerRepository.FindAll();
            ICollection<Priority> priorities = await _context.Priorities.ToListAsync();

			ticketVM.Projects = _mapper.Map<ICollection<Project>, List<ProjectBaseVM>>(projects);
			ticketVM.Developers = _mapper.Map<ICollection<ApplicationUser>, List<DeveloperBaseVM>>(developers);
            ticketVM.Priorities = _mapper.Map<ICollection<Priority>, List<PriorityBaseVM>>(priorities);

			if (ModelState.IsValid)
            {
                ApplicationUser developer = await _developerRepository.FindById(ticketVM.SelectedDeveloperId);
                Project project = await _projectRepository.FindById(ticketVM.SelectedProjectId);
				Priority? priority = await _context.Priorities.FindAsync(ticketVM.SelectedPriorityId);
				Status status = _context.Statuses.First(s => s.Name == "Pending");
                Ticket ticket = _mapper.Map<Ticket>(ticketVM);
                ticket.Status = status;
				ticket.AssignedTo = developer;
				ticket.Project = project;
                ticket.Priority = priority;
				await _repository.Create(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticketVM);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(string? id)
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
            ICollection<Project> projects = await _projectRepository.FindAll();
			ICollection<ApplicationUser> developers = await _developerRepository.FindAll();
			ICollection<Status> statuses = await _context.Statuses.ToListAsync();
			ICollection<Priority> priorities = await _context.Priorities.ToListAsync();

			TicketEditVM ticketVM = _mapper.Map<TicketEditVM>(ticket);
            ticketVM.Projects = _mapper.Map<ICollection<Project>, List<ProjectBaseVM>>(projects);
            ticketVM.Developers = _mapper.Map<ICollection<ApplicationUser>, List<DeveloperBaseVM>>(developers);
            ticketVM.Statuses = _mapper.Map<ICollection<Status>, List<StatusBaseVM>>(statuses);
			ticketVM.Priorities = _mapper.Map<ICollection<Priority>, List<PriorityBaseVM>>(priorities);


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
        public async Task<IActionResult> Edit(string id, TicketEditVM ticketVM)
        {
            if (id != ticketVM.Id)
            {
                return NotFound();
            }

			ICollection<Project> projects  = await _projectRepository.FindAll();
			ICollection<ApplicationUser> developers = await _developerRepository.FindAll();
			ICollection<Status> statuses = await _context.Statuses.ToListAsync();
			ICollection<Priority> priorities = await _context.Priorities.ToListAsync();

			ticketVM.Projects = _mapper.Map<ICollection<Project>, List<ProjectBaseVM>>(projects);
			ticketVM.Developers = _mapper.Map<ICollection<ApplicationUser>, List<DeveloperBaseVM>>(developers);
			ticketVM.Statuses = _mapper.Map<ICollection<Status>, List<StatusBaseVM>>(statuses);
			ticketVM.Priorities = _mapper.Map<ICollection<Priority>, List<PriorityBaseVM>>(priorities);

			Status? status = await _context.Statuses.FindAsync(ticketVM.SelectedStatusId);
			Priority? priority = await _context.Priorities.FindAsync(ticketVM.SelectedPriorityId);

            if (status == null)
            {
                throw new Exception("Status not found in database");
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
                        ticket.Name= ticketVM.Name;
                        ticket.Description= ticketVM.Description;
                        ticket.Project = project;
                        ticket.AssignedTo = AssignedTo;
                        ticket.Priority = priority;
					}
                   
					ticket.Status = status;
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
		public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _repository.FindById(id);
            TicketBaseVM ticketVM = _mapper.Map<TicketBaseVM>(ticket);
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
        public async Task<IActionResult> DeleteConfirmed(string id)
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

		[HttpPost]
		public async Task<StatusCodeResult> EditTicketStatus(string? id,[FromBody] string selectedStatus)
        {
			Ticket ticket = await _repository.FindById(id);
			var status = _context.Statuses.FirstOrDefault(s => s.Name == selectedStatus);
            
            if (id ==null ||ticket == null || status == null)
            {
				return BadRequest();
			}

            try
            {
				ticket.Status = status;
				await _repository.Update(ticket);
                return Ok();
			}
            catch (Exception)
            {

				return NotFound() ;
			}
		}

    }
}
