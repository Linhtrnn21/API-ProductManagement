using Application.Models;
using Microsoft.AspNetCore.Identity;
using PM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public interface IUserService
    {
        Task UpdateUserAsync(UpdateRoleRequest request);
        Task<IdentityResult> DeleteUserAsync(string email);
        string GenerateJwtToken(ApplicationUser user);
    }
}
