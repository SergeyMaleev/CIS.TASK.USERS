using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IUserService
    {
        UserModels Authenticate(string email, string password);
        Task<IEnumerable<UserModels>> GetUsersAsync();
        Task<UserModels> FindByIdAsync(int id);
        Task<int> UpdateUserAsync(UserRequestModels user);
        Task<int> AddUserAsync(UserRequestModels user);
        Task<bool> RemoveUserAsync(int id);
        string GenerateJWT(UserModels user);
    }
}
