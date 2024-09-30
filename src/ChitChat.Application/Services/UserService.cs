using AutoMapper;
using ChitChat.Application.Exceptions;
using ChitChat.Application.Helpers;
using ChitChat.Application.Localization;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Application.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities;
using ChitChat.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ChitChat.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<LoginHistory> _loginHistoryRepository;
        private readonly ILogger<UserService> _logger;
        private UserManager<UserApplication> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private ITokenService _tokenService;
        private IMapper _mapper;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IRepositoryFactory repositoryFactory,
            UserManager<UserApplication> userManager, RoleManager<ApplicationRole> roleManager, ITokenService tokenService, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _loginHistoryRepository = repositoryFactory.GetRepository<LoginHistory>();
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            UserApplication user = await _userRepository.GetFirstOrDefaultAsync(p => p.UserName == loginRequestDto.UserName);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format(loginRequestDto.GetType(), loginRequestDto.UserName));
            }
            if (!isValid)
            {
                throw new InvalidModelException(ValidationTexts.NotValidate.Format(user.GetType(), "Password"));
            }
            // if user is found, Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _tokenService.GenerateAccessToken(user, roles);
            LoginHistory loginHistory = new();
            loginHistory.Id = Guid.NewGuid();
            loginHistory.LoginTime = DateTime.Now;
            loginHistory.UserId = user.Id;
            loginHistory.IsDeleted = false;
            var refreshToken = GenerateRefreshToken(loginHistory);
            await _loginHistoryRepository.AddAsync(loginHistory);

            UserDto userDto = _mapper.Map<UserDto>(user);
            /*   new()
           {
               Email = user.Email,
               PhoneNumber = user.PhoneNumber,
               FirstName = user.FirstName,
               LastName = user.LastName,
               Id = user.Id
           };*/
            LoginResponseDto loginResponse = new()
            {
                RefreshToken = refreshToken,
                AccessToken = accessToken,
                User = userDto,
                LoginHistoryId = loginHistory.Id
            };
            _logger.Log(LogLevel.Information, $"User {loginHistory.UserId} login at ${loginHistory.LoginTime}");
            return loginResponse;
        }

        public async Task<bool> LogoutAsync(Guid loginHistoryId)
        {
            LoginHistory loginHistory = await _loginHistoryRepository.GetFirstOrDefaultAsync(p => p.Id == loginHistoryId);
            loginHistory.LogoutTime = DateTime.Now;
            loginHistory.RefreshToken = null;
            loginHistory.RefreshTokenExpiryTime = null;
            await _loginHistoryRepository.UpdateAsync(loginHistory);
            _logger.Log(LogLevel.Information, $"User {loginHistory.UserId} logout at ${loginHistory.LogoutTime}");

            return true;
        }

        public async Task<bool> RegisterAsync(RegisterationRequestDto registerationRequestDto)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(p => p.Email == registerationRequestDto.Email);
            if (user != null)
                throw new ConflictException(ValidationTexts.Conflict.Format(user.GetType(), user.UserName));
            var loginResponseDto = new LoginResponseDto();
            UserApplication newUser = new()
            {
                UserName = registerationRequestDto.Email,
                Email = registerationRequestDto.Email,
                FirstName = registerationRequestDto.FirstName,
                LastName = registerationRequestDto.LastName,
                PhoneNumber = registerationRequestDto.PhoneNumber,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                AvatarUrl = "",
                Bio = "",
                UserStatus = Domain.Enums.UserStatus.Public,
                Gender = "Male"
            };
            var result = await _userManager.CreateAsync(newUser, registerationRequestDto.Password); // Hash password by .net identity
            if (result.Succeeded)
            {
                var userToReturn = await _userRepository.GetFirstOrDefaultAsync(p => p.Email == registerationRequestDto.Email);

                UserDto userToReturnDto = new UserDto()
                {
                    Email = userToReturn.Email,
                    Id = userToReturn.Id,
                    FirstName = userToReturn.FirstName,
                    LastName = userToReturn.FirstName,
                    PhoneNumber = userToReturn.LastName,
                };
                return true;
            }
            else
            {
                throw new InvalidModelException(result.ToString());
            }
        }
        private string GenerateRefreshToken(LoginHistory loginHistory)
        {
            var (refreshToken, validDays) = _tokenService.GenerateRefreshToken();
            loginHistory.RefreshToken = refreshToken;
            loginHistory.RefreshTokenExpiryTime = DateTime.Now.Add(TimeSpan.FromDays(validDays));
            return refreshToken;
        }
    }
}
