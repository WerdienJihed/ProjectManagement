using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.Data;
using ProjectManagement.Models;
using ProjectManagement.viewModels;

namespace ProjectManagement.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectRepository _repository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public ProjectsController 
            (
            ApplicationDbContext context, 
            IProjectRepository repository,
            ITicketRepository ticketRepository, 
            IMapper mapper
            )
        {
            _context = context;
            _repository = repository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {

            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            else
            {
				ICollection<Project> projects = await _repository.FindAll();
                List<ProjectIndexVM> projectsVM = _mapper.Map<List<ProjectIndexVM>>(projects);
                return View(projectsVM);
            }
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            Project project = await _repository.FindById(id);

            if (project == null)
            {
                return NotFound();
            }

			ProjectDetailsVM projectVM = _mapper.Map<ProjectDetailsVM>(project);
            projectVM.Tickets = await _mapper.ProjectTo<TicketProjectVM>(_context.Tickets).ToListAsync();
            projectVM.Tickets.ForEach(ticket =>
            {
                if (ticket.Developer != null)
                {
                    projectVM.Developers.Add(ticket.Developer);
                }
            });

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
				Status status = _context.Statuses.First(s => s.Name == "Unassigned");

                Project project = _mapper.Map<Project>(projectVM);
				project.Status = status;
				await _repository.Create(project);
                
                return RedirectToAction(nameof(Index));
            }
            return View(projectVM);
        }

        //GET: Projects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _repository.FindById(id);
            if (project == null)
            {
                return NotFound();
            }
            ProjectEditVM projectVM = _mapper.Map<ProjectEditVM>(project);
            projectVM.Statuses = await _mapper.ProjectTo<StatusBaseVM>(_context.Statuses).ToListAsync();
			return View(projectVM);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProjectEditVM projectVM)
        {
            if (id != projectVM.Id)
            {
                return NotFound();
            }
            var statues = await _context.Statuses.ToListAsync();
			projectVM.Statuses = _mapper.Map<List<Status>,List<StatusBaseVM>>(statues);

            if (ModelState.IsValid)
            {
                try
                {
                    Project project = await _repository.FindById(id);
                    project.Status = statues.FirstOrDefault(s => s.Id == projectVM.SelectedStatusId);
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _repository.FindById(id);

            if (project == null)
            {
                return NotFound();
            }

            ProjectBaseVM projectBaseVM = _mapper.Map<ProjectBaseVM>(project);

            return View(projectBaseVM);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var project = await _repository.FindById(id);
            if (project != null)
            {
                await _repository.Delete(project);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
