using Domain.UserDomain.Model;
using Microsoft.AspNetCore.Mvc;

namespace CentralLibrary.ViewModels.User
{
    public class LoginViewModel : LoginModel
    {
        public string ReturnUrl { get; set; }
    }
}
