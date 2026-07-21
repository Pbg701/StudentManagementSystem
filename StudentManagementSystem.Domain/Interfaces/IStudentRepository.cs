using StudentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync(int pageNumber, int pageSize, string searchTerm);
        Task<Student> GetByIdAsync(int id);
        Task<Student> AddAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync(string searchTerm);
    }
}
