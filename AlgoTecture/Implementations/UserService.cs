using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Interfaces;
using AlgoTecture.Persistence.Core.Interfaces;

namespace AlgoTecture.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCredentialsValidator _userCredentialsValidator;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IBearerAuthenticator _bearerAuthenticator;

        public UserService(IUserCredentialsValidator userCredentialsValidator, IUnitOfWork unitOfWork, IPasswordEncryptor passwordEncryptor, IBearerAuthenticator bearerAuthenticator)
        {
            _userCredentialsValidator = userCredentialsValidator ?? throw new ArgumentNullException(nameof(userCredentialsValidator));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordEncryptor = passwordEncryptor ?? throw new ArgumentNullException(nameof(passwordEncryptor));
            _bearerAuthenticator = bearerAuthenticator ?? throw new ArgumentNullException(nameof(bearerAuthenticator));
        }

        public async Task<BearerTokenResponseModel> Create(UserCredentialModel userCredentialModel)
        {
            if (userCredentialModel == null) throw new ArgumentNullException(nameof(userCredentialModel));
            if (!_userCredentialsValidator.IsValidUserName(userCredentialModel.EmailLogin)) throw new ValidationException("Login is not valid");
            if (!_userCredentialsValidator.IsValidPassword(userCredentialModel.UserPassword)) throw new ValidationException("Password is not valid");

            var user = await _unitOfWork.Users.GetByEmail(userCredentialModel.EmailLogin);
            if (user !=null) throw new ValidationException("User with the same email was registered");

            var newUser = new User
            {
                Email = userCredentialModel.EmailLogin,
                CreateDateTime = DateTime.UtcNow
            };
            var createdUser = await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.CompleteAsync();
            if (createdUser == null) throw new ValidationException("User not added");
            
            var hashedPassword = _passwordEncryptor.Encrypt(userCredentialModel.UserPassword);
            var userAuthentication = await _unitOfWork.UserAuthentications.Add(new UserAuthentication{UserId = createdUser.Id, HashedPassword = hashedPassword});
            await _unitOfWork.CompleteAsync();
            if (userAuthentication == null) throw new ValidationException("User not authenticated");
            
            var claimIdentity = _bearerAuthenticator.GetIdentity(createdUser);

            var jwtToken = _bearerAuthenticator.GetJwtToken(claimIdentity);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new BearerTokenResponseModel() {Token = encodedJwt, Login = createdUser.Email}; 
        }
    }
}