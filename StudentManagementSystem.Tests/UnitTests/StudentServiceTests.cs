using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Application.Mappings;
using StudentManagementSystem.Application.Services;
using StudentManagementSystem.Application.Validators;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Exceptions;
using StudentManagementSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StudentManagementSystem.Tests.UnitTests
{
    public class StudentServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly CreateStudentDtoValidator _createValidator;
        private readonly UpdateStudentDtoValidator _updateValidator;
        private readonly IStudentService _studentService;

        public StudentServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            var config = new MapperConfiguration(
    cfg =>
    {
        cfg.AddProfile<MappingProfile>();
    },
    NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
            _mapper = config.CreateMapper();

            _createValidator = new CreateStudentDtoValidator();
            _updateValidator = new UpdateStudentDtoValidator();

            _studentService = new StudentService(
                _unitOfWorkMock.Object,
                _mapper,
                _createValidator,
                _updateValidator
            );
        }

        [Fact]
        public async Task GetStudentByIdAsync_ExistingStudent_ReturnsStudentDto()
        {
            // Arrange
            var student = new Student
            {
                Id = 1,
                Name = "Test Student",
                Email = "test@example.com",
                Age = 20,
                Course = "Computer Science",
                CreatedDate = DateTime.UtcNow
            };

            _unitOfWorkMock.Setup(x => x.Students.GetByIdAsync(1))
                .ReturnsAsync(student);

            // Act
            var result = await _studentService.GetStudentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Student", result.Name);
            Assert.Equal("test@example.com", result.Email);
            Assert.Equal(20, result.Age);
            Assert.Equal("Computer Science", result.Course);
        }

        [Fact]
        public async Task GetStudentByIdAsync_NonExistingStudent_ThrowsStudentNotFoundException()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Students.GetByIdAsync(999))
                .ReturnsAsync((Student)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<StudentNotFoundException>(
                () => _studentService.GetStudentByIdAsync(999));

            Assert.Contains("999", exception.Message);
        }

        [Fact]
        public async Task CreateStudentAsync_ValidStudent_ReturnsStudentDto()
        {
            // Arrange
            var createDto = new CreateStudentDto
            {
                Name = "New Student",
                Email = "new@example.com",
                Age = 22,
                Course = "Engineering"
            };

            var student = _mapper.Map<Student>(createDto);
            student.Id = 1;
            student.CreatedDate = DateTime.UtcNow;

            _unitOfWorkMock.Setup(x => x.Students.AddAsync(It.IsAny<Student>()))
                .ReturnsAsync(student);
            _unitOfWorkMock.Setup(x => x.CompleteAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _studentService.CreateStudentAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Email, result.Email);
            Assert.Equal(createDto.Age, result.Age);
            Assert.Equal(createDto.Course, result.Course);
        }

        [Theory]
        [InlineData("", "test@example.com", 22, "Engineering", "Name is required")]
        [InlineData("Test", "invalid-email", 22, "Engineering", "Email must be valid")]
        [InlineData("Test", "test@example.com", 0, "Engineering", "Age must be between 1 and 120")]
        [InlineData("Test", "test@example.com", 121, "Engineering", "Age must be between 1 and 120")]
        [InlineData("Test", "test@example.com", 22, "", "Course is required")]
        public async Task CreateStudentAsync_InvalidInput_ThrowsValidationException(
            string name, string email, int age, string course, string expectedMessage)
        {
            // Arrange
            var createDto = new CreateStudentDto
            {
                Name = name,
                Email = email,
                Age = age,
                Course = course
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => _studentService.CreateStudentAsync(createDto));

            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task UpdateStudentAsync_ExistingStudent_ReturnsUpdatedStudent()
        {
            // Arrange
            var student = new Student
            {
                Id = 1,
                Name = "Old Name",
                Email = "old@example.com",
                Age = 20,
                Course = "Old Course",
                CreatedDate = DateTime.UtcNow
            };

            var updateDto = new UpdateStudentDto
            {
                Name = "Updated Name",
                Email = "updated@example.com",
                Age = 25,
                Course = "Updated Course"
            };

            _unitOfWorkMock.Setup(x => x.Students.GetByIdAsync(1))
                .ReturnsAsync(student);
            _unitOfWorkMock.Setup(x => x.Students.UpdateAsync(It.IsAny<Student>()))
                .ReturnsAsync(student);
            _unitOfWorkMock.Setup(x => x.CompleteAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _studentService.UpdateStudentAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Email, result.Email);
            Assert.Equal(updateDto.Age, result.Age);
            Assert.Equal(updateDto.Course, result.Course);
            Assert.NotNull(result.UpdatedDate);
        }

        [Fact]
        public async Task DeleteStudentAsync_ExistingStudent_ReturnsTrue()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Students.ExistsAsync(1))
                .ReturnsAsync(true);
            _unitOfWorkMock.Setup(x => x.Students.DeleteAsync(1))
                .ReturnsAsync(true);
            _unitOfWorkMock.Setup(x => x.CompleteAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _studentService.DeleteStudentAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteStudentAsync_NonExistingStudent_ThrowsStudentNotFoundException()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Students.ExistsAsync(999))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<StudentNotFoundException>(
                () => _studentService.DeleteStudentAsync(999));
        }

        [Fact]
        public async Task GetAllStudentsAsync_WithPagination_ReturnsPaginatedResult()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Student 1", Email = "s1@test.com", Age = 20, Course = "Course 1", CreatedDate = DateTime.UtcNow },
                new Student { Id = 2, Name = "Student 2", Email = "s2@test.com", Age = 21, Course = "Course 2", CreatedDate = DateTime.UtcNow }
            };

            _unitOfWorkMock.Setup(x => x.Students.GetAllAsync(1, 10, null))
                .ReturnsAsync(students);
            _unitOfWorkMock.Setup(x => x.Students.GetTotalCountAsync(null))
                .ReturnsAsync(2);

            // Act
            var result = await _studentService.GetAllStudentsAsync(1, 10, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, result.Items.Count());
            Assert.Equal(1, result.PageNumber);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(1, result.TotalPages);
        }
    }
}