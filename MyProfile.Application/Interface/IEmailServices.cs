using MyProfile.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Application.Interface
{
    public interface IEmailServices
    {
        Task SendEmailAsync(EmailFile request);
    }
}
