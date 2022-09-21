using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		public DbSet<ApplicationUser> Developers => Set<ApplicationUser>();
		public DbSet<Project> Projects => Set<Project>();
		public DbSet<Ticket> Tickets => Set<Ticket>();
		public DbSet<Status> Statuses => Set<Status>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Project>().ToTable("Project");
			modelBuilder.Entity<Ticket>().ToTable("Ticket");
			modelBuilder.Entity<Status>().ToTable("Status");


			var hasher = new PasswordHasher<IdentityUser>();

			// Roles 

			IdentityRole adminRole = new IdentityRole();
			adminRole.Id = "989c13c2-05a1-471b-abe4-1aecf8485887";
			adminRole.Name = "Administrator";
			adminRole.NormalizedName = "ADMINISTRATOR";

			IdentityRole developerRole = new IdentityRole();
			developerRole.Id = "2bb80575-a02f-45e3-9504-1f225cbf237e";
			developerRole.Name = "Developer";
			developerRole.NormalizedName = "DEVELOPER";

			// Users 

			ApplicationUser admin = new ApplicationUser();
			admin.Id = "89c4435a-6986-4995-b514-2aec9af0ac3f";
			admin.UserName = "admin@gmail.com"; 
			admin.NormalizedUserName = "ADMIN@GMAIL.COM";
			admin.Email = "admin@gmail.com";
			admin.NormalizedEmail = "ADMIN@GMAIL.COM";
			admin.EmailConfirmed = true;
			admin.PasswordHash = hasher.HashPassword(admin, "Admin123#");

			ApplicationUser user1 = new ApplicationUser();
			user1.Id = "5393722f-d39e-4bb1-8a5e-9641e3ce4a25";
			user1.FirstName = "Nancy";
			user1.LastName = "Stjohn";
			user1.UserName = "Nancy@gmail.com";
			user1.NormalizedUserName = "NANCY@GMAIL.COM";
			user1.Email = "Nancy@gmail.com";
			user1.NormalizedEmail = "Nancy@GMAIL.COM";
			user1.EmailConfirmed = true;
			user1.PasswordHash = hasher.HashPassword(user1, "Nancy123#");

			ApplicationUser user2 = new ApplicationUser();
			user2.Id = "6d91481c-2946-4b3d-9117-072c4953475e";
			user2.FirstName = "Joseph";
			user2.LastName = "Bliss";
			user2.UserName = "Joseph@gmail.com";
			user2.NormalizedUserName = "JOSEPH@GMAIL.COM";
			user2.Email = "Joseph@gmail.com";
			user2.NormalizedEmail = "JOSEPH@GMAIL.COM";
			user2.EmailConfirmed = true;
			user2.PasswordHash = hasher.HashPassword(user1, "Joseph123#");

			// User Roles
			
			List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>()
			{
				new IdentityUserRole<string>()
				{
					RoleId = "989c13c2-05a1-471b-abe4-1aecf8485887",
					UserId = "89c4435a-6986-4995-b514-2aec9af0ac3f"
				},
				new IdentityUserRole<string>()
				{
					RoleId = "2bb80575-a02f-45e3-9504-1f225cbf237e",
					UserId = "5393722f-d39e-4bb1-8a5e-9641e3ce4a25"
				},
				new IdentityUserRole<string>()
				{
					RoleId = "2bb80575-a02f-45e3-9504-1f225cbf237e",
					UserId = "6d91481c-2946-4b3d-9117-072c4953475e"
				}
			};

			// Statuses

			List<Status> statuses = new List<Status>() 
			{
				new Status() 
				{
					Id=Guid.Parse("06351907-ea60-4f42-bcb4-8b8ce7ce87eb"),
					Name="Unassigned" 
				},
				new Status()
				{
					Id=Guid.Parse("4b67a21a-73a6-4310-a045-e9de6d075f8e"),
					Name="Pending"
				},
				new Status()
				{
					Id=Guid.Parse("1aba0280-ae62-4e88-9f3b-efc6a14e76ec"),
					Name="Started"
				},
				new Status()
				{
					Id=Guid.Parse("6acfa4c4-f146-4b4f-8709-f50710494b7b"),
					Name="Blocked"
				},
				new Status()
				{
					Id=Guid.Parse("1ce7d834-b757-4f42-9a0f-f55ee31f1e29"),
					Name="Completed"
				},
			};


			modelBuilder.Entity<IdentityRole>().HasData(adminRole, developerRole);
			modelBuilder.Entity<ApplicationUser>().HasData(admin,user1,user2);
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
			modelBuilder.Entity<Status>().HasData(statuses);

		}
	}
}