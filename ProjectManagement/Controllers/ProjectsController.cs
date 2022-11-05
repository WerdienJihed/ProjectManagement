using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.CustomHelpers;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _repository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public ProjectsController 
            (
            IProjectRepository repository,
            ITicketRepository ticketRepository,
			IStatusRepository statusRepository, 
            IMapper mapper
            )
        {
            _repository = repository;
            _ticketRepository = ticketRepository;
			_statusRepository = statusRepository;
            _mapper = mapper;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
			ICollection<Project> projects = await _repository.FindAll();
			List<ProjectIndexVM> projectsVM = _mapper.Map<List<ProjectIndexVM>>(projects);
			return View(projectsVM);
		}

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project project = await _repository.FindById(id);

            if (project == null)
            {
                return NotFound();
            }

			ProjectDetailsVM projectVM = _mapper.Map<ProjectDetailsVM>(project);
            projectVM.Tickets = _mapper.Map<ICollection<Ticket>, List<TicketProjectVM>>(project.Tickets);
            return View(projectVM);
        }

        //GET: Projects/Create
        public IActionResult Create()
        {
			ProjectCreateVM projectVM = new ProjectCreateVM();
            
            return View(projectVM);
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateVM projectVM)
        {
            if (ModelState.IsValid)
            {
				Status status = await _statusRepository.GetStatusByEnum(StatusEnum.Unassigned);
				Project project = _mapper.Map<Project>(projectVM);
				project.Status = status;
				await _repository.Create(project);
                
                return RedirectToAction(nameof(Index));
            }
            return View(projectVM);
        }

        //GET: Projects/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project project = await _repository.FindById(id);
            if (project == null)
            {
                return NotFound();
            }
            ProjectEditVM projectVM = _mapper.Map<ProjectEditVM>(project);
            ICollection<Status> statuses = await _statusRepository.FindAll();

            projectVM.Statuses = _mapper.Map<ICollection<Status>,List<StatusBaseVM>>(statuses);
			return View(projectVM);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProjectEditVM projectVM)
        {
            if (id != projectVM.Id)
            {
                return NotFound();
            }
			ICollection<Status> statues = await _statusRepository.FindAll();
			projectVM.Statuses = _mapper.Map<ICollection<Status>,List<StatusBaseVM>>(statues);
            Status? status = statues.FirstOrDefault(s => s.Id == projectVM.SelectedStatusId);

			if (ModelState.IsValid)
            {
                try
                {
                    Project project = await _repository.FindById(id);
					
                    if (status == null)
					{
						throw new Exception("Status not found in database");
					}
					
                    project.Status = status;
					await _repository.Update(project);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.Exists(projectVM.Id))
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
            return View(projectVM);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project project = await _repository.FindById(id);

            if (project == null)
            {
                return NotFound();
            }

            ProjectBaseVM projectVM = _mapper.Map<ProjectBaseVM>(project);

            return View(projectVM);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Project project = await _repository.FindById(id);
            try
            {
				bool isDeleted = await _repository.Delete(project);
			}
            catch (Exception)
            {
				ICollection<Ticket> tickets = await _ticketRepository.FindByProjectId(id);

				if (tickets.Count() > 0 )
                {
                    ViewData["Tickets"] = tickets;
					ProjectBaseVM projectVM = _mapper.Map<ProjectBaseVM>(project);
					return View(projectVM);
				}
                else
                {
                    throw;
                }
            }
			return RedirectToAction(nameof(Index));
        }


		// POST: Projects/DeleteRelatedTickets/5
		[HttpPost, ActionName("DeleteRelatedTickets")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteRelatedTickets(string id)
		{
			Project project = await _repository.FindById(id);
            await _ticketRepository.DeleteByProjectId(id);
			ProjectBaseVM projectVM = _mapper.Map<ProjectBaseVM>(project);
            return View(nameof(Delete),projectVM);
		}
	}
}
