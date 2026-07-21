using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Application.DTOs
{
    public class CreateStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }
    }
}
