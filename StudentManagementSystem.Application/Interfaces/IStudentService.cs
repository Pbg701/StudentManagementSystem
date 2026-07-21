using StudentManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace StudentManagementSystem.Application.Interfaces
{
    public interface IStudentService
    {
        Task<PaginatedResultDto<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize, string searchTerm);
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}
