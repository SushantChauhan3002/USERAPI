using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Domain.Response;
using MediatR;

namespace CleanArchitecture.Application.Application
{
    public class AdminUserRequestInput : IRequest<Result<string>>
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
