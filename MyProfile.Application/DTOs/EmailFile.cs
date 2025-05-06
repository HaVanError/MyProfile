using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Application.DTOs
{
    public class EmailFile
    {
        //public string FileName { get; set; } = string.Empty;
        //public byte[]? Content { get; set; }
        //public string ContentType { get; set; } = string.Empty;
        public string SenderEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public byte[] FileContent { get; set; } // File content dưới dạng byte array
        public string FileName { get; set; }
        public string ContentType { get; set; } // Ví dụ: "application/pdf"
    }
}
