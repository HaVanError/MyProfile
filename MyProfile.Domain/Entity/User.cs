using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Domain.Entity
{
    public class User
    {
        public int id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;

        private User() { }

    }
}
