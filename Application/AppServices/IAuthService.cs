using Application.Models;
using Microsoft.AspNetCore.Identity;
using PM.Entities;
using PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public interface IAuthService
    {
        Task Regiter(RegisterModel model);
        Task<LoginResponse> Login (LoginModel loginModel);
    }
}

