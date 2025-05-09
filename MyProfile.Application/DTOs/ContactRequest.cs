using Microsoft.AspNetCore.Http; // đây là nguyên nhân code sai logic 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Application.DTOs
{
    public class ContactRequest
    {
        public string SenderEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        // public IFormFile File { get; set; } // không sử dụng iFORMFILE DO APPLI KO TRỰC TIẾP SỬ DỤNG HTTP 
        // public EmailFile File { get; set; }
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }
    }
}
