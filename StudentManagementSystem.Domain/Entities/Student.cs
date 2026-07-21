using StudentManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }
    }
}
