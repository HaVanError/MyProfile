using MyProfile.Application.DTOs;
using MyProfile.Application.Messege;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Application.Interface
{
    public interface ISendMail
    {
        Task SendMailAsysn(ContactRequest request);
    }
}
