using AssesmentPayment.Application.Features;
using AssesmentPayment.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Application.Features
{
    public class GetMyPaymentQuery : IRequest<List<PaymentModel>>
    {
    }

    public class GetMyPaymentQueryHandler : IRequestHandler<GetMyPaymentQuery, List<PaymentModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;

        public GetMyPaymentQueryHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<List<PaymentModel>> Handle(GetMyPaymentQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Payment>()
                .Where(x => x.UserId == Guid.Parse(_currentUser.IdUser) && !x.IsDeleted)
                .Select(x => new PaymentModel
                {
                    Id = x.Id,
                    EventCategoryId = x.EventCategoryId,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    PaymentStatus = x.PaymentStatus
                })
                .ToListAsync();

            if (query is null) throw new NotFoundException("payment not found");

            return query;
        }
    }
}
