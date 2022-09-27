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
        public ProjectsController(ApplicationDbContext context, IProjectRepository repository,ITicketRepository ticketRepository)
        {
            _context = context;
            _repository = repository;
            _ticketRepository = ticketRepository;
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
				List<ProjectIndexVM> projectsVM = new List<ProjectIndexVM>();
				foreach (var project in projects)
				{
					ProjectIndexVM projectVM = new ProjectIndexVM();
					projectVM.Id=project.Id;
                    projectVM.Name = project.Name;
					projectVM.CreatedAt = project.CreatedAt;
					projectVM.ModifiedAt = project.ModifiedAt;
					projectVM.Status = project.Status.Name;
					projectsVM.Add(projectVM);
				}

                return View(projectsVM.AsEnumerable());
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

            ProjectDetailsVM projectVM = new ProjectDetailsVM();
			projectVM.Name=  project.Name;
			projectVM.Status = project.Status.Name;
            List<TicketProjectVM> ticketDetailsVMs = new List<TicketProjectVM>();

            project.Tickets.ForEach(t =>
            {

                var relatedTask = _context.Tickets.Include("Status").Include("AssignedTo").FirstOrDefault(x => x.Id == t.Id);

                if (relatedTask != null)
                {
                    var ticketVM = new TicketProjectVM()
                    {
                        Id = relatedTask.Id,
                        Name = relatedTask.Name,
                        Status = relatedTask.Status.Name
                    };

					if (t.AssignedTo != null) 
                    {
						var developer = new DeveloperBaseVM()
						{
							Id = Guid.Parse(t.AssignedTo.Id),
							UserName = t.AssignedTo.UserName,
                            FullName = $"{t.AssignedTo.FirstName} {t.AssignedTo.LastName}"
						};
						projectVM.Developers.Add(developer);
                        ticketVM.Developer = developer;
					}; 

					ticketDetailsVMs.Add(ticketVM);
				}
            });

			projectVM.Tickets = ticketDetailsVMs;


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
				
                Project project = new Project();
                project.Id = Guid.NewGuid();
                project.Name= projectVM.Name;
                project.CreatedAt = DateTime.Now;
                project.ModifiedAt = DateTime.Now;
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

			ProjectEditVM projectVM = new ProjectEditVM();
            projectVM.Id = project.Id;
            projectVM.Name = project.Name;

			_context.Statuses.ToList().ForEach(status => projectVM.Statuses.Add(new StatusBaseVM() { Id = status.Id, Name= status.Name }));
            projectVM.SelectedStatusId = project.Status.Id;

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
			statues.ForEach(status => projectVM.Statuses.Add(new StatusBaseVM() { Id = status.Id, Name = status.Name }));
			

			if (ModelState.IsValid)
            {
                try
                {
                    Project project = await _repository.FindById(id);
					var status = statues.FirstOrDefault(s => s.Id == projectVM.SelectedStatusId);
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

			ProjectBaseVM projectBaseVM = new ProjectBaseVM();
            projectBaseVM.Name = project.Name;
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
