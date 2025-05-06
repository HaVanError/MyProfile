using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Domain.Interface
{
    public interface IEmailServices
    {
        Task EmailAsysnc(string to, string subject, string body);
    }
}
