using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace StudentManagementSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
