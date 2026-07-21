using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interfaces;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Student>> GetAllAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var query = _context.Students
                .AsNoTracking()
                .Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim();
                query = query.Where(s =>
                    s.Name.Contains(searchTerm) ||
                    s.Email.Contains(searchTerm) ||
                    s.Course.Contains(searchTerm));
            }

            return await query
                .OrderByDescending(s => s.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<Student> AddAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            await _context.Students.AddAsync(student);
            return student;
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            _context.Students.Update(student);
            return await Task.FromResult(student);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (student == null)
                return false;

            student.IsDeleted = true;
            student.UpdatedDate = DateTime.UtcNow;
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students
                .AnyAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var query = _context.Students
                .AsNoTracking()
                .Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim();
                query = query.Where(s =>
                    s.Name.Contains(searchTerm) ||
                    s.Email.Contains(searchTerm) ||
                    s.Course.Contains(searchTerm));
            }

            return await query.CountAsync();
        }
    }
}