using AssesmentUser.Application;
using AssesmentUser.Domain;
using AssesmentUser.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService? _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
        }

        public DbSet<T> Entity<T>() where T : class, IEntity
        {
            return Set<T>();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Id = entry.Entity.Role == RoleEnum.Administrator ? Guid.Empty : entry.Entity.Id;
                    entry.Entity.CreatedBy = string.IsNullOrEmpty(entry.Entity.CreatedBy) ? _currentUserService?.IdUser ?? Guid.Empty.ToString() : entry.Entity.CreatedBy;
                    entry.Entity.CreatedAt = DateTime.UtcNow.Date;
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedBy = _currentUserService.IdUser == null ? Guid.Empty.ToString() : _currentUserService.IdUser;
                        entry.Entity.DeletedAt = DateTime.UtcNow.Date;
                    }
                    else
                    {
                        entry.Entity.UpdatedBy = _currentUserService.IdUser == null ? Guid.NewGuid().ToString() : _currentUserService.IdUser;
                        entry.Entity.UpdatedAt = DateTime.UtcNow.Date;
                    }
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;   
        }

    }
}
