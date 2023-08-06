using Microsoft.AspNetCore.Identity;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Model
{
    public class RegisterReturnModel
    {
        public IdentityResult IdentityResult { get; set; }
        public User User { get;set; }
    }
}
