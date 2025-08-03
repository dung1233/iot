using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TTS.Dto;
using TTS.Models;
using TTS.Models.User;
using TTS.Repositories.Users;
namespace TTS.Service.Users
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        public UserService(UserRepository userRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;

        }

        public async Task<List<UserRepodto>> GetAll()
        {
            var list = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserRepodto>>(list);
        }
        public async Task<UserRepodto> CreateUser(Userdto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "User DTO cannot be null");
            }
            var user = new User
            {
                userName = userDto.userName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };
            return _mapper.Map<UserRepodto>(await _userRepository.CreateAsyncreated(user));

        }
        public async Task<bool> IsUsernameExistsAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(userName));
            }
            return await _userRepository.IsUsernameExistsAsync(userName);
        }

        public async Task<LoginResponseDto> LoginAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Username and password cannot be empty."
                };
            }
            var user = await _userRepository.GetByUsernameAsync(userName);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid password."
                };
            }

            string token = GenerateJwtToken(user);
            return new LoginResponseDto
            {
                Success = true,
                Message = "Login successful.",
               
                Token = token
            };
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.userName),
                new("userId", user.Id),
                new("userName", user.userName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
