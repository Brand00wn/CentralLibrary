using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserDomain.Model;
using Microsoft.AspNetCore.Identity;
using Repository.Entities;

namespace Domain.UserDomain
{
    public interface IUserDomain
    {
        Task<RegisterReturnModel> CreateUser(RegisterModel model);
    }
}
