using AssesmentPayment.Domain;
using AssesmentPayment.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Application
{
    public interface IApplicationDbContext
    {
        DbSet<T> Entity<T>() where T : class, IEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
