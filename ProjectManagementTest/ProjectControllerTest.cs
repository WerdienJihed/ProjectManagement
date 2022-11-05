using Xunit;
using AutoMapper;
using Moq;
using ProjectManagement.Contracts;
using ProjectManagement.Controllers;
using ProjectManagement.Models;
using ProjectManagement.Profiles;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.viewModels;

namespace ProjectManagementTest
{
	public class ProjectControllerTest 
	{
		private readonly Mock<IProjectRepository> _projectRepository;
		private readonly Mock<ITicketRepository> _ticketRepository;
		private readonly Mock<IStatusRepository> _statusRepository;
		private readonly IMapper _mapper;
		private readonly ProjectsController _controller;

		public ProjectControllerTest()
		{
			// Arrange
			_projectRepository = new Mock<IProjectRepository>();
			_projectRepository.Setup(repo => repo.FindAll())
				.ReturnsAsync(GetTestProjects());

			_ticketRepository = new Mock<ITicketRepository>();
			_ticketRepository.Setup(repo => repo.FindAll())
				.ReturnsAsync(GetTestTickets());

			_statusRepository = new Mock<IStatusRepository>();
			_statusRepository.Setup(repo => repo.FindAll())
				.ReturnsAsync(GetTestStatuses());


			//auto mapper configuration
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new ProjectProfiles());
			});
			_mapper = mockMapper.CreateMapper();
			_controller = new ProjectsController(_projectRepository.Object, _ticketRepository.Object, _statusRepository.Object, _mapper);

		}

		[Fact]
		public async Task Create_Should_Redirect_To_Index()
		{
			ProjectCreateVM projectVM = new ProjectCreateVM();
			projectVM.Name = "Project 1";
			var result = await _controller.Create(projectVM);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirectToActionResult.ActionName);
		}

		[Fact]
		public async Task Create_Should_Return_ViewModel()
		{
			ProjectCreateVM projectVM = new ProjectCreateVM();
			projectVM.Name = null!;
			var result = await _controller.Create(projectVM);
			var ViewResult = Assert.IsType<ViewResult>(result);
			Assert.Equal("Create", ViewResult.ViewName);
			Assert.Equal(projectVM, ViewResult.Model);
		}

		private ICollection<Project> GetTestProjects()
		{
			ICollection<Project> projects = new List<Project>();
			return projects;
		}
		private ICollection<Ticket> GetTestTickets()
		{
			ICollection<Ticket> tickets = new List<Ticket>();
			return tickets;
		}
		private ICollection<Status> GetTestStatuses()
		{
			ICollection<Status> statuses = new List<Status>();
			return statuses;
		}
	}
}