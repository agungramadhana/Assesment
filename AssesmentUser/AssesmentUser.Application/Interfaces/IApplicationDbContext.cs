using AssesmentUser.Domain;
using AssesmentUser.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application
{
    public interface IApplicationDbContext
    {
        DbSet<T> Entity<T>() where T : class, IEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
