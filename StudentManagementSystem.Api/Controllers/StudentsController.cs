using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Exceptions;

namespace StudentManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        /// <summary>
        /// Get all students with pagination and search
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10, max: 100)</param>
        /// <param name="searchTerm">Optional search term</param>
        /// <returns>Paginated list of students</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = null)
        {
            _logger.LogInformation("Getting students - Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            var result = await _studentService.GetAllStudentsAsync(pageNumber, pageSize, searchTerm);
            return Ok(result);
        }

        /// <summary>
        /// Get student by ID
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>Student details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting student with ID: {Id}", id);

            var student = await _studentService.GetStudentByIdAsync(id);
            return Ok(student);
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="createStudentDto">Student details</param>
        /// <returns>Created student</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto createStudentDto)
        {
            _logger.LogInformation("Creating new student");

            var student = await _studentService.CreateStudentAsync(createStudentDto);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        /// <summary>
        /// Update an existing student
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <param name="updateStudentDto">Updated student details</param>
        /// <returns>Updated student</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            _logger.LogInformation("Updating student with ID: {Id}", id);

            var student = await _studentService.UpdateStudentAsync(id, updateStudentDto);
            return Ok(student);
        }

        /// <summary>
        /// Delete a student (soft delete)
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting student with ID: {Id}", id);

            var result = await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}