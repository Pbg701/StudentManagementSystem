using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Domain.Exceptions
{
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException(int id)
            : base($"Student with ID {id} was not found.")
        {
        }

        public StudentNotFoundException(string message) : base(message)
        {
        }
    }
}
