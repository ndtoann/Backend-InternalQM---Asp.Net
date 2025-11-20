using Backend_InternalQM.Entities;
using Backend_InternalQM.Models;
using Backend_InternalQM.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend_InternalQM.Services
{
    public class AuthService : IAuthService
    {
        private readonly DatabaseContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;

        public AuthService(DatabaseContext context, JwtSettings jwtSettings, ILogger<AuthService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var accountData = await (from a in _context.Account
                                         join e in _context.Employee on a.EmployeeId equals e.Id
                                         where a.UserName == request.Username && a.DeleteAt == null
                                         select new { Account = a, Employee = e })
                    .FirstOrDefaultAsync();

                if (accountData == null)
                {
                    _logger.LogWarning($"Tài khoản không hợp lệ: {request.Username}");
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Tài khoản hoặc mật khẩu không chính xác"
                    };
                }

                if (!accountData.Account.UserName.ValidPassword(accountData.Account.Salt, request.Password, accountData.Account.Password))
                {
                    _logger.LogWarning($"Đăng nhập thất bại: {request.Username}");
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Tài khoản hoặc mật khẩu không chính xác"
                    };
                }

                var userDto = await GetUserById(accountData.Account.Id);
                var token = GenerateJwtToken(userDto);

                _logger.LogInformation($"Đăng nhập thành công: {request.Username}");

                return new LoginResponse
                {
                    Success = true,
                    Message = "Đăng nhập thành công",
                    Token = token,
                    User = userDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi đăng nhập: {ex.Message}");
                return new LoginResponse
                {
                    Success = false,
                    Message = "Lỗi hệ thống"
                };
            }
        }

        public async Task<LoginResponse> Register(RegisterRequest request)
        {
            try
            {
                if (request.Password != request.ConfirmPassword)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Mật khẩu không trùng khớp"
                    };
                }

                var existingAccount = await _context.Account
                    .FirstOrDefaultAsync(a => a.UserName == request.UserName);

                if (existingAccount != null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Tài khoản đã tồn tại"
                    };
                }

                var employee = await _context.Employee
                    .FirstOrDefaultAsync(e => e.Id == request.EmployeeId && e.DeleteAt == null);

                if (employee == null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Nhân viên không họp lệ"
                    };
                }

                var salt = CheckPass.GenerateSalt();
                var passwordHash = request.UserName.ComputeSha256Hash(salt, request.Password);

                var account = new Account
                {
                    UserName = request.UserName,
                    Password = passwordHash,
                    Salt = salt,
                    EmployeeId = request.EmployeeId,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = 0
                };

                _context.Account.Add(account);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Đăng ký thành công: {request.UserName}");

                var userDto = await GetUserById(account.Id);
                var token = GenerateJwtToken(userDto);

                return new LoginResponse
                {
                    Success = true,
                    Message = "Đăng ký thành công",
                    Token = token,
                    User = userDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi đăng ký: {ex.Message}");
                return new LoginResponse
                {
                    Success = false,
                    Message = "Lỗi hệ thống"
                };
            }
        }

        public async Task<UserDto> GetUserById(long userId)
        {
            try
            {
                var accountData = await (from a in _context.Account
                                         join e in _context.Employee on a.EmployeeId equals e.Id
                                         where a.Id == userId && a.DeleteAt == null
                                         select new { Account = a, Employee = e })
                    .FirstOrDefaultAsync();

                if (accountData == null)
                    return null;

                var permissions = await (from ap in _context.AccountPermission
                                         join p in _context.Permission on ap.PermissionId equals p.Id
                                         where ap.AccountId == userId
                                         select p.ClaimValue)
                    .ToListAsync();

                return new UserDto
                {
                    Id = accountData.Account.Id,
                    UserName = accountData.Account.UserName,
                    EmployeeId = accountData.Employee.Id,
                    EmployeeName = accountData.Employee.EmployeeName,
                    EmployeeCode = accountData.Employee.EmployeeCode,
                    Avatar = accountData.Employee.Avatar,
                    Permissions = permissions
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy thông tin người dùng: {ex.Message}");
                return null;
            }
        }

        public string GenerateJwtToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("EmployeeId", user.EmployeeId.ToString()),
            new Claim("EmployeeCode", user.EmployeeCode ?? "")
        };

            foreach (var permission in user.Permissions ?? new List<string>())
            {
                claims.Add(new Claim("Permission", permission));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
