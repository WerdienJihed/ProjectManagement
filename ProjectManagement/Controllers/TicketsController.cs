using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.CustomHelpers;
using ProjectManagement.Models;
using ProjectManagement.viewModels;
using System.Security.Claims;

namespace ProjectManagement.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IPriorityRepository _priorityRepository;
        private readonly IStatusRepository _statusRepository;
		private readonly IMapper _mapper;

		public TicketsController(
            ITicketRepository repository, 
            IProjectRepository projectRepository, 
            IDeveloperRepository developerRepository,
            IPriorityRepository priorityRepository,
            IStatusRepository statusRepository,
            IMapper mapper
            )
        {
            _repository = repository;
            _projectRepository = projectRepository;
			_developerRepository = developerRepository;
            _priorityRepository = priorityRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
			bool isAdmin = User.IsInRole("Administrator");
			ICollection<Ticket> tickets = new List<Ticket>();

			if (isAdmin)
			{
				tickets = await _repository.FindAll();
			}
			else
			{
				string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				tickets = await _repository.FindByUserId(userId);

			}

			List<TicketIndexVM> ticketsVM = _mapper.Map<List<TicketIndexVM>>(tickets);

			if (isAdmin)
			{
				return View(ticketsVM);
			}
			else
			{
				return View("DeveloperIndex", ticketsVM);
			}
		}

		// GET: Tickets/Details/5
		[Authorize(Roles=("Administrator"))]
		public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _repository.FindById(id);
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
            ICollection<Priority> priorities = await _priorityRepository.FindAll();

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
            ICollection<Priority> priorities = await _priorityRepository.FindAll();

			ticketVM.Projects = _mapper.Map<ICollection<Project>, List<ProjectBaseVM>>(projects);
			ticketVM.Developers = _mapper.Map<ICollection<ApplicationUser>, List<DeveloperBaseVM>>(developers);
            ticketVM.Priorities = _mapper.Map<ICollection<Priority>, List<PriorityBaseVM>>(priorities);

			if (ModelState.IsValid)
            {
                ApplicationUser developer = await _developerRepository.FindById(ticketVM.SelectedDeveloperId);
                Project project = await _projectRepository.FindById(ticketVM.SelectedProjectId);
				Priority? priority = await _priorityRepository.FindById(ticketVM.SelectedPriorityId);
				Status status = await _statusRepository.GetStatusByEnum(StatusEnum.Pending);
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
		[Authorize(Roles = ("Administrator"))]
		public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _repository.FindById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ICollection<Project> projects = await _projectRepository.FindAll();
			ICollection<ApplicationUser> developers = await _developerRepository.FindAll();
			ICollection<Status> statuses = await _statusRepository.FindAll();
			ICollection<Priority> priorities = await _priorityRepository.FindAll();

			TicketEditVM ticketVM = _mapper.Map<TicketEditVM>(ticket);
            ticketVM.Projects = _mapper.Map<ICollection<Project>, List<ProjectBaseVM>>(projects);
            ticketVM.Developers = _mapper.Map<ICollection<ApplicationUser>, List<DeveloperBaseVM>>(developers);
            ticketVM.Statuses = _mapper.Map<ICollection<Status>, List<StatusBaseVM>>(statuses);
			ticketVM.Priorities = _mapper.Map<ICollection<Priority>, List<PriorityBaseVM>>(priorities);


			return View(ticketVM);
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
			ICollection<Status> statuses = await _statusRepository.FindAll();
			ICollection<Priority> priorities = await _priorityRepository.FindAll();

			ticketVM.Projects = _mapper.Map<ICollection<Project>, List<ProjectBaseVM>>(projects);
			ticketVM.Developers = _mapper.Map<ICollection<ApplicationUser>, List<DeveloperBaseVM>>(developers);
			ticketVM.Statuses = _mapper.Map<ICollection<Status>, List<StatusBaseVM>>(statuses);
			ticketVM.Priorities = _mapper.Map<ICollection<Priority>, List<PriorityBaseVM>>(priorities);


			if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = await _repository.FindById(id);
					ticket.Name = ticketVM.Name;
					ticket.Description = ticketVM.Description;
					ticket.Project = await _projectRepository.FindById(ticketVM.SelectedProjectId);
					ticket.AssignedTo = await _developerRepository.FindById(ticketVM.SelectedDeveloperId);
					ticket.Priority = await _priorityRepository.FindById(ticketVM.SelectedPriorityId); ;
					ticket.Status = await _statusRepository.FindById(ticketVM.SelectedStatusId); ;
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
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _repository.FindById(id);
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
            Ticket ticket = await _repository.FindById(id);
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

            bool exists = Enum.TryParse(selectedStatus,false,out StatusEnum statusEnum);
            
            if (exists)
                return BadRequest();

            Status status = await _statusRepository.GetStatusByEnum(statusEnum);
			try
            {
                ticket.Status = status;
                await _repository.Update(ticket);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
		}

    }
}
