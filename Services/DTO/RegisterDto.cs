using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
   
        public class RegisterRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Matricule { get; set; }
            public int RoleId { get; set; } // Example: role for user
        }

    
}
