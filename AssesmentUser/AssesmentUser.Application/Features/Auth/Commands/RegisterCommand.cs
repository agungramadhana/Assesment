using AssesmentUser.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application.Features
{
    public class RegisterCommand : IRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;

        public RegisterCommandHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Entity<Domain.User>().Any(x => x.Email!.ToLower() == request.Email.ToLower());

            if (query) throw new BadRequestException("Email has been registred");

            var passwordHash = _jwtProvider.Hash(request.Password);

            _dbContext.Entity<Domain.User>().Add(new Domain.User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Password = passwordHash,
                Email = request.Email,
                Role = RoleEnum.User,
                PhoneNumber = request.PhoneNumber
            });

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
