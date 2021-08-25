using Api.DataContext;
using Api.DataContext.Entity;
using Api.Models;
using Auth.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserServices: IUserService
    {
        private readonly EfDbContext _context;
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IMapper _mapper;

        public UserServices(EfDbContext context, IOptions<AuthOptions> authOptions, IMapper mapper)
        {
            _context = context;
            _authOptions = authOptions;
            _mapper = mapper;
        }

        public UserModels Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users
                .Include(t => t.Roles)
                .Where(x => x.Email == email.ToLower())
                .FirstOrDefault();

            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<User, UserModels>(user);
        }
      
        public async Task<IEnumerable<UserModels>> GetUsersAsync()
        {
            return await _context.Users
                .Include(t => t.Roles)
                .Select(x => _mapper.Map<User, UserModels>(x))
                .ToListAsync();
        }

        public async Task<UserModels> FindByIdAsync(int id)
        {
            return await _context.Users
                .Where(t => t.Id == id)
                .Include(t => t.Roles)
                .Select(x => _mapper.Map<User, UserModels>(x))
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateUserAsync(UserRequestModels user)
        {
            var updatingUser = await _context.Users
                .Where(t => t.Id == user.Id)
                .FirstAsync();
           
            updatingUser.Name = user.Name;
            updatingUser.Email = user.Email;

            _context.SaveChanges();

            return updatingUser.Id;
        }

        public async Task<int> AddUserAsync(UserRequestModels user)
        {
            var savingUser = _mapper.Map<UserRequestModels, User>(user);

            await _context.Users.AddAsync(savingUser);
            _context.SaveChanges();

            return savingUser.Id;
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            var user = await _context.Users.FirstAsync(t => t.Id == id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }



        internal static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (var i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public string GenerateJWT(UserModels user)
        {
            var securityKey = _authOptions.Value.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Roles.Select(t=>t.Name).FirstOrDefault())
            };
       
            var token = new JwtSecurityToken(
                _authOptions.Value.Issuer,
                _authOptions.Value.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(_authOptions.Value.TokenLiveTime),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
