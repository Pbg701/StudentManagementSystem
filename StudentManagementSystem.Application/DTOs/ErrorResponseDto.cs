using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Application.DTOs
{
    public class ErrorResponseDto
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Path { get; set; }
        public string RequestId { get; set; }
    }
}
