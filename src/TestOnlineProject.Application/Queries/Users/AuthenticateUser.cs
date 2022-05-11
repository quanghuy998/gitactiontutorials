using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Domain.SeedWork;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Users
{
    public class AuthenticateUserQuery : IQuery<string>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }

    public class AuthenticateUserQueryHandler : IQueryHandler<AuthenticateUserQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticateUserQueryHandler(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<string> Handle(AuthenticateUserQuery query, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<User>(x => x.UserName == query.UserName);
            spec.Includes.Add(x => x.Role);
            var user = await _userRepository.FindOneAsync(spec, cancellationToken);
            if (user == null) return null;
            if (user.Password != query.Password) return null;

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
            return JwTToken;
        }
    }
}
