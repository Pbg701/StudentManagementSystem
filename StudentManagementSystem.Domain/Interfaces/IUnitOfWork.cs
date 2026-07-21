using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        Task<int> CompleteAsync();
    }
}
