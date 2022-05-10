using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.CQRS.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceProject.Application.Commands.Users.AuthenticateCustomer
{
    public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticateUserCommandHandler(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<CommandResult<string>> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<User>(x => x.UserName == command.UserName);
            spec.Includes.Add(x => x.Role);
            var user = await _userRepository.FindOneAsync(spec, cancellationToken);
            if (user == null) return CommandResult<string>.Error("Customer is not valid.");
            if (user.Password != command.Password) return CommandResult<string>.Error("Your password is not exactly.");

            var claims = new[]
             {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var JwTToken = new JwtSecurityTokenHandler().WriteToken(token);
            return CommandResult<string>.Success(JwTToken);
        }
    }
}
