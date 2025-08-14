using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Users.Interfaces;
using AlgoTecture.Libraries.Users.Models;
using AlgoTecture.Libraries.Users.Models.AppsettingsModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AlgoTecture.Libraries.Users.Implementations
{
    public class BearerAuthenticator : IBearerAuthenticator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCredentialsValidator _userCredentialsValidator;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IOptions<AuthenticationOptions> _options;

        public BearerAuthenticator(IUserCredentialsValidator userCredentialsValidator, IUnitOfWork unitOfWork, IPasswordEncryptor passwordEncryptor, IOptions<AuthenticationOptions> options)
        {
            _userCredentialsValidator = userCredentialsValidator ?? throw new ArgumentNullException(nameof(userCredentialsValidator));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordEncryptor = passwordEncryptor ?? throw new ArgumentNullException(nameof(passwordEncryptor));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<BearerTokenResponseModel> BearerAuthentication(UserCredentialModel userCredentialModel)
        {
            if (userCredentialModel == null) throw new ArgumentNullException(nameof(userCredentialModel));
            
            if (!_userCredentialsValidator.IsValidUserName(userCredentialModel.EmailLogin)) throw new ValidationException("Login is not valid");
            if (!_userCredentialsValidator.IsValidPassword(userCredentialModel.UserPassword)) throw new ValidationException("Password is not valid");

            var user = await _unitOfWork.Users.GetByEmail(userCredentialModel.EmailLogin);

            if (user == null) throw new ArgumentNullException(nameof(user));

            var hashedPassword = _passwordEncryptor.Encrypt(userCredentialModel.UserPassword);
            var isValidPassword = await _unitOfWork.UserAuthentications.IsValidPasswordAsync(user.Id, hashedPassword);

            if (!isValidPassword) throw new ValidationException("Password or login is incorrect");

            var claimIdentity = GetIdentity(user);

            var jwtToken = GetJwtToken(claimIdentity);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new BearerTokenResponseModel {Token = encodedJwt, Login = user.Email!};
        }
        
        public ClaimsIdentity GetIdentity(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email!),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "default")
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        
        public JwtSecurityToken GetJwtToken(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
                throw new ArgumentNullException(nameof(claimsIdentity));
            var jwt = new JwtSecurityToken(issuer: _options.Value.JwtIssuer,
                audience: AuthenticationConstants.Audience,
                notBefore: DateTime.UtcNow,
                claims: claimsIdentity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthenticationConstants.LifeTimeMinutes)),
                signingCredentials: new SigningCredentials(AuthenticationConstants.GetSymmetricSecurityKey(_options.Value.JwtAlgotectureSecret),
                    SecurityAlgorithms.HmacSha256));
            return jwt;
        }
    }
}