using AutoMapper;
using Domain.BookDomain.Model;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BookRegistrationModel, Book>();
            CreateMap<Book, BookModel>();
        }
    }
}
