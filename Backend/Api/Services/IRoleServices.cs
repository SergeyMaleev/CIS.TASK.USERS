using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IRoleServices
    {
        Task<IEnumerable<RoleModels>> GetAvailableRoles();
        Task<IEnumerable<RoleModels>> AddUserRole(int userId, int roleId);
        Task<IEnumerable<RoleModels>> DeleteUserRole(int userId, int roleId);

    }
}
