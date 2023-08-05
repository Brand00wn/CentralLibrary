using Domain.UserDomain.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CentralLibrary.ViewModels.User
{
    public class RegisterViewModel : RegisterModel
    {
        public string ReturnUrl { get; set; }
    }
}
