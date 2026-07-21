using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Application.Validators;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Exceptions;
using StudentManagementSystem.Domain.Interfaces;

namespace StudentManagementSystem.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateStudentDtoValidator _createValidator;
        private readonly UpdateStudentDtoValidator _updateValidator;

        public StudentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            CreateStudentDtoValidator createValidator,
            UpdateStudentDtoValidator updateValidator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _createValidator = createValidator ?? throw new ArgumentNullException(nameof(createValidator));
            _updateValidator = updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));
        }

        public async Task<PaginatedResultDto<StudentDto>> GetAllStudentsAsync(
            int pageNumber, int pageSize, string searchTerm)
        {
            // Validate pagination parameters
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var students = await _unitOfWork.Students.GetAllAsync(pageNumber, pageSize, searchTerm);
            var totalCount = await _unitOfWork.Students.GetTotalCountAsync(searchTerm);

            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

            return new PaginatedResultDto<StudentDto>
            {
                Items = studentDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
                throw new StudentNotFoundException(id);

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            // Validate
            var validationResult = await _createValidator.ValidateAsync(createStudentDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new System.ComponentModel.DataAnnotations.ValidationException(errors);
            }

            var student = _mapper.Map<Student>(createStudentDto);
            student.CreatedDate = DateTime.UtcNow;
            student.IsDeleted = false;

            var createdStudent = await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<StudentDto>(createdStudent);
        }

        public async Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto)
        {
            // Validate
            var validationResult = await _updateValidator.ValidateAsync(updateStudentDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new System.ComponentModel.DataAnnotations.ValidationException(errors);
            }

            var existingStudent = await _unitOfWork.Students.GetByIdAsync(id);

            if (existingStudent == null)
                throw new StudentNotFoundException(id);

            // Update only non-null properties
            _mapper.Map(updateStudentDto, existingStudent);
            existingStudent.UpdatedDate = DateTime.UtcNow;

            var updatedStudent = await _unitOfWork.Students.UpdateAsync(existingStudent);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<StudentDto>(updatedStudent);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            if (!await _unitOfWork.Students.ExistsAsync(id))
                throw new StudentNotFoundException(id);

            var result = await _unitOfWork.Students.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

            return result;
        }
    }
}